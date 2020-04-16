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

    [Header("Setting")]
    public float outBoundaryAllowBackTime = 7.5f;

    [Header("UI Elements")]
    public TextMeshProUGUI durabilityText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI CountdownTextElement;
    public Slider resourceSlider;
    public Toggle[] ammoGroup;
    public Button[] upgradeSlots;
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
    public GameObject wastedPage;
    public GameObject weaponUI;
    public GameObject laserUI;
    public GameObject upgradeUI;
    public GameObject reloadUI;
    public GameObject cooldownUI;
    public Canvas pauseMenuGUI;
    public Canvas inGameGUI;

    public int SelectID { get; private set; }

    private bool GameIsPaused = false;
    private float outBoundaryTimer;

    private PlayerSpaceship Player => PlayerSpaceship.MainCharacter;

    private void Awake()
    {
        if (!Instance) Instance = this;        
    }

    private void Start()
    {
        BindEvent();
        outBoundaryTimer = outBoundaryAllowBackTime;

        UpdateAllPlayerInfoUI();
    }

    private void UpdateAllPlayerInfoUI()
    {
        UpdateResourceUI(Player.resources,0);
        UpdateDurabilityUI(Player.Durability,Player.MaxDurability);
        UpdateArmourUI(0, 0);
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
        if (outsideWarningPage.activeSelf) {
            outBoundaryTimer = Mathf.Max(0, outBoundaryTimer - Time.deltaTime);
            CountdownTextElement.text = outBoundaryTimer.ToString("f1");
            if (outBoundaryTimer <= 0) {
                //Do something.
                Vector3 toBoundary = LevelManager.Instance.GetToBoundaryVector(Player.transform.position);
                Player.ImpactDurability(-toBoundary.magnitude * Time.deltaTime);
            }
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
        }
        else if (SelectID == 1 && Player.resources >= 250) {
            Player.ImpactResources(-250);
            Player.ImpactDurability(100);
        }
        else if (SelectID == 2 && Player.resources >= 600) {
            Player.ImpactResources(-600);
            var shootPoint = Instantiate(Player.shootStartPoints.GetChild(0), Player.shootStartPoints);
        }
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
        outBoundaryTimer = outBoundaryAllowBackTime;
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

    public void SetSelectID(int id)
    {
        SelectID = id;
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
