using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI durability;
    public TextMeshProUGUI volume;
    public TextMeshProUGUI resource;
    public Image[] selectBG;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject wastedPage;
    public GameObject weaponUI;
    public GameObject laserUI;
    public GameObject upgradeUI;

    public int SelectID { get; private set; }

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

    private void ChangeSelectBG()
    {
        foreach (var bg in selectBG) {
            bg.enabled = false;
        }

        selectBG[SelectID].enabled = true;
    }

    public void SetNextID()
    {
        SelectID++;
        if (SelectID > 2)
            SelectID = 0;
        ChangeSelectBG();
    }

    public void SetLastID()
    {
        SelectID--;
        if (SelectID < 0)
            SelectID = 2;
        ChangeSelectBG();
    }
}
