using Character;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class WeaponAnimation: MonoBehaviour
    {
        public Animator animator;
        
        private CharacterControllerScript _player;
        private IInputService _input;

        [Inject]
        private void Construct(CharacterControllerScript player ,IInputService input)
        {
            _player = player;
            _input = input;
        }
        
        
        
        /*private void SetAnimations()
        {
            if (_isJumping)
                return;

            if (isAiming)
            {
                animator.speed = 0;
                if(animator.speed < .05f)
                    animator.enabled = false;  
            
                animator.transform.localPosition =
                    Vector3.Lerp(animator.transform.localPosition, Vector3.zero, Time.deltaTime * settings.aimingTime);
                animator.transform.localRotation = Quaternion.Lerp(animator.transform.localRotation,
                    Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * settings.aimingTime);
                return;
            }
        
            animator.enabled = true;
        
            if (_playerMove == Vector2.zero)
            {
                animator.speed = 1;
                animator.SetBool("isIdle", true);
                return;
            }

            animator.SetBool("isIdle", false);
            animator.SetBool("isSprinting", _isSprinting);
            animator.speed = Mathf.Lerp(animator.speed, _characterVelocity, Time.deltaTime * 10f);
        }

        private void SetJumpingAnimation()
        {
            _isJumping = true;

            animator.speed = 1;
            animator.SetTrigger("isJumping");
        }

        private void SetFallingAnimation()
        {
            if(_isFalling)
                return;

            _isFalling = true;

            animator.speed = 1;
            animator.SetTrigger("isFalling");
        }
    
        private void SetLandingAnimation()
        {
            _isJumping = false;
            _isFalling = false;
            animator.speed = 1;

            animator.SetTrigger("isLanding");
        }*/

    }
}