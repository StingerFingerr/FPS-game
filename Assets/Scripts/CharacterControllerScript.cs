using System;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    private CharacterController _characterController;
    private InputActions _input;
    public PlayerSettings playerSettings;


    public Vector2 _move;
    private Vector2 _look;
    public bool isSprinting;
    
    
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
        Move();
    }

    private void Move()
    {
        Vector3 currentMoving = _characterController.velocity;
        currentMoving.y = 0;
        float currentSpeed = currentMoving.magnitude;
        float targetSpeed;

        if (_move == Vector2.zero)
            targetSpeed = 0f;
        else
            targetSpeed = 10f;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed * _move.magnitude, Time.deltaTime * 10);

        Vector3 inputDirection = new Vector3(_move.x, 0, _move.y).normalized;

        _characterController.Move(inputDirection * currentSpeed * Time.deltaTime);

    }
    
    private void Jump()
    {
        
    }
}
