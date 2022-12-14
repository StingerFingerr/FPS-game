//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Input/InputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""f4e5828b-df31-44d5-bdcf-34c3839454d9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ddc93450-6a88-4a70-b78c-bb3e2a167234"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2d32bf93-a78f-48a6-b9e2-6348fb90386a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""953e6d97-0a9b-45dd-97c2-4acf940b8cc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""4a9335a2-8daa-4e61-aa35-22df19673891"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""4cdf2de7-827a-4e70-9925-ec7a5a59eeda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Prone"",
                    ""type"": ""Button"",
                    ""id"": ""358c0dda-c303-45a9-95d4-932b83054b16"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""26bc11e7-20ca-4b36-b87f-97135a2e1c79"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5407498-6849-42be-9ed9-88b142c0e3c8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12c5ffb1-7ca6-46bf-ba57-43ae431bba64"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6ca80eb-dc11-4c28-b7ed-c937aa42e02d"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dbe3b1c-94bd-4fc4-9c98-59c6af9cf564"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dd3de66-9ccf-4eca-b04a-8d93e33fadb4"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Prone"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""631ea439-3546-494f-9c16-3ed7da966288"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""313d0324-a0a8-4d37-a6c0-5f3262e0c1f2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b713a376-589c-4293-9ab8-679ebc1605dc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f563b701-7c05-4fc3-adc2-127993bce5f4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e940c9da-3b98-4532-ba6e-2fed518a2dec"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""ff52747d-fe1d-4bd8-9e48-5c168677e9ab"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""11aad2a5-f04c-4350-9f98-665366cc7462"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""4ef5bacf-19a0-45c3-90ac-df13f84ad2f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchFiringMode"",
                    ""type"": ""Button"",
                    ""id"": ""73e19754-6b01-4ec4-adbe-167617796048"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reloading"",
                    ""type"": ""Button"",
                    ""id"": ""c05ff79b-c059-43be-b0ba-4eba2b787217"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""30d34879-a092-4622-933d-2aacdd7ca6f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThrowAway"",
                    ""type"": ""Button"",
                    ""id"": ""caac86d2-1f7b-4d6d-b44b-918b1967f5e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollForward"",
                    ""type"": ""Value"",
                    ""id"": ""2ab924e5-f515-4ced-888d-e9496c909cae"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8869f6ad-4fd3-4ad8-af5f-f240bba36279"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c86a750-d8da-4386-8fc9-ba03644b4a88"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a677519c-f02c-46fd-bfce-079a4c735cc6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb0c9174-d654-443a-bbb0-eb2c0524f4be"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchFiringMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""912508fe-1fd0-4f3c-b5c7-e67e6548c75e"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reloading"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a99e8d2-927b-42ea-a693-f0a4906f4587"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51149f15-9493-4948-845d-0c527e1c9a13"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowAway"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8f50748-32ec-4a07-9c66-e86ed7d76716"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Sprint = m_Player.FindAction("Sprint", throwIfNotFound: true);
        m_Player_Crouch = m_Player.FindAction("Crouch", throwIfNotFound: true);
        m_Player_Prone = m_Player.FindAction("Prone", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_Aim = m_Weapon.FindAction("Aim", throwIfNotFound: true);
        m_Weapon_Fire = m_Weapon.FindAction("Fire", throwIfNotFound: true);
        m_Weapon_SwitchFiringMode = m_Weapon.FindAction("SwitchFiringMode", throwIfNotFound: true);
        m_Weapon_Reloading = m_Weapon.FindAction("Reloading", throwIfNotFound: true);
        m_Weapon_Interact = m_Weapon.FindAction("Interact", throwIfNotFound: true);
        m_Weapon_ThrowAway = m_Weapon.FindAction("ThrowAway", throwIfNotFound: true);
        m_Weapon_ScrollForward = m_Weapon.FindAction("ScrollForward", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Sprint;
    private readonly InputAction m_Player_Crouch;
    private readonly InputAction m_Player_Prone;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Sprint => m_Wrapper.m_Player_Sprint;
        public InputAction @Crouch => m_Wrapper.m_Player_Crouch;
        public InputAction @Prone => m_Wrapper.m_Player_Prone;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSprint;
                @Crouch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCrouch;
                @Prone.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProne;
                @Prone.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProne;
                @Prone.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnProne;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Prone.started += instance.OnProne;
                @Prone.performed += instance.OnProne;
                @Prone.canceled += instance.OnProne;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_Aim;
    private readonly InputAction m_Weapon_Fire;
    private readonly InputAction m_Weapon_SwitchFiringMode;
    private readonly InputAction m_Weapon_Reloading;
    private readonly InputAction m_Weapon_Interact;
    private readonly InputAction m_Weapon_ThrowAway;
    private readonly InputAction m_Weapon_ScrollForward;
    public struct WeaponActions
    {
        private @InputActions m_Wrapper;
        public WeaponActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Aim => m_Wrapper.m_Weapon_Aim;
        public InputAction @Fire => m_Wrapper.m_Weapon_Fire;
        public InputAction @SwitchFiringMode => m_Wrapper.m_Weapon_SwitchFiringMode;
        public InputAction @Reloading => m_Wrapper.m_Weapon_Reloading;
        public InputAction @Interact => m_Wrapper.m_Weapon_Interact;
        public InputAction @ThrowAway => m_Wrapper.m_Weapon_ThrowAway;
        public InputAction @ScrollForward => m_Wrapper.m_Weapon_ScrollForward;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @Aim.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnAim;
                @Fire.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire;
                @SwitchFiringMode.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSwitchFiringMode;
                @SwitchFiringMode.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSwitchFiringMode;
                @SwitchFiringMode.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnSwitchFiringMode;
                @Reloading.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReloading;
                @Reloading.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReloading;
                @Reloading.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReloading;
                @Interact.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnInteract;
                @ThrowAway.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowAway;
                @ThrowAway.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowAway;
                @ThrowAway.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnThrowAway;
                @ScrollForward.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScrollForward;
                @ScrollForward.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScrollForward;
                @ScrollForward.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScrollForward;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @SwitchFiringMode.started += instance.OnSwitchFiringMode;
                @SwitchFiringMode.performed += instance.OnSwitchFiringMode;
                @SwitchFiringMode.canceled += instance.OnSwitchFiringMode;
                @Reloading.started += instance.OnReloading;
                @Reloading.performed += instance.OnReloading;
                @Reloading.canceled += instance.OnReloading;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @ThrowAway.started += instance.OnThrowAway;
                @ThrowAway.performed += instance.OnThrowAway;
                @ThrowAway.canceled += instance.OnThrowAway;
                @ScrollForward.started += instance.OnScrollForward;
                @ScrollForward.performed += instance.OnScrollForward;
                @ScrollForward.canceled += instance.OnScrollForward;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnProne(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnAim(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnSwitchFiringMode(InputAction.CallbackContext context);
        void OnReloading(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnThrowAway(InputAction.CallbackContext context);
        void OnScrollForward(InputAction.CallbackContext context);
    }
}
