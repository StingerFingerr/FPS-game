using Services.Input;
using UnityEngine;
using Weapon;
using Zenject;

namespace Character
{
    public class Interaction: MonoBehaviour
    {
        public LayerMask interactableMask;
        public float interactableDistance = 2f;
        public Transform fpsCameraTransform;
        public WeaponHolder weaponHolder;
        
        private IInputService _input;

        private IInteractable _interactable;
        
        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;

            Subscribe();
        }

        private void Subscribe()
        {
            _input.Interact += Interact;
        }

        private void Update()
        {
            CastRay();
        }

        private void CastRay()
        {
            Ray ray = new Ray(fpsCameraTransform.position, fpsCameraTransform.forward);
            
            if (Physics.Raycast(ray, out RaycastHit hit, interactableDistance, interactableMask))
            {
                if (hit.transform.TryGetComponent<Weapon.Weapon>(out Weapon.Weapon weapon))
                    _interactable = weapon;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(new Ray(fpsCameraTransform.position, fpsCameraTransform.forward));
        }

        private void Interact()
        {
            _interactable?.Interact();
            
            if (_interactable is Weapon.Weapon)            
                PickUpWeapon((Weapon.Weapon) _interactable);

            _interactable = null;
        }

        private void PickUpWeapon(Weapon.Weapon weapon)
        {
            weaponHolder.SetNewWeapon(weapon);
        }
    }
}