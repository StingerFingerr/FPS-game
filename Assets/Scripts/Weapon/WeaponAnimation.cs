using Character;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Weapon
{
    public class WeaponAnimation: MonoBehaviour
    {
        private static readonly int IsIdle = Animator.StringToHash("isIdle");
        private static readonly int IsSprinting = Animator.StringToHash("isSprinting");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsFalling = Animator.StringToHash("isFalling");
        private static readonly int IsLanding = Animator.StringToHash("isLanding");

        public Animator animator;
        
        private CharacterControllerScript _player;
        private IInputService _input;
        private bool _isFalling;
        private bool _isAiming;

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

            _input.StartAiming += StartAiming;
            _input.FinishAiming += FinishAiming;
        }


        private void Update()
        {
            UpdateAiming();
            SetIdleAnimation();
            SetSprintingAnimation();
        }

        private void StartAiming()
        {
            _isAiming = true;
            animator.enabled = false;
        }

        private void FinishAiming()
        {
            _isAiming = false;
            animator.enabled = true;
            
            animator.Rebind();
        }

        private void UpdateAiming()
        {
            if(_isAiming)
            {
                float aimingSpeed = Time.deltaTime * 10f;
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, aimingSpeed);
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero), aimingSpeed);
            }
        }

        private void SetIdleAnimation() => 
            animator.SetBool(IsIdle, _input.GetMove() == Vector2.zero);

        private void SetSprintingAnimation() => 
            animator.SetBool(IsSprinting, _player.IsSprinting);

        private void SetJumpingAnimation() => 
            animator.SetTrigger(IsJumping);

        private void SetFallingAnimation()
        {
            if(_isFalling)
                return;
            
            _isFalling = true;
            animator.SetTrigger(IsFalling);
        }
    
        private void SetLandingAnimation()
        {
            _isFalling = false;
            animator.SetTrigger(IsLanding);
        }

    }
}