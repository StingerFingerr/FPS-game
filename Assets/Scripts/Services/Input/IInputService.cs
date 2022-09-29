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
        
        event Action Jump;

        event Action Crouch;
        event Action Prone;

        Vector2 GetMove();
        Vector2 GetLook();
    }
}