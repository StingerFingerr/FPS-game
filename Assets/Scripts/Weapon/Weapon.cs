using Character;
using Services.Input;
using UnityEngine;

namespace Weapon
{
    public class Weapon : MonoBehaviour
    {
        public WeaponSettings settings;
        public Vector3 hipPosition;
        public Vector3 aimPosition;
        public Recoil recoil;

        private IInputService _input;
        private CharacterControllerScript _player;
        
        private void Construct(CharacterControllerScript player, IInputService input)
        {
            _player = player;
            _input = input;
            
            Subscribe();
        }

        private void Subscribe()
        {
            _input.Fire += Fire;
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

        private void Fire()
        {

        }
    }
}
