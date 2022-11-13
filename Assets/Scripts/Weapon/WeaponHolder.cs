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
        private int _weaponIndex = 1;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;
            Subscribe();
        }

        private void Subscribe() => 
            _input.ThrowAway += ThrowAwayWeapon;

        public void SetNewWeapon(Weapon weapon)
        {
            switch (weapon.type)
            {
                case WeaponType.Pistol:
                    SetWeaponInSlot(ref _weaponsSlots[0], weapon);
                    break;
                case WeaponType.MachineGun: 
                    SetWeaponInSlot(ref _weaponsSlots[1], weapon);
                    break;
                case WeaponType.SniperRifle: 
                    SetWeaponInSlot(ref _weaponsSlots[2], weapon);
                    break;
            }
        }

        private void SetWeaponInSlot(ref Weapon slot, Weapon weapon)
        {
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
    }
}