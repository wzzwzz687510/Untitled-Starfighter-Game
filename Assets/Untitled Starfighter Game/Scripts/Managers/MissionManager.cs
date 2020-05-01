using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Mission
{
    public Animator animator;
    public bool IsComplete { get; private set; }

    public void AddMission()
    {
        animator.gameObject.SetActive(true);
        animator.SetTrigger("Display");

        AudioManager.Instance.PlayMissionAppearClip();
    }

    public IEnumerator ICompleteMission()
    {
        animator.SetTrigger("Hide");
        IsComplete = true;
        AudioManager.Instance.PlayMissionCompleteClip();
        yield return new WaitForSeconds(1);
        animator.gameObject.SetActive(false);
    }
}

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;

    [Header("Reference")]
    public CapitalShip capitalShip;
    public PlayerSpaceship player;
    public DialogueManager dialogueManager;
    public Transform turretHolder;

    [Header("GUI Elements")]
    public TextMeshProUGUI tFloatingTurretCount;
    public TextMeshProUGUI tEngineTimer;
    public Mission mCollectResource;
    public Mission mDestroySentries;
    public Mission mDestroyDefenders;
    public Mission mFindCapticalShip;
    public Mission mShipDefenses;
    public Mission mShipEngine;
    public Mission mDestroyCapitalShip;

    private float timer = 600;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {
        mCollectResource.AddMission();
        mDestroySentries.AddMission();
        mFindCapticalShip.AddMission();

        foreach (var turret in capitalShip.turrets) {
            turret.OnDestoryedEvent.AddListener(OnCapitalTurretDestroyed);
        }

        capitalShip.engine.OnDestoryedEvent.AddListener(OnDestroyShipEngine);
        capitalShip.OnDestoryedEvent.AddListener(OnDestroyCapitalShip);
    }

    private void FixedUpdate()
    {
        if (!mCollectResource.IsComplete && player.resources > 600) {
            OnReachTargetResource();
        }

        if (!mShipEngine.IsComplete) {
            timer -= Time.deltaTime;
            if (timer < 0) UIManager.Instance.DisplayLosePage();
            tEngineTimer.text = string.Format("<color=#ff0000ff>{0}</color>", timer.ToString("f0"));
        }
    }

    public void OnCapitalTurretDestroyed()
    {
        if(capitalShip.turrets.Length == 0) {
            OnDestroyShipDefenses();
        }
    }

    public void OnSentryDestroyed()
    {
        tFloatingTurretCount.text = string.Format("<color=#ff0000ff>{0}</color>", (turretHolder.childCount - 1).ToString());
        if (turretHolder.childCount == 1) {
            OnDestroyAllSentries();
        }
    }

    public void OnReachTargetResource()
    {
        StartCoroutine(mCollectResource.ICompleteMission());
        dialogueManager.Dialogue_KE_Var2();
    }

    public void OnDestroyAllSentries()
    {
        StartCoroutine(mDestroySentries.ICompleteMission());
        dialogueManager.Dialogue_R3X_Var1();
    }

    public void OnSpawnDefenders()
    {
        mDestroyDefenders.AddMission();
    }

    public void OnDestroyAllDefenders()
    {
        StartCoroutine(mDestroyDefenders.ICompleteMission());
        dialogueManager.Dialogue_R3X_Var2();
    }

    public void OnFindCapitalShip()
    {
        StartCoroutine(mFindCapticalShip.ICompleteMission());
        dialogueManager.Dialogue_SS_Var1();
        mShipDefenses.AddMission();
        mShipEngine.AddMission();
        mDestroyCapitalShip.AddMission();
        timer = 90;
    }

    public void OnDestroyShipDefenses()
    {
        StartCoroutine(mShipDefenses.ICompleteMission());
    }

    public void OnDestroyShipEngine()
    {
        StartCoroutine(mShipEngine.ICompleteMission());
    }

    public void OnDestroyCapitalShip()
    {
        StartCoroutine(mDestroyCapitalShip.ICompleteMission());
        UIManager.Instance.DisplayVictoryPage();
    }
}
