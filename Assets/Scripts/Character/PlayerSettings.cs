using System;
using UnityEngine;


[CreateAssetMenu(order = 1, fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Look")]
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    public float aimingMouseSensitivityModifier;
    
    public float bottomClamp;
    public float topClamp;

    public bool mouseInvertedX;
    public bool mouseInvertedY;
    
    [Header("Movement")] 
    public float forwardSpeed;
    public float sprintSpeed;

    public float backwardSpeedModifier;
    public float fallingSpeedModifier;
    public float sidewaysSpeedModifier;

    public float crouchSpeedModifier;
    public float proneSpeedModifier;

    public float aimingSpeedModifier;

    [Header("Jumping")] public float jumpingHeight;
    public float gravityValue;

    [Serializable]
    public struct StanceCharacterParameters
    {
        public float Height;
        public Vector3 Center;
        public Vector3 CameraPos;
    }

    public enum PlayerStance
    {
        Normal,
        Crouch,
        Prone
    }

    [Header("Player Stances")] public StanceCharacterParameters normalStance;
    public StanceCharacterParameters crouchStance;
    public StanceCharacterParameters proneStance;

    public float stanceTransitionSmooth;



    public float GetStanceHeight(PlayerStance stance)
    {
        if (stance is PlayerStance.Crouch)
            return crouchStance.Height;
        if (stance is PlayerStance.Prone)
            return proneStance.Height;

        return normalStance.Height;
    }
    public Vector3 GetStanceCenter(PlayerStance stance)
    {
        if (stance is PlayerStance.Crouch)
            return crouchStance.Center;
        if (stance is PlayerStance.Prone)
            return proneStance.Center;

        return normalStance.Center;
    }
    public Vector3 GetStanceCameraPos(PlayerStance stance)
    {
        if (stance is PlayerStance.Crouch)
            return crouchStance.CameraPos;
        if (stance is PlayerStance.Prone)
            return proneStance.CameraPos;

        return normalStance.CameraPos;
    }

}   
