using System;
using Character;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class Sway: MonoBehaviour
    {
        public Weapon weapon;
        public WeaponSwaySettings settings;

        private IInputService _input;

        private Quaternion _originRotation;
        private Quaternion _targetRotation;
        private bool _isCanSway;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
            Subscribe();
        }

        private void Subscribe()
        {
            weapon.OnWeaponPickedUp += () => _isCanSway = true;
            weapon.OnWeaponThrown += () =>  _isCanSway = false;
        }

        private void Start()
        {
            _originRotation = Quaternion.Euler(Vector3.zero);
        }

        private void FixedUpdate()
        {
            if(_isCanSway is false)
                return;
            
            SetSway();
            CalculateSway();
            CalculateMovementSway();
        }

        private void CalculateSway()
        {
            var localRotation = transform.localRotation;
            localRotation = Quaternion.Lerp(localRotation,
                localRotation * _targetRotation, Time.fixedDeltaTime * settings.swaySmooth);
            transform.localRotation = localRotation;
        }

        private void SetSway()
        {
            Vector2 look = _input.GetLook();
            
            float swayIntensityX = settings.swayIntensityX;
            float swayIntensityY = settings.swayIntensityY;

            if (weapon.IsAiming)
            {
                swayIntensityX *= AimingSwayIntensityModifier();
                swayIntensityY *= AimingSwayIntensityModifier();
            }
        
            Quaternion rotationX =
                Quaternion.AngleAxis(look.x * swayIntensityX * (settings.swayInvertedX ? -1 : 1),
                    Vector3.up);
            Quaternion rotationY =
                Quaternion.AngleAxis(look.y * swayIntensityY * (settings.swayInvertedY ? -1 : 1),
                    Vector3.right);

            rotationX.y = Math.Clamp(rotationX.y, -settings.swayClampX, settings.swayClampX);
            rotationY.x = Math.Clamp(rotationY.x, -settings.swayClampY, settings.swayClampY);

            _targetRotation = rotationX * rotationY;
        }

        private void CalculateMovementSway()
        {
            Vector2 move = _input.GetMove();
            
            Quaternion rotationX = Quaternion.AngleAxis(move.y * settings.movementSwayIntensity, Vector3.right);
            Quaternion rotationZ = Quaternion.AngleAxis(move.x * settings.movementSwayIntensity, Vector3.forward);

            rotationX.x = Math.Clamp(rotationX.x, -settings.movementSwayClampY, settings.movementSwayClampY);
            rotationZ.z = Math.Clamp(rotationZ.z, -settings.movementSwayClampX, settings.movementSwayClampX);

            Quaternion targetRotation = _originRotation * rotationX * rotationZ;

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, 
                    Time.fixedDeltaTime * settings.movementSwaySmooth);
        }

        private float AimingSwayIntensityModifier() => 
            settings.aimingSwayIntensityModifier;
    }
}