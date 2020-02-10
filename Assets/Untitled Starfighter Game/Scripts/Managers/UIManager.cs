using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI durability;
    public TextMeshProUGUI volume;
    public TextMeshProUGUI resource;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject wastedPage;
    public GameObject weaponUI;
    public GameObject laserUI;

    private PlayerSpaceship Player => PlayerSpaceship.MainCharacter;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void Start()
    {
        Player.OnResourceChangedEvent += UpdateResourceUI;
        Player.OnDurabilityChangedEvent += UpdateDurabilityUI;
        Player.OnVolumeChangedEvent += UpdateVolumeUI;
        Player.OnBoundaryEvent.AddListener(UpdateOutsideWarningUI);
        Player.OnDeathEvent.AddListener(DisplayLosePage);
        Player.OnSwitchEquipmentEvent.AddListener(UpdateEquipmentUI);
    }

    public void SetReload()
    {
        volume.text = "Reloading";
    }

    private void HideAllUI()
    {
        outsideWarningPage.SetActive(false);
        wastedPage.SetActive(false);
    }

    private void UpdateEquipmentUI()
    {
        if (weaponUI.activeSelf) {
            weaponUI.SetActive(false);
            laserUI.SetActive(true);
        }
        else {
            weaponUI.SetActive(true);
            laserUI.SetActive(false);
        }
    }

    private void UpdateResourceUI(float value,float useless)
    {
        resource.text = "RESERVE - " + value.ToString();
    }

    private void UpdateDurabilityUI(float curD,float maxD)
    {
        durability.text = curD.ToString();
    }

    private void UpdateVolumeUI(float curV, float maxV)
    {
        volume.text = curV.ToString();
    }

    private void UpdateOutsideWarningUI()
    {
        outsideWarningPage.SetActive(Player.IsOutsideBoundary);
    }

    private void DisplayLosePage()
    {
        HideAllUI();
        wastedPage.SetActive(true);
    }


}
