﻿using System;
using UnityEngine;

namespace Services.Input
{
    public class InputService: MonoBehaviour, IInputService
    {
        public event Action StartFiring;
        public event Action FinishFiring;
        public event Action StartAiming;
        public event Action FinishAiming;
        public event Action StartSprinting;
        public event Action FinishSprinting;
        public event Action Jump;
        public event Action Crouch;
        public event Action Prone;

        private InputActions _inputActions;

        private Vector2 _move;
        private Vector2 _look;

        private void Awake()
        {
            _inputActions = new InputActions();

            _inputActions.Player.Move.performed += e => _move = e.ReadValue<Vector2>();
            _inputActions.Player.Look.performed += e => _look = e.ReadValue<Vector2>();
            _inputActions.Player.Crouch.performed += e => Crouch?.Invoke();
            _inputActions.Player.Prone.performed += e => Prone?.Invoke();
            _inputActions.Player.Jump.performed += e => Jump?.Invoke();
            _inputActions.Player.Sprint.performed += e => StartSprinting?.Invoke();
            _inputActions.Player.Sprint.canceled += e => FinishSprinting?.Invoke();
            
            _inputActions.Weapon.Aim.performed += e => StartAiming?.Invoke();
            _inputActions.Weapon.Aim.canceled += e => FinishAiming?.Invoke();
            _inputActions.Weapon.Fire.performed += e => StartFiring?.Invoke();
            _inputActions.Weapon.Fire.canceled += e => FinishFiring?.Invoke();

            _inputActions.Enable();
        }

        public Vector2 GetMove() => _move;
        public Vector2 GetLook() => _look;
    }
}