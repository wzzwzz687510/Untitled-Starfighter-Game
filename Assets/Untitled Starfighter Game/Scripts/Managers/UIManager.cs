using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI durabilityText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI CountdownTextElement;
    public Slider resourceSlider;
    public Toggle[] ammoGroup;
    public Button[] upgradeSlots;
    public EventSystem eventSystem;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject wastedPage;
    public GameObject weaponUI;
    public GameObject laserUI;
    public GameObject upgradeUI;
    public GameObject reloadUI;
    public Canvas pauseMenuGUI;
    public Canvas inGameGUI;

    private float countdownTimeInSeconds = 7.5f;

    public static bool GameIsPaused = false;

    private float currentTime;

    public int SelectID { get; private set; }

    private PlayerSpaceship Player => PlayerSpaceship.MainCharacter;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (GameIsPaused) {
                Resume();
            }
            else {
                Pause();
            }
        }
        else if (Player.IsDestroyed && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    private void LateUpdate()
    {
        if (currentTime > 0) {
            currentTime -= 1 * Time.deltaTime;
            CountdownTextElement.text = currentTime.ToString("f1");
            if (currentTime <= 0) {
                //Do something.
                Debug.Log("COUNTDOWN FINISHED!");
            }
        }
    }

    public void Start()
    {
        Player.OnResourceChangedEvent += UpdateResourceUI;
        Player.OnDurabilityChangedEvent += UpdateDurabilityUI;
        Player.OnVolumeChangedEvent += UpdateVolumeUI;
        Player.OnBoundaryEvent.AddListener(UpdateOutsideWarningUI);
        Player.OnDestoryedEvent.AddListener(DisplayLosePage);
        Player.OnSwitchEquipmentEvent.AddListener(UpdateEquipmentUI);
        currentTime = countdownTimeInSeconds;
    }

    public void SetReload()
    {
        //volume.text = "Reloading";
        reloadUI.SetActive(true);
    }

    public void SetSelectGameObject(GameObject target)
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(target);
    }

    public void DisplayUpgradePage(bool bl)
    {
        upgradeUI.SetActive(bl);
        SetSelectGameObject(upgradeSlots[SelectID].gameObject);
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
        resourceText.text = value.ToString();
        resourceSlider.value = value;
    }

    private void UpdateDurabilityUI(float curD,float maxD)
    {
        durabilityText.text = curD.ToString("f0");
    }

    private void UpdateVolumeUI(float curV, float maxV)
    {
        volumeText.text = curV.ToString();

        for (int i = 0; i < ammoGroup.Length; i++) {
            ammoGroup[i].isOn = i < curV;
        }
        reloadUI.SetActive(false);
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

    public void SetNextID()
    {
        SelectID++;
        if (SelectID > 2)
            SelectID = 0;
        SetSelectGameObject(upgradeSlots[SelectID].gameObject);
    }

    public void SetLastID()
    {
        SelectID--;
        if (SelectID < 0)
            SelectID = 2;
        SetSelectGameObject(upgradeSlots[SelectID].gameObject);
    }

    private void Pause()
    {
      inGameGUI.enabled = false;
      pauseMenuGUI.enabled = true;
      Time.timeScale = 0;
      GameIsPaused = true;
    }

    private void Resume()
    {
      pauseMenuGUI.enabled = false;
      inGameGUI.enabled = true;
      Time.timeScale = 1;
      GameIsPaused = false;
    }

}
