using System;
using System.Collections;
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
        public event Action OnStartAiming;
        public event Action OnFinishAiming;
        public event Action<float> OnStartReloading;
        public event Action OnFinishReloading;
        
        public bool IsAiming { get; private set; }
        public bool IsReloading { get; private set; }

        private CharacterControllerScript _player;
        private IInputService _input;

        private int _firingModeIndex = 0;
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
            
            _input.SwitchFiringMode += SwitchFiringMode;
            _input.Reloading += Reload;
            
            _input.StartAiming += StartAiming;
            _input.FinishAiming += FinishAiming;
        }

        private void Update()
        {
            if(_isFiring)
                Fire();
            
            CalculateAiming();
        }

        private void SwitchFiringMode()
        {
            _firingModeIndex++;
            if (_firingModeIndex > firingModes.Length - 1)
                _firingModeIndex = 0;
        }

        public void Fire() => 
            firingModes[_firingModeIndex].TryShoot(OnShot);

        private void Reload()
        {
            if(IsAiming)
                FinishAiming();
            
            StartCoroutine(StartReloading());
        }

        private IEnumerator StartReloading()
        {
            IsReloading = true;
            OnStartReloading?.Invoke(settings.reloadingTime);

            yield return new WaitForSeconds(settings.reloadingTime);

            IsReloading = false;
            OnFinishReloading?.Invoke();
        }

        private void StartAiming()
        {
            if(IsReloading)
                return;
            
            IsAiming = true;
            OnStartAiming?.Invoke();
        }

        private void FinishAiming()
        {
            if(IsAiming is false)
                return;
            
            IsAiming = false;
            OnFinishAiming?.Invoke();
        }

        private void CalculateAiming()
        {
            if (IsAiming)
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * settings.aimingSpeed);
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * settings.aimingSpeed);
        }
    }
}
