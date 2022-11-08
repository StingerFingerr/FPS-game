using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(order = 1, fileName = "gun_Settings", menuName = "ScriptableObjects/WeaponSettings")]
    public class WeaponSettings : ScriptableObject
    {
        public string weaponName;
        public float aimingSpeed = 10;

        public float reloadingTime = 3f;
    }
}
