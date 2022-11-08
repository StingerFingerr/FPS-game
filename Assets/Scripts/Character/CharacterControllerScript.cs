using System;
using Services.Input;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Character
{
    public class CharacterControllerScript : MonoBehaviour
    {
        public LayerMask obstaclesLayerMask;
        public float checkSphereRadius;
        public Transform fpsCameraTransform;
        public PlayerSettings playerSettings;
        public float terminalVerticalVelocity = 10;
        public CharacterController characterController;

        public event Action PlayerJump;
        public event Action PlayerFalling;
        public event Action PlayerLands;
        public bool IsSprinting { get; private set; }
        public bool IsGrounded { get; private set; }

        private IInputService _input;


        private float _verticalVelocity;

        public Weapon.Weapon currentWeapon;

        private PlayerSettings.PlayerStance _currentStance = PlayerSettings.PlayerStance.Normal;
        private float _verticalRotation;
        private float _horizontalRotation;
        private Vector2 _recoil = Vector2.zero;
        private bool IsProneStance => _currentStance is PlayerSettings.PlayerStance.Prone;
        private bool IsCrouchStance => _currentStance is PlayerSettings.PlayerStance.Crouch;
        private bool IsStand => _currentStance is PlayerSettings.PlayerStance.Normal;

        [Inject]
        private void Construct(IInputService input)
        {
            _input = input;

            Subscribe();
        }

        private void Subscribe()
        {
            _input.StartSprinting += () => IsSprinting = true;
            _input.FinishSprinting += () => IsSprinting = false;
            _input.Jump += Jump;
            _input.Crouch += ToggleCrouch;
            _input.Prone += ToggleProne;

            //_input.StartAiming += () => IsAiming = true;
            //_input.FinishAiming += () => IsAiming = false;

            currentWeapon.OnShot += SetRecoil;
        }

        private void FixedUpdate()
        {
            Move();
            CheckGround();
            CalculateVerticalVelocity();
        }

        private void Update()
        {
            CalculateStance();
        }

        private void LateUpdate()
        {
            Look();
        }

        private void Look()
        {
            Vector2 look = _input.GetLook();
            
            float sensX = playerSettings.mouseSensitivityX;
            float sensY = playerSettings.mouseSensitivityY;

            if (currentWeapon.IsAiming)
            {
                sensX *= playerSettings.aimingMouseSensitivityModifier;
                sensY *= playerSettings.aimingMouseSensitivityModifier;
            }
        
            _horizontalRotation = look.x * sensX * Time.deltaTime *
                                  (playerSettings.mouseInvertedX ? 1 : -1);
            _verticalRotation += look.y * sensY * Time.deltaTime *
                                 (playerSettings.mouseInvertedY ? 1 : -1);

            float recoilSmooth = currentWeapon ? currentWeapon.recoil.recoilSmooth : 1;
            _recoil = Vector2.Lerp(_recoil, Vector2.zero, Time.deltaTime * recoilSmooth);

            _horizontalRotation += _recoil.x;
            _verticalRotation -= _recoil.y;
        
            _verticalRotation = Mathf.Clamp(_verticalRotation, playerSettings.bottomClamp, playerSettings.topClamp);

            fpsCameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
            transform.Rotate(Vector3.up, _horizontalRotation);

        }

        private void Move()
        {
            Vector2 move = _input.GetMove();
            
            CheckSprinting(move);

            Vector3 currentMoving = characterController.velocity;
            currentMoving.y = 0;
            
            float currentVelocity = currentMoving.magnitude;
            float targetVelocity = CalculateTargetVelocity(move);

            currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity * move.magnitude, Time.deltaTime * 10f);
            Vector3 inputDirection = new Vector3(move.x, 0, move.y).normalized;
            Vector3 newDirection = transform.TransformDirection(inputDirection);
            characterController.Move(newDirection * currentVelocity * Time.deltaTime +
                                      Vector3.up * _verticalVelocity * Time.deltaTime);
        }

        private void CheckGround()
        {
            bool newGrounded = characterController.isGrounded;
        
            if(_verticalVelocity < 0 && newGrounded is false)
                PlayerFalling?.Invoke();

            if(IsGrounded is false && newGrounded)
                PlayerLands?.Invoke();
        
            IsGrounded = newGrounded;
        }

        private void CheckSprinting(Vector2 move)
        {
            if (move.y < 0 ||
                IsProneStance||
                currentWeapon.IsAiming||
                currentWeapon.IsReloading)
                IsSprinting = false;
        }

        private float CalculateTargetVelocity(Vector2 move)
        {
            float targetVelocity = 0f;

            if (move != Vector2.zero)
            {
                if (math.abs(move.x) > 0)
                    targetVelocity = playerSettings.forwardSpeed * playerSettings.sidewaysSpeedModifier;
            
                if (move.y > 0)
                {
                    if(IsSprinting)
                        targetVelocity = playerSettings.sprintSpeed;
                    else 
                        targetVelocity = playerSettings.forwardSpeed;

                    if (math.abs(move.x) > 0)
                        targetVelocity *= playerSettings.sidewaysSpeedModifier;
                }
                else if (move.y < 0)
                    targetVelocity = playerSettings.forwardSpeed * playerSettings.backwardSpeedModifier;


                if (IsCrouchStance)
                    targetVelocity *= playerSettings.crouchSpeedModifier;
                if (IsProneStance)
                    targetVelocity *= playerSettings.proneSpeedModifier;
            }

            if (currentWeapon.IsAiming)
            {
                if (IsProneStance)
                    targetVelocity = 0;
                else
                    targetVelocity *= playerSettings.aimingSpeedModifier;
            }
        
            return targetVelocity;
        }

        private void CalculateVerticalVelocity()
        {
            if(IsGrounded)
                return;

            if(math.abs(_verticalVelocity)< terminalVerticalVelocity)
                _verticalVelocity -= playerSettings.gravityValue * Time.fixedDeltaTime;
        }

        private void CalculateStance()
        {
            float targetHeight = playerSettings.GetStanceHeight(_currentStance);
            Vector3 targetCenter = playerSettings.GetStanceCenter(_currentStance);
            Vector3 targetCameraPos = playerSettings.GetStanceCameraPos(_currentStance);

            float currentHeight = characterController.height;
            Vector3 currentCenter = characterController.center;
            Vector3 currentCameraPos = fpsCameraTransform.localPosition;

            if(Math.Abs(currentHeight - targetHeight) < .01)
                return;
        
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * playerSettings.stanceTransitionSmooth);
            currentCenter = Vector3.Lerp(currentCenter, targetCenter, Time.deltaTime * playerSettings.stanceTransitionSmooth);
            currentCameraPos = Vector3.Lerp(currentCameraPos, targetCameraPos,
                Time.deltaTime * playerSettings.stanceTransitionSmooth);
        
            characterController.height = currentHeight;
            characterController.center = currentCenter;
            fpsCameraTransform.localPosition = currentCameraPos;
        }

        private void Jump()
        {
            if(CheckObstaclesOverhead())
                return;
        
            if(IsProneStance)
                return;
            if (IsCrouchStance)
                SetNormalStance();
        
            if(IsGrounded is false)
                return;
        
            _verticalVelocity = Mathf.Sqrt(playerSettings.jumpingHeight * 2f * playerSettings.gravityValue);
            PlayerJump?.Invoke();
        }

        private void ToggleCrouch()
        {
            if (IsCrouchStance)
            {
                if (CheckObstaclesOverhead())
                    return;

                SetNormalStance();
                return;
            }

            if (IsProneStance)
            {
                if (CheckObstaclesOverhead())
                    return;
            
                SetCrouchStance();
                return;
            }
        
            SetCrouchStance();
        }

        private void ToggleProne()
        {
            if (IsStand || IsCrouchStance)
                SetProneStance();
            else
            {
                if(CheckObstaclesOverhead())
                    return;
            
                SetCrouchStance();
            }
        }

        private void SetProneStance() => 
            _currentStance = PlayerSettings.PlayerStance.Prone;

        private void SetNormalStance() => 
            _currentStance = PlayerSettings.PlayerStance.Normal;

        private void SetCrouchStance() => 
            _currentStance = PlayerSettings.PlayerStance.Crouch;

        private bool CheckObstaclesOverhead() => 
            Physics.CheckSphere(fpsCameraTransform.position, checkSphereRadius, obstaclesLayerMask, QueryTriggerInteraction.Ignore);

        private void OnDrawGizmos()
        {
            //Gizmos.DrawSphere(fpsCameraTransform.position, checkSphereRadius);
        }

        private void SetRecoil() => 
            _recoil = currentWeapon.recoil.GetRecoilAmount();
    }
}
