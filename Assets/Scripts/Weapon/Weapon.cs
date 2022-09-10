using System;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public static UnityEvent<bool> onPlayerAiming = new();
    
    private InputActions _input;
    public WeaponSettings settings;

    public Vector3 hipPosition;
    public Vector3 aimPosition;

    public bool isAiming { get; private set; }
    private Quaternion _originRotation;

    private bool _isSprinting;
    private bool _isJumping;
    private bool _isFalling;
    
    private Vector2 _playerLook;
    private Vector2 _playerMove;
    private float _characterVelocity;

    private void Awake()
    {
        _input = new InputActions();
        _input.Player.Look.performed += e => _playerLook = e.ReadValue<Vector2>();
        _input.Player.Move.performed += e => _playerMove = e.ReadValue<Vector2>();
        _input.Player.Sprint.performed += e => _isSprinting = !isAiming;
        _input.Player.Sprint.canceled += e => _isSprinting = false;
        _input.Weapon.Aim.performed += e => StartAiming();
        _input.Weapon.Aim.canceled += e => FinishAiming();

        Cursor.visible = false;

        _originRotation = transform.localRotation;
    }

    private void Start()
    {
        CharacterControllerScript.onPlayerJump.AddListener(SetJumpingAnimation);
        CharacterControllerScript.onPlayerLands.AddListener(SetLandingAnimation);
        CharacterControllerScript.onPlayerFalling.AddListener(SetFallingAnimation);
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
        SetAnimations();
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
        if (isAiming)
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * settings.aimingTime);
        else
            transform.localPosition = Vector3.Lerp(transform.localPosition, hipPosition, Time.deltaTime * settings.aimingTime);
    }

    private void SetAnimations()
    {
        if (_isJumping)
            return;

        if (isAiming)
        {
            _animator.speed = 0;
            if(_animator.speed < .05f)
                _animator.enabled = false;  
            
            _animator.transform.localPosition =
                Vector3.Lerp(_animator.transform.localPosition, Vector3.zero, Time.deltaTime * settings.aimingTime);
            _animator.transform.localRotation = Quaternion.Lerp(_animator.transform.localRotation,
                Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * settings.aimingTime);
            return;
        }
        
        _animator.enabled = true;
        
        if (_playerMove == Vector2.zero)
        {
            _animator.speed = 1;
            _animator.SetBool("isIdle", true);
            return;
        }

        _animator.SetBool("isIdle", false);
        _animator.SetBool("isSprinting", _isSprinting);
        _animator.speed = Mathf.Lerp(_animator.speed, _characterVelocity, Time.deltaTime * 10f);
    }

    private void SetJumpingAnimation()
    {
        _isJumping = true;

        _animator.speed = 1;
        _animator.SetTrigger("isJumping");
    }

    private void SetFallingAnimation()
    {
        if(_isFalling)
            return;

        _isFalling = true;

        _animator.speed = 1;
        _animator.SetTrigger("isFalling");
    }
    
    private void SetLandingAnimation()
    {
        _isJumping = false;
        _isFalling = false;
        _animator.speed = 1;

        _animator.SetTrigger("isLanding");
    }

    private void StartAiming()
    {
        if(_isSprinting)
            return;
        if(_isFalling)
            return;
          
        isAiming = true;
        onPlayerAiming.Invoke(isAiming);
    }

    private void FinishAiming()
    {
        isAiming = false;
        onPlayerAiming.Invoke(isAiming);
    }
    
    public void SetCharacterVelocity(float normVelocity)
    {
        _characterVelocity = normVelocity;
    }

    
        
    

}
