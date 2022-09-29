using System;
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
        private bool _isFalling;

        [Inject]
        private void Construct(CharacterControllerScript player ,IInputService input)
        {
            _player = player;
            _input = input;

            Subscribe();
        }

        private void Subscribe()
        {
            _player.PlayerJump += SetJumpingAnimation;
            _player.PlayerLands += SetLandingAnimation;
            _player.PlayerFalling += SetFallingAnimation;
        }


        private void Update()
        {
            SetIdleAnimation();
            SetSprintingAnimation();
        }

        private void SetAnimations()
        {
            //if (_isJumping)
            //    return;
//
            //if (isAiming)
            //{
            //    animator.speed = 0;
            //    if(animator.speed < .05f)
            //        animator.enabled = false;  
            //
            //    animator.transform.localPosition =
            //        Vector3.Lerp(animator.transform.localPosition, Vector3.zero, Time.deltaTime * settings.aimingTime);
            //    animator.transform.localRotation = Quaternion.Lerp(animator.transform.localRotation,
            //        Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * settings.aimingTime);
            //    return;
            //}
//
        }

        private void SetIdleAnimation()
        {
            animator.SetBool("isIdle", _input.GetMove() == Vector2.zero);
        }

        private void SetSprintingAnimation()
        {
            animator.SetBool("isSprinting", _player.IsSprinting);
        }

        private void SetJumpingAnimation()
        {
            animator.SetTrigger("isJumping");
        }

        private void SetFallingAnimation()
        {
            if(_isFalling)
                return;
            
            _isFalling = true;
            animator.SetTrigger("isFalling");
        }
    
        private void SetLandingAnimation()
        {
            _isFalling = false;
            animator.SetTrigger("isLanding");
        }

    }
}