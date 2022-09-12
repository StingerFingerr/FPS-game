using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControllerScript : MonoBehaviour
{
    public static UnityEvent onPlayerJump = new();
    public static UnityEvent onPlayerLands = new();
    public static UnityEvent onPlayerFalling = new();
    
    private CharacterController _characterController;
    public Transform fpsCameraTransform;
    private InputActions _input;
    public PlayerSettings playerSettings;

    private PlayerSettings.PlayerStance _currentStance = PlayerSettings.PlayerStance.Normal;

    private Vector2 _move;
    private Vector2 _look;
    private float _verticalRotation;
    private float _horizontalRotation;
    private bool _isSprinting;
    private bool _isGrounded;

    private float _verticalVelocity;
    public float terminalVerticalVelocity = 10;

    public LayerMask obstaclesLayerMask;
    public float checkSphereRadius;

    public Weapon currentWeapon;

    private Vector2 _recoil;
    
    private void Awake()
    {
        _input = new InputActions();
        _input.Player.Move.performed += e => _move = e.ReadValue<Vector2>();
        _input.Player.Look.performed += e => _look = e.ReadValue<Vector2>();
        _input.Player.Sprint.performed += e => _isSprinting = true;
        _input.Player.Sprint.canceled += e => _isSprinting = false;
        _input.Player.Jump.performed += e => Jump();
        _input.Player.Crouch.performed += e => ToggleCrouch();
        _input.Player.Prone.performed += e => ToggleProne();
        
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Weapon.onPlayerShoot.AddListener(e => _recoil = e);
    }

    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        CheckGround();
        CalculateVerticalVelocity();
        Move();
        CalculateStance();
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Look()
    {
        float sensX = playerSettings.mouseSensitivityX;
        float sensY = playerSettings.mouseSensitivityY;

        if (currentWeapon.isAiming)
        {
            sensX *= playerSettings.aimingMouseSensitivityModifier;
            sensY *= playerSettings.aimingMouseSensitivityModifier;
        }
        
        _horizontalRotation = _look.x * sensX * Time.deltaTime *
                             (playerSettings.mouseInvertedX ? 1 : -1);
        _verticalRotation += _look.y * sensY * Time.deltaTime *
                            (playerSettings.mouseInvertedY ? 1 : -1);

        _recoil = Vector2.Lerp(_recoil, Vector2.zero, Time.deltaTime * 10);

        _horizontalRotation += _recoil.x;
        _verticalRotation -= _recoil.y;
        
        _verticalRotation = Mathf.Clamp(_verticalRotation, playerSettings.bottomClamp, playerSettings.topClamp);

        fpsCameraTransform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        transform.Rotate(Vector3.up, _horizontalRotation);

    }

    private void Move()
    {
        CheckSprinting();

        Vector3 currentMoving = _characterController.velocity;
        currentMoving.y = 0;
        float currentVelocity = currentMoving.magnitude;
        float targetVelocity = CalculateTargetVelocity();

        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity * _move.magnitude, Time.deltaTime * 10f);
        Vector3 inputDirection = new Vector3(_move.x, 0, _move.y).normalized;
        Vector3 newDirection = transform.TransformDirection(inputDirection);
        _characterController.Move(newDirection * currentVelocity * Time.deltaTime +
                                  Vector3.up * _verticalVelocity * Time.deltaTime);

        Vector3 newHorizontalVelocity = _characterController.velocity;
        newHorizontalVelocity.y = 0;
        
        currentWeapon.SetCharacterVelocity(newHorizontalVelocity.magnitude / (targetVelocity + .01f));
    }

    private void CheckGround()
    {
        bool newGrounded = _characterController.isGrounded;
        
        if(_verticalVelocity < 0 && newGrounded is false)
            onPlayerFalling.Invoke();

        if(_isGrounded is false && newGrounded)
            onPlayerLands.Invoke();
        
        _isGrounded = newGrounded;
    }
    private void CheckSprinting()
    {
        if (_move.y < 0)
            _isSprinting = false;
        if (_currentStance is PlayerSettings.PlayerStance.Prone)
            _isSprinting = false;
        if (currentWeapon.isAiming)
            _isSprinting = false;
    }

    private float CalculateTargetVelocity()
    {
        float targetVelocity = 0f;

        if (_move != Vector2.zero)
        {
            if (math.abs(_move.x) > 0)
                targetVelocity = playerSettings.forwardSpeed * playerSettings.sidewaysSpeedModifier;
            
            if (_move.y > 0)
            {
                if(_isSprinting)
                    targetVelocity = playerSettings.sprintSpeed;
                else 
                    targetVelocity = playerSettings.forwardSpeed;

                if (math.abs(_move.x) > 0)
                    targetVelocity *= playerSettings.sidewaysSpeedModifier;
            }
            else if (_move.y < 0)
                targetVelocity = playerSettings.forwardSpeed * playerSettings.backwardSpeedModifier;


            if (_currentStance is PlayerSettings.PlayerStance.Crouch)
                targetVelocity *= playerSettings.crouchSpeedModifier;
            if (_currentStance is PlayerSettings.PlayerStance.Prone)
                targetVelocity *= playerSettings.proneSpeedModifier;
        }

        if (currentWeapon.isAiming)
        {
            if (_currentStance == PlayerSettings.PlayerStance.Prone)
                targetVelocity = 0;
            else
                targetVelocity *= playerSettings.aimingSpeedModifier;
        }
        
        return targetVelocity;
    }
    
    private void CalculateVerticalVelocity()
    {
        if(_isGrounded)
            return;

        if(math.abs(_verticalVelocity)< terminalVerticalVelocity)
            _verticalVelocity -= playerSettings.gravityValue * Time.deltaTime;
    }

    private void CalculateStance()
    {
        float targetHeight = playerSettings.GetStanceHeight(_currentStance);
        Vector3 targetCenter = playerSettings.GetStanceCenter(_currentStance);
        Vector3 targetCameraPos = playerSettings.GetStanceCameraPos(_currentStance);

        float currentHeight = _characterController.height;
        Vector3 currentCenter = _characterController.center;
        Vector3 currentCameraPos = fpsCameraTransform.localPosition;

        if(Math.Abs(currentHeight - targetHeight) < .01)
            return;
        
        currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime * playerSettings.stanceTransitionSmooth);
        currentCenter = Vector3.Lerp(currentCenter, targetCenter, Time.deltaTime * playerSettings.stanceTransitionSmooth);
        currentCameraPos = Vector3.Lerp(currentCameraPos, targetCameraPos,
            Time.deltaTime * playerSettings.stanceTransitionSmooth);
        
        _characterController.height = currentHeight;
        _characterController.center = currentCenter;
        fpsCameraTransform.localPosition = currentCameraPos;
    }
    
    private void Jump()
    {
        if(CheckObstaclesOverhead())
            return;
        
        if(_currentStance is PlayerSettings.PlayerStance.Prone)
            return;
        if (_currentStance is PlayerSettings.PlayerStance.Crouch)
            _currentStance = PlayerSettings.PlayerStance.Normal;
        
        if(_isGrounded is false)
            return;
        
        _verticalVelocity = Mathf.Sqrt(playerSettings.jumpingHeight * 2f * playerSettings.gravityValue);
        
        onPlayerJump.Invoke();
    }

    private void ToggleCrouch()
    {
        if (_currentStance is PlayerSettings.PlayerStance.Crouch)
        {
            if (CheckObstaclesOverhead())
                return;

            _currentStance = PlayerSettings.PlayerStance.Normal;
            return;
        }

        if (_currentStance is PlayerSettings.PlayerStance.Prone)
        {
            if (CheckObstaclesOverhead())
                return;
            
            _currentStance = PlayerSettings.PlayerStance.Crouch;
            return;
        }
        
        _currentStance = PlayerSettings.PlayerStance.Crouch;
    }

    private void ToggleProne()
    {
        if (_currentStance is PlayerSettings.PlayerStance.Normal ||
            _currentStance is PlayerSettings.PlayerStance.Crouch)
            _currentStance = PlayerSettings.PlayerStance.Prone;
        else
        {
            if(CheckObstaclesOverhead())
                return;
            
            _currentStance = PlayerSettings.PlayerStance.Crouch;
        }
    }

    private bool CheckObstaclesOverhead()
    {
        return Physics.CheckSphere(fpsCameraTransform.position, checkSphereRadius, obstaclesLayerMask, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(fpsCameraTransform.position, checkSphereRadius);
    }
}
