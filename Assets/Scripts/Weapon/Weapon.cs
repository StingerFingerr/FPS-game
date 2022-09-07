using System;
using Unity.Mathematics;
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
    private Vector2 _playerMove;

    private void Awake()
    {
        _input = new InputActions();
        _input.Player.Look.performed += e => _playerLook = e.ReadValue<Vector2>();
        _input.Player.Move.performed += e => _playerMove = e.ReadValue<Vector2>();
        _input.Weapon.Aim.performed += e => _isAiming = true;
        _input.Weapon.Aim.canceled += e => _isAiming = false;

        Cursor.visible = false;

        _originRotation = transform.localRotation;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        CalculateSway();
        CalculateMovementSway();
        CalculateAiming();
    }

    private void CalculateSway()
    {
        Quaternion rotationX =
            Quaternion.AngleAxis(_playerLook.x * settings.swayIntensityX * (settings.swayInvertedX ? -1 : 1),
                Vector3.up);
        Quaternion rotationY =
            Quaternion.AngleAxis(_playerLook.y * settings.swayIntensityY * (settings.swayInvertedY ? -1 : 1),
                Vector3.right);

        rotationX.y = Math.Clamp(rotationX.y, -settings.swayClampX, settings.swayClampX);
        rotationY.x = Math.Clamp(rotationY.x, -settings.swayClampY, settings.swayClampY);

        Quaternion targetRotation = _originRotation * rotationX * rotationY;

        transform.localRotation =
            Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * settings.swaySmooth);
    }

    private void CalculateMovementSway()
    {
        Quaternion rotationX = Quaternion.AngleAxis(_playerMove.y * settings.movementSwayIntensity, Vector3.right);
        Quaternion rotationZ = Quaternion.AngleAxis(_playerMove.x * settings.movementSwayIntensity, Vector3.forward);

        rotationX.x = Math.Clamp(rotationX.x, -settings.movementSwayClampY, settings.movementSwayClampY);
        rotationZ.z = Math.Clamp(rotationZ.z, -settings.movementSwayClampX, settings.movementSwayClampX);

        Quaternion targetRotation = _originRotation * rotationX * rotationZ;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation,
            Time.deltaTime * settings.movementSwaySmooth);
        
    }

    private void CalculateAiming()
    {
        if (_isAiming)
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * settings.aimingTime);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * settings.aimingTime);
    }
    
    
}
