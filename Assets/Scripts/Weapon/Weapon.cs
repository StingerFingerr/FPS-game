using System;
using System.Collections;
using Services.Input;
using UnityEngine;
using Weapon.Firing_Modes;
using Zenject;
using Random = UnityEngine.Random;

namespace Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class Weapon : MonoBehaviour, IInteractable
    {
        public WeaponSettings settings;
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public Recoil recoil;
        public BaseFiringMode[] firingModes;
        public WeaponType type;
        public Rigidbody rb;
        public new BoxCollider collider;
        public event Action OnShot;
        public event Action OnStartAiming;
        public event Action OnFinishAiming;
        public event Action<float> OnStartReloading;
        public event Action OnFinishReloading;
        public event Action OnWeaponPickedUp;
        public event Action OnWeaponThrown;
        
        public bool IsAiming { get; private set; }
        public bool IsReloading { get; private set; }

        private IInputService _input;

        private int _firingModeIndex = 0;
        private bool _isFiring;
        private bool _isPicked;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
        }

        public void Interact()
        {
            Subscribe();
            _isPicked = true;
            collider.enabled = false;
            rb.isKinematic = true;
            OnWeaponPickedUp?.Invoke();
        }

        public void ThrowAway()
        {
            UnSubscribe();
            _isPicked = false;
            IsAiming = false;
            transform.parent = null;

            collider.enabled = true;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * 3, ForceMode.Impulse);
            rb.AddTorque(transform.forward * Random.Range(-2, 2));

            OnWeaponThrown?.Invoke();
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

        private void UnSubscribe()
        {
            _input.SwitchFiringMode -= SwitchFiringMode;
            _input.Reloading -= Reload;
            
            _input.StartAiming -= StartAiming;
            _input.FinishAiming -= FinishAiming;
        }

        private void Update()
        {
            if(_isPicked is false)
                return;
            
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
