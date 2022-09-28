using System;
using UnityEngine;

namespace Services.Input
{
    public class InputService: MonoBehaviour, IInputService
    {
        public event Action Fire;
        public event Action StartAiming;
        public event Action FinishAiming;
        public event Action StartSprinting;
        public event Action FinishSprinting;
        public event Action<Vector2> Look;
        public event Action Jump;
        public event Action Crouch;
        public event Action Prone;

        private InputActions _inputActions;

        private Vector2 _move; 
        
        private void Awake()
        {
            _inputActions = new InputActions();

            _inputActions.Player.Move.performed += e => _move = e.ReadValue<Vector2>();
            _inputActions.Player.Look.performed += e => Look?.Invoke(e.ReadValue<Vector2>());

            _inputActions.Enable();
        }

        public Vector2 GetMove() => _move;
    }
}