using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider durability;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject wastedPage;

    private PlayerSpaceship Player => PlayerSpaceship.MainCharacter;

    public void Start()
    {
        Player.OnDurabilityChangedEvent += UpdateDurabilityUI;
        Player.OnBoundaryEvent.AddListener(UpdateOutsideWarningUI);
        Player.DestroyEvent.AddListener(DisplayLosePage);
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
