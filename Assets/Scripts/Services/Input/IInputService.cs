using System;
using Infrastructure.Services;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService: IService
    {
        event Action Fire;

        event Action StartAiming;
        event Action FinishAiming;

        event Action StartSprinting;
        event Action FinishSprinting;
        
        event Action<Vector2> Move;
        event Action<Vector2> Look;

        event Action Jump;

        event Action Crouch;
        event Action Prone;
    }
}