using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public Slider durability;
    public Text volume;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject wastedPage;

    private PlayerSpaceship Player => PlayerSpaceship.MainCharacter;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public void Start()
    {
        Player.OnDurabilityChangedEvent += UpdateDurabilityUI;
        Player.OnVolumeChangedEvent += UpdateVolumeUI;
        Player.OnBoundaryEvent.AddListener(UpdateOutsideWarningUI);
        Player.OnDeathEvent.AddListener(DisplayLosePage);
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

    private void UpdateDurabilityUI(float curD,float maxD)
    {
        durability.value = curD / maxD;
    }

    private void UpdateVolumeUI(float curV, float maxV)
    {
        volume.text = curV + "/" + maxV;
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
