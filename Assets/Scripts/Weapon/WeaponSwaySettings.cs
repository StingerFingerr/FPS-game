using UnityEngine;

namespace Weapon
{
    public class WeaponSwaySettings: ScriptableObject
    {
        [Header("Sway")]
        public float swayIntensityX = .5f;
        public float swayIntensityY = .5f;
        public float aimingSwayIntensityModifier = .5f;
        public bool swayInvertedX = true;
        public bool swayInvertedY;
        public float swayClampX = .15f;
        public float swayClampY = .15f;
        public float swaySmooth = 10f;

        [Header("Movement Sway")] 
        public float movementSwayIntensity = 10f;
        public float movementSwayClampX = 20f;
        public float movementSwayClampY = 20f;
        public float movementSwaySmooth = 3f;
    }
}