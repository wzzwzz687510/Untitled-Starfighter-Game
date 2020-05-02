// GENERATED AUTOMATICALLY FROM 'Assets/Untitled Starfighter Game/Scripts/Controllers/SpaceShipInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @SpaceShipInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @SpaceShipInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""SpaceShipInputActions"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""2d4348fa-5059-49c2-93f4-fbc0d033118f"",
            ""actions"": [
                {
                    ""name"": ""MoveHorizontal"",
                    ""type"": ""Button"",
                    ""id"": ""4b3fa7a0-af92-446b-bf22-f851bc2e2986"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""3803b7f6-087a-4598-b2cf-ea1d8eeb9ffa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""da4cfab8-4d9a-48d5-a63b-2d6e61662b52"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""269bccaf-a3b4-4e1e-a888-e2e5df58cc93"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Upgrade"",
                    ""type"": ""Button"",
                    ""id"": ""2e61edd7-fb4a-4859-ae1d-eb170c02f142"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""8653fbb4-2551-4dfa-806c-8280f04048b5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""dc733cc4-2769-4f55-92fb-b2e0dde09b7f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""D-Pad"",
                    ""type"": ""Button"",
                    ""id"": ""9539f1e8-893e-42bf-a9a3-1528a30aded2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""ccfc7b3e-57ad-4996-aa02-854fb7dcae21"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""a5956dfb-8f23-4b90-84ea-3d9cc2e6f56a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpeedBoost"",
                    ""type"": ""Button"",
                    ""id"": ""d0754c16-57eb-4c1d-b5e6-f494f2a122ad"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchEquipment"",
                    ""type"": ""Button"",
                    ""id"": ""f3541d25-4230-482d-bae5-a030a7282326"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""f2286658-43e3-4e0f-a40d-52336e262a57"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e642e1ff-ff6b-4434-a11e-c5866d794662"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""706a4b9c-0d61-4f61-af9f-dde4cb0b236f"",
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
                    ""id"": ""4e7a3818-9ba1-4bd3-b402-28e9cc9c8596"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""330425ee-4000-4140-ac09-13ce204c8135"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6a999ce9-78fe-48b1-be53-5fdc96e29382"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aa725362-4ad9-4702-9a11-bd31173531d6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""37c762cf-1f84-4019-b75b-7c9dfb1e350d"",
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
                    ""id"": ""1e186847-6d0e-4efa-815b-9fe5860adafd"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf913fda-6367-478c-8ac6-e374b7e24638"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""361490bb-0ae2-48ca-9f3c-d5462a44c928"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8bda2c6c-fcbc-4597-bfe3-e95f5ab4d36d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3c0c9578-eec7-4d0d-b6c2-20a965f7fae0"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6699409c-eb11-4529-bdc4-82ca16a65f0e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfe06a3b-eea6-41f2-9b14-200900f1cca6"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42e6204e-f843-4095-a9a5-0f7dd4ce12dc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ec3223e-8ca2-4a33-b9f5-150951ef4dcd"",
                    ""path"": ""<XInputController>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6691b1af-1130-4d64-b95a-210fbee1d1de"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Upgrade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""34c06b9d-fbf0-4c46-aa76-196ac4374d5b"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Upgrade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3ef9a46-8cf2-401f-b75d-a28c32b057a1"",
                    ""path"": ""<XInputController>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""38c77723-5db5-476b-8d5b-44cb19f63ccd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f471a56b-63b1-4f42-af57-c324ca330e66"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c4c56159-0954-4f16-b6a0-c6921ed9ea4a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5280217d-da78-49a3-be08-ec53791df2e0"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43636c38-c2f4-40cd-af55-d02ffd5134b2"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""debb8ec2-5b0e-4d2e-b3c7-6060e79c4a94"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""446a1c30-99ef-47ca-b74c-b2d0b32e3d26"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea085b19-9fec-4908-b93a-f904764abec4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f398f9a9-fe6f-4d89-91f8-bfe7e95aad05"",
                    ""path"": ""<XInputController>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""D-Pad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""c90eddd9-d30f-4dec-a38b-2ec2d1474dbc"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""D-Pad"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""57610541-d814-49e5-ba2a-da233782032b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""D-Pad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d3fdd4e8-e075-4687-975e-21cf719fd722"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""D-Pad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""32ddcb6a-b472-4499-8727-61bbf918867e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""D-Pad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e794d6af-0b13-42ee-8409-ec235b5ba336"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""D-Pad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""25e776e0-2402-4637-8fa6-61b417ef79d2"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d9403b7-adfe-4f2e-a7b6-320fe7731dff"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9d31996-80d3-4845-8015-d27bff576d44"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aa159d67-0011-439b-ae5c-6b3008856f07"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b78099bb-182c-4984-90d6-ef1abbc5ecbb"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SpeedBoost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c494f158-c6e3-465e-9aea-b04f87af9e21"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SpeedBoost"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41e09fec-1461-435a-9d6c-d41aee1bd13f"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwitchEquipment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ab13fc48-a17a-48f9-b9e6-dbb545811230"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchEquipment"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a2ee0f60-cf78-4568-a49f-3079e6c3f9b0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SwitchEquipment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ba306438-1dfc-46a6-904e-d88e250d38ea"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""SwitchEquipment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3c88596b-d1ec-4a85-8823-c18ebb3bd468"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e07172ab-bc1b-42c4-bd53-8351b4dc483c"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player Controls
        m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
        m_PlayerControls_MoveHorizontal = m_PlayerControls.FindAction("MoveHorizontal", throwIfNotFound: true);
        m_PlayerControls_Move = m_PlayerControls.FindAction("Move", throwIfNotFound: true);
        m_PlayerControls_Fire = m_PlayerControls.FindAction("Fire", throwIfNotFound: true);
        m_PlayerControls_Accelerate = m_PlayerControls.FindAction("Accelerate", throwIfNotFound: true);
        m_PlayerControls_Upgrade = m_PlayerControls.FindAction("Upgrade", throwIfNotFound: true);
        m_PlayerControls_Dodge = m_PlayerControls.FindAction("Dodge", throwIfNotFound: true);
        m_PlayerControls_Reload = m_PlayerControls.FindAction("Reload", throwIfNotFound: true);
        m_PlayerControls_DPad = m_PlayerControls.FindAction("D-Pad", throwIfNotFound: true);
        m_PlayerControls_Confirm = m_PlayerControls.FindAction("Confirm", throwIfNotFound: true);
        m_PlayerControls_Cancel = m_PlayerControls.FindAction("Cancel", throwIfNotFound: true);
        m_PlayerControls_SpeedBoost = m_PlayerControls.FindAction("SpeedBoost", throwIfNotFound: true);
        m_PlayerControls_SwitchEquipment = m_PlayerControls.FindAction("SwitchEquipment", throwIfNotFound: true);
        m_PlayerControls_Menu = m_PlayerControls.FindAction("Menu", throwIfNotFound: true);
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

    // Player Controls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_MoveHorizontal;
    private readonly InputAction m_PlayerControls_Move;
    private readonly InputAction m_PlayerControls_Fire;
    private readonly InputAction m_PlayerControls_Accelerate;
    private readonly InputAction m_PlayerControls_Upgrade;
    private readonly InputAction m_PlayerControls_Dodge;
    private readonly InputAction m_PlayerControls_Reload;
    private readonly InputAction m_PlayerControls_DPad;
    private readonly InputAction m_PlayerControls_Confirm;
    private readonly InputAction m_PlayerControls_Cancel;
    private readonly InputAction m_PlayerControls_SpeedBoost;
    private readonly InputAction m_PlayerControls_SwitchEquipment;
    private readonly InputAction m_PlayerControls_Menu;
    public struct PlayerControlsActions
    {
        private @SpaceShipInputActions m_Wrapper;
        public PlayerControlsActions(@SpaceShipInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveHorizontal => m_Wrapper.m_PlayerControls_MoveHorizontal;
        public InputAction @Move => m_Wrapper.m_PlayerControls_Move;
        public InputAction @Fire => m_Wrapper.m_PlayerControls_Fire;
        public InputAction @Accelerate => m_Wrapper.m_PlayerControls_Accelerate;
        public InputAction @Upgrade => m_Wrapper.m_PlayerControls_Upgrade;
        public InputAction @Dodge => m_Wrapper.m_PlayerControls_Dodge;
        public InputAction @Reload => m_Wrapper.m_PlayerControls_Reload;
        public InputAction @DPad => m_Wrapper.m_PlayerControls_DPad;
        public InputAction @Confirm => m_Wrapper.m_PlayerControls_Confirm;
        public InputAction @Cancel => m_Wrapper.m_PlayerControls_Cancel;
        public InputAction @SpeedBoost => m_Wrapper.m_PlayerControls_SpeedBoost;
        public InputAction @SwitchEquipment => m_Wrapper.m_PlayerControls_SwitchEquipment;
        public InputAction @Menu => m_Wrapper.m_PlayerControls_Menu;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @MoveHorizontal.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMoveHorizontal;
                @Move.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMove;
                @Fire.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnFire;
                @Accelerate.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnAccelerate;
                @Upgrade.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUpgrade;
                @Upgrade.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUpgrade;
                @Upgrade.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnUpgrade;
                @Dodge.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDodge;
                @Reload.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnReload;
                @DPad.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDPad;
                @DPad.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDPad;
                @DPad.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnDPad;
                @Confirm.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnConfirm;
                @Cancel.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCancel;
                @SpeedBoost.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpeedBoost;
                @SpeedBoost.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpeedBoost;
                @SpeedBoost.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSpeedBoost;
                @SwitchEquipment.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchEquipment;
                @SwitchEquipment.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchEquipment;
                @SwitchEquipment.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSwitchEquipment;
                @Menu.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMenu;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveHorizontal.started += instance.OnMoveHorizontal;
                @MoveHorizontal.performed += instance.OnMoveHorizontal;
                @MoveHorizontal.canceled += instance.OnMoveHorizontal;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @Upgrade.started += instance.OnUpgrade;
                @Upgrade.performed += instance.OnUpgrade;
                @Upgrade.canceled += instance.OnUpgrade;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @DPad.started += instance.OnDPad;
                @DPad.performed += instance.OnDPad;
                @DPad.canceled += instance.OnDPad;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @SpeedBoost.started += instance.OnSpeedBoost;
                @SpeedBoost.performed += instance.OnSpeedBoost;
                @SpeedBoost.canceled += instance.OnSpeedBoost;
                @SwitchEquipment.started += instance.OnSwitchEquipment;
                @SwitchEquipment.performed += instance.OnSwitchEquipment;
                @SwitchEquipment.canceled += instance.OnSwitchEquipment;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnMoveHorizontal(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnAccelerate(InputAction.CallbackContext context);
        void OnUpgrade(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnDPad(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnSpeedBoost(InputAction.CallbackContext context);
        void OnSwitchEquipment(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
    }
}
