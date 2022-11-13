using System;
using Infrastructure.Services;
using UnityEngine;

namespace Services.Input
{
    public interface IInputService: IService
    {
        event Action StartFiring;
        event Action FinishFiring;
        event Action SwitchFiringMode;
        event Action Reloading;
        event Action StartAiming;
        event Action FinishAiming;

        event Action StartSprinting;
        event Action FinishSprinting;
        
        event Action Jump;

        event Action Crouch;
        event Action Prone;

        event Action Interact;
        event Action ThrowAway;

        Vector2 GetMove();
        Vector2 GetLook();
    }
}