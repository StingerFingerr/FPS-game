using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "gun_Settings", menuName = "ScriptableObjects/WeaponSettings")]
public class WeaponSettings : ScriptableObject
{
    public string name;
    public float aimingTime = 10;

    [Header("Sway")]
    public float swayIntensityX = .5f;
    public float swayIntensityY = .5f;
    public float aimingSwayIntensityModifier = .5f;
    public bool swayInvertedX = true;
    public bool swayInvertedY;
    public float swayClampX = .15f;
    public float swayClampY = .15f;
    public float swaySmooth = 10f;

    [Header("Movement Sway")] 
    public float movementSwayIntensity = 10f;
    public float movementSwayClampX = 20f;
    public float movementSwayClampY = 20f;
    public float movementSwaySmooth = 3f;


}
