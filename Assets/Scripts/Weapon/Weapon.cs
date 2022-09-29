using Character;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        public WeaponSettings settings;
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public Recoil recoil;

        private CharacterControllerScript _player;
        
        [Inject]
        private void Construct(CharacterControllerScript player)
        {
            _player = player;
        }

        
        private void Update()
        {
            CalculateAiming();
        }

        private void CalculateAiming()
        {
            if (_player.IsAiming)
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * settings.aimingTime);
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * settings.aimingTime);
        }
    }
}
