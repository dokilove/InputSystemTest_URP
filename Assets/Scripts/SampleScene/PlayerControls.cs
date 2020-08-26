// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""b114d4f0-b62c-4254-bb22-7b0815507569"",
            ""actions"": [
                {
                    ""name"": ""Grow"",
                    ""type"": ""Button"",
                    ""id"": ""b00d6ee6-0aeb-45dd-947e-ad5ee898dd2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""21d22144-8882-46a9-ab99-5a9180a4e130"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""05238dfa-41b8-4498-adf1-57cb730b7bd7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateY"",
                    ""type"": ""Button"",
                    ""id"": ""efd9ceaf-01d1-48f6-81f0-df4361ea6289"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetCam"",
                    ""type"": ""Button"",
                    ""id"": ""0f355728-517b-43b3-af09-6c599f1efa51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Target"",
                    ""type"": ""Button"",
                    ""id"": ""93c0cc86-bb5c-4758-b083-bc033f262624"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeMap"",
                    ""type"": ""Button"",
                    ""id"": ""37b4e236-0f8f-414e-b4a3-f8eae22f516b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""59492ad1-0d77-4dff-9250-315398fb5f2b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e09ccf20-da04-4952-88ff-8812995ffbd5"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc8c2021-0342-4358-8995-bc643a82fe32"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f20c11b3-0301-436a-beb4-287582f3051b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fe2b6ea-a2ec-4e34-90bf-3d190d407d42"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""af4c7552-9104-4910-9271-399508dd1685"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Target"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc1320a1-4d0b-403d-8b49-caacbbd85998"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Map"",
            ""id"": ""505fcb18-8169-4ad2-8e2f-28c8ca80e432"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f374c0ca-b180-4cb2-b89b-e267c221436d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeMap"",
                    ""type"": ""Button"",
                    ""id"": ""20f585d8-590b-4214-9b4b-264ef63c5b69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a44b323b-e6b2-4d1c-8246-2a48f46ccaf0"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1af5e0f3-49f8-418b-8dbe-925494478b80"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_Grow = m_GamePlay.FindAction("Grow", throwIfNotFound: true);
        m_GamePlay_Move = m_GamePlay.FindAction("Move", throwIfNotFound: true);
        m_GamePlay_Rotate = m_GamePlay.FindAction("Rotate", throwIfNotFound: true);
        m_GamePlay_RotateY = m_GamePlay.FindAction("RotateY", throwIfNotFound: true);
        m_GamePlay_ResetCam = m_GamePlay.FindAction("ResetCam", throwIfNotFound: true);
        m_GamePlay_Target = m_GamePlay.FindAction("Target", throwIfNotFound: true);
        m_GamePlay_ChangeMap = m_GamePlay.FindAction("ChangeMap", throwIfNotFound: true);
        // Map
        m_Map = asset.FindActionMap("Map", throwIfNotFound: true);
        m_Map_Move = m_Map.FindAction("Move", throwIfNotFound: true);
        m_Map_ChangeMap = m_Map.FindAction("ChangeMap", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_Grow;
    private readonly InputAction m_GamePlay_Move;
    private readonly InputAction m_GamePlay_Rotate;
    private readonly InputAction m_GamePlay_RotateY;
    private readonly InputAction m_GamePlay_ResetCam;
    private readonly InputAction m_GamePlay_Target;
    private readonly InputAction m_GamePlay_ChangeMap;
    public struct GamePlayActions
    {
        private @PlayerControls m_Wrapper;
        public GamePlayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Grow => m_Wrapper.m_GamePlay_Grow;
        public InputAction @Move => m_Wrapper.m_GamePlay_Move;
        public InputAction @Rotate => m_Wrapper.m_GamePlay_Rotate;
        public InputAction @RotateY => m_Wrapper.m_GamePlay_RotateY;
        public InputAction @ResetCam => m_Wrapper.m_GamePlay_ResetCam;
        public InputAction @Target => m_Wrapper.m_GamePlay_Target;
        public InputAction @ChangeMap => m_Wrapper.m_GamePlay_ChangeMap;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @Grow.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGrow;
                @Grow.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGrow;
                @Grow.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnGrow;
                @Move.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotate;
                @RotateY.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotateY;
                @RotateY.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotateY;
                @RotateY.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnRotateY;
                @ResetCam.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnResetCam;
                @ResetCam.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnResetCam;
                @ResetCam.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnResetCam;
                @Target.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTarget;
                @Target.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTarget;
                @Target.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnTarget;
                @ChangeMap.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeMap;
                @ChangeMap.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeMap;
                @ChangeMap.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeMap;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Grow.started += instance.OnGrow;
                @Grow.performed += instance.OnGrow;
                @Grow.canceled += instance.OnGrow;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @RotateY.started += instance.OnRotateY;
                @RotateY.performed += instance.OnRotateY;
                @RotateY.canceled += instance.OnRotateY;
                @ResetCam.started += instance.OnResetCam;
                @ResetCam.performed += instance.OnResetCam;
                @ResetCam.canceled += instance.OnResetCam;
                @Target.started += instance.OnTarget;
                @Target.performed += instance.OnTarget;
                @Target.canceled += instance.OnTarget;
                @ChangeMap.started += instance.OnChangeMap;
                @ChangeMap.performed += instance.OnChangeMap;
                @ChangeMap.canceled += instance.OnChangeMap;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);

    // Map
    private readonly InputActionMap m_Map;
    private IMapActions m_MapActionsCallbackInterface;
    private readonly InputAction m_Map_Move;
    private readonly InputAction m_Map_ChangeMap;
    public struct MapActions
    {
        private @PlayerControls m_Wrapper;
        public MapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Map_Move;
        public InputAction @ChangeMap => m_Wrapper.m_Map_ChangeMap;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void SetCallbacks(IMapActions instance)
        {
            if (m_Wrapper.m_MapActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnMove;
                @ChangeMap.started -= m_Wrapper.m_MapActionsCallbackInterface.OnChangeMap;
                @ChangeMap.performed -= m_Wrapper.m_MapActionsCallbackInterface.OnChangeMap;
                @ChangeMap.canceled -= m_Wrapper.m_MapActionsCallbackInterface.OnChangeMap;
            }
            m_Wrapper.m_MapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @ChangeMap.started += instance.OnChangeMap;
                @ChangeMap.performed += instance.OnChangeMap;
                @ChangeMap.canceled += instance.OnChangeMap;
            }
        }
    }
    public MapActions @Map => new MapActions(this);
    public interface IGamePlayActions
    {
        void OnGrow(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnRotateY(InputAction.CallbackContext context);
        void OnResetCam(InputAction.CallbackContext context);
        void OnTarget(InputAction.CallbackContext context);
        void OnChangeMap(InputAction.CallbackContext context);
    }
    public interface IMapActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnChangeMap(InputAction.CallbackContext context);
    }
}
