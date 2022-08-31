using System;
using UnityEngine;


[CreateAssetMenu(order = 1, fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [Header("Movement")]
    public float forwardSpeed;
    public float sprintSpeed;

    public float backwardSpeedMultiplier;
    public float fallingSpeedMultiplier;
    public float sidewaysSpeedMultiplier;

    [Header("Jumping")] 
    public float jumpingHeight;
    public float gravityValue;
}
