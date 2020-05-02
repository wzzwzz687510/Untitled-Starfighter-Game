using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Cinemachine.PostFX;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Setting")]
    public float outBoundaryAllowBackTime = 7.5f;
    public float normalHealthIntensity = 0.15f;
    public float inDangerHealthIntensity = 0.255f;
    public Color normalHealthColour;
    public Color inDangerHealthColour;

    [Header("UI Elements")]
    public TextMeshProUGUI durabilityText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI CountdownTextElement;
    public Slider resourceSlider;
    public Button restartButtonLose;
    public Button restartButtonVictory;
    public Button pauseButton;
    public Toggle[] ammoGroup;
    public Button[] upgradeSlots;
    public CinemachineVolumeSettings cinemachineVolumeSettings;
    public EventSystem eventSystem;

    [Header("Laser Elements")]
    public Slider laserHeatBar;
    public Image laserHeatBarFill;
    public Color normalColour;
    public Color overheatColour;


    [Header("Armour Slot")]
    public Toggle armourSlotPrefab;
    public Transform topArmourSlotHolder;
    public Transform bottomArmourSlotHolder;
    public Transform leftArmourSlotHolder;
    public Transform rightArmourSlotHolder;

    private int armourCount;
    private Toggle[] topArmourSlots;
    private Toggle[] bottomArmourSlots;
    private Toggle[] leftArmourSlots;
    private Toggle[] rightArmourSlots;

    [Header("Pages")]
    public GameObject outsideWarningPage;
    public GameObject defeatedPage;
    public GameObject victoryPage;
    public GameObject weaponUI;
    public GameObject laserUI;
    public GameObject upgradeUI;
    public GameObject reloadUI;
    public GameObject cooldownUI;
    public Canvas pauseMenuGUI;
    public Canvas inGameGUI;

    [Header("Scene Management")]
    public GameObject loadingScreen;
    public TextMeshProUGUI progressText;
    public GameObject loseEndScreen;
    public GameObject winEndScreen;
    public GameObject pauseScreen;

    public int SelectID { get; private set; }

    private bool GameIsPaused = false;
    private bool middleDamage;
    private bool fatalDamage;
    private float outBoundaryTimer;
    private Vignette vignetteRef;

    private PlayerSpaceship Player => PlayerSpaceship.MainCharacter;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {
        BindEvent();
        outBoundaryTimer = outBoundaryAllowBackTime;
        cinemachineVolumeSettings.m_Profile.TryGet(out vignetteRef);

        UpdateAllPlayerInfoUI();
    }

    private void UpdateAllPlayerInfoUI()
    {
        UpdateResourceUI(Player.resources,0);
        UpdateDurabilityUI(Player.Durability,Player.MaxDurability);
        UpdateArmourUI(0, 0);
    }

    private void LateUpdate()
    {
        if (outsideWarningPage.activeSelf) {
            // inGameGUI.enabled = false;
            outBoundaryTimer = Mathf.Max(0, outBoundaryTimer - Time.deltaTime);
            CountdownTextElement.text = outBoundaryTimer.ToString("f1");
            if (outBoundaryTimer <= 0) {
                //Do something.
                Vector3 toBoundary = LevelManager.Instance.GetToBoundaryVector(Player.transform.position);
                Player.ImpactDurability(-toBoundary.magnitude * Time.deltaTime);
            }
        }
        // else
        // {
        //   inGameGUI.enabled = true;
        // }
    }

    public void OnMenuButtonPressed()
    {
        if (GameIsPaused) {
            Resume();
        }
        else if (inGameGUI.enabled) {
            Pause();
        }
    }

    public void BindEvent()
    {
        Player.OnResourceChangedEvent += UpdateResourceUI;
        Player.OnDurabilityChangedEvent += UpdateDurabilityUI;
        Player.OnVolumeChangedEvent += UpdateVolumeUI;
        Player.OnAmourChangeEvent += UpdateArmourUI;
        Player.OnSpaceShipOutBoundaryEvent.AddListener(UpdateOutsideWarningUI);
        Player.OnDestoryedEvent.AddListener(DisplayLosePage);
        Player.OnEquipmentSwitchedEvent.AddListener(UpdateEquipmentUI);
        Player.OnLaserHeatChangedEvent.AddListener(UpdateLaserHeatBar);
    }

    public void SetReload()
    {
        if (weaponUI.activeSelf) {
            reloadUI.SetActive(true);
        }
        else {

            cooldownUI.SetActive(Player.LaserStun);
        }
    }

    public void SetSelectGameObject(GameObject target)
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(target);
    }

    public void ApplyUpgrade()
    {
        if (SelectID == 0 && Player.resources >= 450) {
            Player.ImpactResources(-450);
            Player.UpgradeArmour();
            MissionManager.Instance.dialogueManager.Dialogue_AW_Var1();
            AudioManager.Instance.PlayUpgradeClip();
        }
        else if (SelectID == 1 && Player.resources >= 250) {
            Player.ImpactResources(-250);
            Player.ImpactDurability(100);
            AudioManager.Instance.PlayUpgradeClip();
        }
        else if (SelectID == 2 && Player.resources >= 600) {
            Player.ImpactResources(-600);
            var shootPoint = Instantiate(Player.shootStartPoints.GetChild(0), Player.shootStartPoints);
            AudioManager.Instance.PlayUpgradeClip();
        }
    }

    public void DisplayUpgradePage(bool bl)
    {
        upgradeUI.SetActive(bl);
        SetSelectGameObject(upgradeSlots[SelectID].gameObject);
    }

    private void HideAllUI()
    {
        inGameGUI.enabled = false;
        outsideWarningPage.SetActive(false);
        defeatedPage.SetActive(false);
        victoryPage.SetActive(false);
    }

    private void AddArmourSlots()
    {
        int deltaNumber = Mathf.FloorToInt(Player.MaxArmour - armourCount);
        armourCount += deltaNumber;
        for (int i = 0; i < deltaNumber; i++) {
            Instantiate(armourSlotPrefab, topArmourSlotHolder);
            Instantiate(armourSlotPrefab, bottomArmourSlotHolder);
            Instantiate(armourSlotPrefab, leftArmourSlotHolder);
            Instantiate(armourSlotPrefab, rightArmourSlotHolder);
        }

        topArmourSlots = topArmourSlotHolder.GetComponentsInChildren<Toggle>();
        bottomArmourSlots = bottomArmourSlotHolder.GetComponentsInChildren<Toggle>();
        leftArmourSlots = leftArmourSlotHolder.GetComponentsInChildren<Toggle>();
        rightArmourSlots = rightArmourSlotHolder.GetComponentsInChildren<Toggle>();
    }

    private void UpdateArmourUI(float op,float op2)
    {
        if (armourCount < Player.MaxArmour) {
            AddArmourSlots();
        }

        for (int i = 0; i < armourCount; i++) {
            topArmourSlots[i].isOn = i < Player.TopArmour;
            bottomArmourSlots[i].isOn = i < Player.BottomArmour;
            leftArmourSlots[i].isOn = i < Player.LeftArmour;
            rightArmourSlots[i].isOn = i < Player.RightArmour;
        }
    }

    private void UpdateLaserHeatBar()
    {
        laserHeatBar.value = Player.LaserHeat / Player.laserLimitHeat;
        laserHeatBarFill.color = Player.LaserStun ? overheatColour : normalColour;
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

    private void UpdateDurabilityUI(float curD, float maxD)
    {
        durabilityText.text = curD.ToString("f0");
        if (vignetteRef) {
            vignetteRef.color.value = Color.Lerp(inDangerHealthColour, normalHealthColour, curD / 100.0f);
            vignetteRef.intensity.value = Mathf.Lerp(inDangerHealthIntensity, normalHealthIntensity, curD / 100.0f);
        }
        if (!middleDamage && curD < 80) {
            middleDamage = true;
            MissionManager.Instance.dialogueManager.Dialogue_BB_Var1();
        }
        if (!fatalDamage && curD < 20) {
            fatalDamage = true;
            MissionManager.Instance.dialogueManager.Dialogue_KE_Var1();
        }
    }

    private void UpdateVolumeUI(float curV, float maxV)
    {
        volumeText.text = curV.ToString();

        for (int i = 0; i < ammoGroup.Length; i++) {
            ammoGroup[i].isOn = i < curV;
        }
        if (reloadUI.activeSelf) {
            AudioManager.Instance.PlayReloadCompleteClip();
            reloadUI.SetActive(false);
        }
    }

    private void UpdateOutsideWarningUI()
    {
        outsideWarningPage.SetActive(Player.IsOutsideBoundary);
        outBoundaryTimer = outBoundaryAllowBackTime;
    }

    public void DisplayLosePage()
    {
        HideAllUI();
        defeatedPage.SetActive(true);
        restartButtonLose.Select();
        Player.SetKinematic();
        AudioManager.Instance.PlayGameoverClip();
    }

    public void DisplayVictoryPage()
    {
        HideAllUI();
        victoryPage.SetActive(true);
        restartButtonVictory.Select();
        Player.SetKinematic();
        AudioManager.Instance.PlayVictoryBGM();
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

    public void SetSelectID(int id)
    {
        SelectID = id;
        SetSelectGameObject(upgradeSlots[SelectID].gameObject);
    }

    public void Pause()
    {
        inGameGUI.enabled = false;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        pauseButton.Select();

        outsideWarningPage.SetActive(false);
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        inGameGUI.enabled = true;
        Time.timeScale = 1;
        GameIsPaused = false;
        eventSystem.SetSelectedGameObject(null);

        if (Player.IsOutsideBoundary) outsideWarningPage.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void MainMenu(int sceneIndex)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadAsync(sceneIndex));
    }

    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        loseEndScreen.SetActive(false);
        winEndScreen.SetActive(false);
        pauseScreen.SetActive(false);
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //functionalityElement.SetActive(false);
        loadingScreen.SetActive(true);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;
        }
    }

}
