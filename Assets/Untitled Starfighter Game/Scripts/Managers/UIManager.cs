using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public Image[] selectBG;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject wastedPage;
    public GameObject weaponUI;
    public GameObject laserUI;
    public GameObject upgradeUI;
    public GameObject reloadUI;
    public GameObject pauseMenuGUI;
    public GameObject inGameGUI;

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
        if (Input.GetKeyDown(KeyCode.Escape)) {
          if (GameIsPaused)
          {
            Resume();
          }
          else
          {
            Pause();
          }
        }
        else if (Player.IsDeath && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(0);
        }
    }

    private void LateUpdate()
    {
      if(currentTime>0) {
        currentTime -= 1 * Time.deltaTime;
        CountdownTextElement.text = currentTime.ToString ("f1");
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
        Player.OnDeathEvent.AddListener(DisplayLosePage);
        Player.OnSwitchEquipmentEvent.AddListener(UpdateEquipmentUI);
        currentTime = countdownTimeInSeconds;
    }

    public void SetReload()
    {
        //volume.text = "Reloading";
        reloadUI.SetActive(true);
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
        durabilityText.text = curD.ToString();
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

    private void Pause()
    {
      inGameGUI.SetActive(false);
      pauseMenuGUI.SetActive(true);
      Time.timeScale = 0;
      GameIsPaused = true;
    }

    private void Resume()
    {
      pauseMenuGUI.SetActive(false);
      inGameGUI.SetActive(true);
      Time.timeScale = 1;
      GameIsPaused = false;
    }

}
