using System;
using UnityEngine;

namespace Weapon
{
    public class WeaponHolder: MonoBehaviour
    {
        public Weapon pistolSlot;
        public Weapon machineGunSlot;
        public Weapon sniperRifleSlot;

        public event Action<Weapon> OnWeaponSwitched;

        public void SetNewWeapon(Weapon weapon)
        {
            switch (weapon.type)
            {
                case WeaponType.Pistol:
                    SetWeaponInSlot(ref pistolSlot, weapon);
                    break;
                case WeaponType.MachineGun: 
                    SetWeaponInSlot(ref machineGunSlot, weapon);
                    break;
                case WeaponType.SniperRifle: 
                    SetWeaponInSlot(ref sniperRifleSlot, weapon);
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
    }
}