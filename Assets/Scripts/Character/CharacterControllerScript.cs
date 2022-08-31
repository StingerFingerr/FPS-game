using System;
using Unity.Mathematics;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private CharacterController _characterController;
    private InputActions _input;
    public PlayerSettings playerSettings;


    private Vector2 _move;
    private Vector2 _look;
    private bool isSprinting;
    private bool isGrounded;

    private float _verticalVelocity;
    public float terminalVerticalVelocity = 10;
    
    private void Awake()
    {
        _input = new InputActions();
        _input.Player.Move.performed += e => _move = e.ReadValue<Vector2>();
        _input.Player.Look.performed += e => _look = e.ReadValue<Vector2>();
        _input.Player.Sprint.performed += e => isSprinting = true;
        _input.Player.Sprint.canceled += e => isSprinting = false;
        _input.Player.Jump.performed += e => Jump();
        _input.Enable();

        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = _characterController.isGrounded;
        CalculateVerticalVelocity();
        Move();
        
    }

    private void Move()
    {
        if (_move.y < 0)
            isSprinting = false;
        
        Vector3 currentMoving = _characterController.velocity;
        currentMoving.y = 0;
        float currentVelocity = currentMoving.magnitude;
        float targetVelocity = CalculateTargetVelocity();


        currentVelocity = Mathf.Lerp(currentVelocity, targetVelocity * _move.magnitude, Time.deltaTime * 10);
        Vector3 inputDirection = new Vector3(_move.x, 0, _move.y).normalized;
        _characterController.Move(inputDirection * currentVelocity * Time.deltaTime + Vector3.up * _verticalVelocity*Time.deltaTime);

    }

    private float CalculateTargetVelocity()
    {
        float targetVelocity = 0f;

        if (_move != Vector2.zero)
        {
            if (_move.y > 0)
            {
                if(isSprinting)
                    targetVelocity = playerSettings.sprintSpeed;
                else 
                    targetVelocity = playerSettings.forwardSpeed;

                if (math.abs(_move.x) > 0)
                    targetVelocity *= playerSettings.sidewaysSpeedMultiplier;
            }
            else if (_move.y < 0)
                targetVelocity = playerSettings.forwardSpeed * playerSettings.backwardSpeedMultiplier;

            if (math.abs(_move.x) > 0)
                targetVelocity = playerSettings.forwardSpeed * playerSettings.sidewaysSpeedMultiplier;
        }

        return targetVelocity;
    }
    
    private void CalculateVerticalVelocity()
    {
        if(isGrounded)
            return;

        if(math.abs(_verticalVelocity)< terminalVerticalVelocity)
            _verticalVelocity -= playerSettings.gravityValue * Time.deltaTime;
    }
    private void Jump()
    {
        if(isGrounded is false)
            return;
        
        _verticalVelocity = Mathf.Sqrt(playerSettings.jumpingHeight * 2f * playerSettings.gravityValue);
    }
}
