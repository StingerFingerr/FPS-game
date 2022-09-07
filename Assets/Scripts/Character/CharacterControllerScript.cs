using System;
using Unity.Mathematics;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private CharacterController _characterController;
    public Transform fpsCameraTransform;
    private InputActions _input;
    public PlayerSettings playerSettings;

    private PlayerSettings.PlayerStance _currentStance = PlayerSettings.PlayerStance.Normal;

    private Vector2 _move;
    private Vector2 _look;
    private float verticalRotation;
    private float horizontalRotation;
    private bool _isSprinting;
    private bool _isGrounded;

    private float _verticalVelocity;
    public float terminalVerticalVelocity = 10;

    public LayerMask ObstaclesLayerMask;
    public float checkSphereRadius;
    
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
        _input.Enable();

        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;
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
        horizontalRotation = _look.x * playerSettings.mouseSensitivityX * Time.deltaTime *
                             (playerSettings.mouseInvertedX ? 1 : -1);
        verticalRotation += _look.y * playerSettings.mouseSensitivityY * Time.deltaTime *
                            (playerSettings.mouseInvertedY ? 1 : -1);

        verticalRotation = Mathf.Clamp(verticalRotation, playerSettings.bottomClamp, playerSettings.topClamp);

        fpsCameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up, horizontalRotation);

    }

    private void Move()
    {
        CheckSprinting();

        Vector3 currentMoving = _characterController.velocity;
        currentMoving.y = 0;
        float currentVelocity = currentMoving.magnitude;
        float targetVelocity = CalculateTargetVelocity();


        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity * _move.magnitude, Time.deltaTime * 10);
        Vector3 inputDirection = new Vector3(_move.x, 0, _move.y).normalized;
        Vector3 newDirection = transform.TransformDirection(inputDirection);
        _characterController.Move(newDirection * currentVelocity * Time.deltaTime +
                                  Vector3.up * _verticalVelocity * Time.deltaTime);
    }

    private void CheckSprinting()
    {
        if (_move.y < 0)
            _isSprinting = false;
        if (_currentStance is PlayerSettings.PlayerStance.Prone)
            _isSprinting = false;
    }

    private float CalculateTargetVelocity()
    {
        float targetVelocity = 0f;

        if (_move != Vector2.zero)
        {
            if (math.abs(_move.x) > 0)
                targetVelocity = playerSettings.forwardSpeed * playerSettings.sidewaysSpeedMultiplier;
            
            if (_move.y > 0)
            {
                if(_isSprinting)
                    targetVelocity = playerSettings.sprintSpeed;
                else 
                    targetVelocity = playerSettings.forwardSpeed;

                if (math.abs(_move.x) > 0)
                    targetVelocity *= playerSettings.sidewaysSpeedMultiplier;
            }
            else if (_move.y < 0)
                targetVelocity = playerSettings.forwardSpeed * playerSettings.backwardSpeedMultiplier;


            if (_currentStance is PlayerSettings.PlayerStance.Crouch)
                targetVelocity *= playerSettings.crouchSpeedMultiplier;
            if (_currentStance is PlayerSettings.PlayerStance.Prone)
                targetVelocity *= playerSettings.proneSpeedMultiplier;
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
        return Physics.CheckSphere(fpsCameraTransform.position, checkSphereRadius, ObstaclesLayerMask, QueryTriggerInteraction.Ignore);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(fpsCameraTransform.position, checkSphereRadius);
    }
}
