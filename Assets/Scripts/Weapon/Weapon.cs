using System;
using System.Collections.Generic;
using Character;
using Services.Input;
using UnityEngine;
using Weapon.Firing_Modes;
using Zenject;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        public WeaponSettings settings;
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public Recoil recoil;
        public BaseFiringMode[] firingModes;

        public event Action OnShot;
        
        private CharacterControllerScript _player;
        private IInputService _input;

        public int firingModeIndex = 0;
        private bool _isFiring;


        [Inject]
        private void Construct(CharacterControllerScript player, IInputService input)
        {
            _player = player;
            _input = input;

            Subscribe();
        }

        private void Subscribe()
        {
            _input.StartFiring += () => _isFiring = true;
            _input.FinishFiring += () => _isFiring = false;
        }
        
        private void Update()
        {
            if(_isFiring)
                Fire();
            
            CalculateAiming();
        }

        public void Fire() => 
            firingModes[firingModeIndex].TryShoot(OnShot);

        private void CalculateAiming()
        {
            if (_player.IsAiming)
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * settings.aimingTime);
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * settings.aimingTime);
        }
    }
}
