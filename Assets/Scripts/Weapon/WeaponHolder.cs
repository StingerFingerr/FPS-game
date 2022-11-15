using System;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class WeaponHolder: MonoBehaviour
    {
        public event Action<Weapon> OnWeaponSwitched;

        private IInputService _input;
        
        private Weapon[] _weaponsSlots = new Weapon[3];
        private int _weaponIndex = 0;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
            Subscribe();
        }

        private void Subscribe()
        {
            _input.ThrowAway += ThrowAwayWeapon;
            _input.MouseScroll += SwitchWeapon;
        }

        public void SetNewWeapon(Weapon weapon)
        {
            switch (weapon.type)
            {
                case WeaponType.Pistol:
                    SwitchActiveSlot(0);
                    SetWeaponInSlot(ref _weaponsSlots[0], weapon);
                    break;
                case WeaponType.MachineGun: 
                    SwitchActiveSlot(1);
                    SetWeaponInSlot(ref _weaponsSlots[1], weapon);
                    break;
                case WeaponType.SniperRifle: 
                    SwitchActiveSlot(2);
                    SetWeaponInSlot(ref _weaponsSlots[2], weapon);
                    break;
            }
        }

        private void SwitchActiveSlot(int target)
        {
            if (target == _weaponIndex)
                return;
            
            _weaponsSlots[_weaponIndex]?.Hide();
            _weaponIndex = target;
        }
        private void SetWeaponInSlot(ref Weapon slot, Weapon weapon)
        {
            slot?.Take();
            slot?.ThrowAway();
            slot = weapon;

            weapon.transform.parent = transform;

            OnWeaponSwitched?.Invoke(weapon);
        }

        private void ThrowAwayWeapon()
        {
            _weaponsSlots[_weaponIndex]?.ThrowAway();
            _weaponsSlots[_weaponIndex] = null;

        }

        private void SwitchWeapon(float scroll)
        {
            _weaponsSlots[_weaponIndex]?.Hide();
            if (scroll > 0)
            {
                _weaponIndex++;
                if (_weaponIndex > _weaponsSlots.Length - 1)                
                    _weaponIndex = 0;
            }else if (scroll < 0)
            {
                _weaponIndex--;
                if (_weaponIndex < 0)
                    _weaponIndex = _weaponsSlots.Length - 1;
            }
            _weaponsSlots[_weaponIndex]?.Take();
        }
    }
}