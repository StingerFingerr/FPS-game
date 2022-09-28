using System;
using UnityEngine;

namespace Services.Input
{
    public class InputService: IInputService
    {
        public event Action Fire;
        public event Action StartAiming;
        public event Action FinishAiming;
        public event Action StartSprinting;
        public event Action FinishSprinting;
        public event Action<Vector2> Move;
        public event Action<Vector2> Look;
        public event Action Jump;
        public event Action Crouch;
        public event Action Prone;

        public InputService()
        {
            var inputActions = new InputActions();
            
            inputActions.Player.Move.performed += 
                e => Move?.Invoke(e.ReadValue<Vector2>());
            //inputActions.Player.Look.performed +=
                //e => Look?.Invoke(e.ReadValue<Vector2>());
            
            inputActions.Enable();
        }
    }
}