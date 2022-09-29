using System;
using Character;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class Sway: MonoBehaviour
    {
        public WeaponSwaySettings settings;
        
        private CharacterControllerScript _player;
        private IInputService _input;

        private Quaternion _originRotation;
        private Quaternion _targetRotation;

        [Inject]
        private void Construct(CharacterControllerScript player, IInputService input)
        {
            _player = player;
            _input = input;
            
            Subscribe();
        }

        private void Subscribe()
        {
            
        }

        private void Start()
        {
            _originRotation = transform.localRotation;
        }

        private void FixedUpdate()
        {
            SetSway();
            CalculateSway();
            CalculateMovementSway();
        }

        private void CalculateSway()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation,
                transform.localRotation * _targetRotation, Time.fixedDeltaTime * settings.swaySmooth);
        }

        private void SetSway()
        {
            Vector2 look = _input.GetLook();
            
            float swayIntensityX = settings.swayIntensityX;
            float swayIntensityY = settings.swayIntensityY;

            if (_player.IsAiming)
            {
                swayIntensityX *= settings.aimingSwayIntensityModifier;
                swayIntensityY *= settings.aimingSwayIntensityModifier;
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

            //transform.localRotation =
            //    Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * settings.swaySmooth);
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
    }
}