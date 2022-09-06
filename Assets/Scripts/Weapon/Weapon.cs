using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private InputActions _input;
    public WeaponSettings settings;

    public Vector3 hipPosition;
    public Vector3 aimPosition;

    private bool _isAiming;
    private Quaternion _originRotation;
    private Vector2 _playerLook;
    
    private void Awake()
    {
        _input = new InputActions();
        _input.Player.Look.performed += e => _playerLook = e.ReadValue<Vector2>();
        _input.Weapon.Aim.performed += e => _isAiming = true;
        _input.Weapon.Aim.canceled += e => _isAiming = false;
        _input.Enable();

        Cursor.visible = false;

        _originRotation = transform.localRotation;
    }

    private void Update()
    {
        CalculateSway();
        CalculateAiming();
    }

    private void CalculateSway()
    {
        Quaternion rotationX =
            Quaternion.AngleAxis(_playerLook.x * settings.swayIntensityX * (settings.swayInvertedX ? -1 : 1), Vector3.up);
        Quaternion rotationY =
            Quaternion.AngleAxis(_playerLook.y * settings.swayIntensityY * (settings.swayInvertedY ? -1 : 1), Vector3.right);
        
        Quaternion targetRotation = _originRotation * rotationX * rotationY;
        
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * settings.swaySmooth);
    }
    private void CalculateAiming()
    {
        if (_isAiming)
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * settings.aimingTime);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * settings.aimingTime);
    }
    
    
}
