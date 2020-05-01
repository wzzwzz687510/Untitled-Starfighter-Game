using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public enum Panel { TmpMain, Main, Options, Graphics, Audio };

    [Header("Event System")]
    public AudioSource highlightSound;
    public AudioSource clickSound;
    public EventSystem eventSystem;
    [Header("Buttons")]
    public Button[] tmpMainButtons;
    public Button[] mainButtons;
    public Button[] optionButtons;
    public Button[] graphicsButtons;
    public Button[] audioButtons;

    private Panel currentPanel;
    private Dictionary<Panel, Button[]> panelDic;
    private Button currentSelectButton;
    private SpaceShipInputActions inputActions;

    public void SetPanel(string targetPanel)
    {
        currentPanel = (Panel)Enum.Parse(typeof(Panel), targetPanel);
        currentSelectButton = panelDic[currentPanel][0];
        currentSelectButton.Select();
    }

    public void OnEnable()
    {
        inputActions.Enable();
    }

    public void OnDisable()
    {
        inputActions.Disable();
    }

    private void Awake()
    {
        var buttons = GetComponentsInChildren<Button>();
        foreach (var button in buttons) {
            button.onClick.AddListener(() => clickSound.Play());
        }

        currentPanel = Panel.TmpMain;
        panelDic = new Dictionary<Panel, Button[]> {
            { Panel.TmpMain, tmpMainButtons },
            { Panel.Main,mainButtons },
            { Panel.Options,optionButtons },
            { Panel.Graphics,graphicsButtons },
            { Panel.Audio,audioButtons }
        };
        eventSystem.firstSelectedGameObject = tmpMainButtons[0].gameObject;

        inputActions = new SpaceShipInputActions();
        inputActions.PlayerControls.Move.performed += ctx => OnPressDPad(ctx.ReadValue<Vector2>());
        inputActions.PlayerControls.DPad.started += ctx => OnPressDPad(ctx.ReadValue<Vector2>());
        inputActions.PlayerControls.Cancel.started += ctx => OnPressCancel();
    }

    private void OnPressDPad(Vector2 value)
    {
        if (!currentSelectButton || currentSelectButton.gameObject != eventSystem.currentSelectedGameObject) {
            currentSelectButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            highlightSound.Play();
        }           
    }

    private void OnPressCancel()
    {
        Button[] buttons = panelDic[currentPanel];
        buttons[buttons.Length - 1].onClick.Invoke();
    }
}
