using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(order = 1, fileName = "gun_Settings", menuName = "ScriptableObjects/WeaponSettings")]
public class WeaponSettings : ScriptableObject
{
    public string name;
    public float aimingTime;

    public float swayIntensityX;
    public float swayIntensityY;
    public bool swayInvertedX;
    public bool swayInvertedY;
    public float swaySmooth;
    
}
