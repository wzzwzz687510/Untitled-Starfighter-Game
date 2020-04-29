using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TransformState
{
    public float yaw;
    public float pitch;
    public float roll;
    public float x;
    public float y;
    public float z;

    public void SetFromTransform(Transform t)
    {
        pitch = t.eulerAngles.x;
        yaw = t.eulerAngles.y;
        roll = t.eulerAngles.z;
        x = t.position.x;
        y = t.position.y;
        z = t.position.z;
    }

    public void SetPosition(Vector3 pos)
    {
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }

    public void Translate(Vector3 translation)
    {
        Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

        x += rotatedTranslation.x;
        y += rotatedTranslation.y;
        z += rotatedTranslation.z;
    }

    public void LerpTowards(TransformState target, float positionLerpPct, float rotationLerpPct)
    {
        yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
        pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
        roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

        x = Mathf.Lerp(x, target.x, positionLerpPct);
        y = Mathf.Lerp(y, target.y, positionLerpPct);
        z = Mathf.Lerp(z, target.z, positionLerpPct);
    }

    public void UpdateTransform(Transform t)
    {
        t.eulerAngles = new Vector3(pitch, yaw, roll);
        t.position = new Vector3(x, y, z);
    }
}

[System.Serializable]
public class AimedTarget
{
    public float Timer { get; private set; }

    public AimedTarget(float time = 0)
    {
        Timer = time;
    }

    public void UpdateTimer(float deltaTime)
    {
        Timer += deltaTime;
    }
}

[RequireComponent(typeof(PlayerSpaceship))]
public class SpaceShipController : MonoBehaviour
{
    [Header("Test")]
    public Animator shipAnimator;
    public LayerMask enemyLayer;

    [Header("Spaceship Parameters")]
    [Range(0.001f, 1f)] public float positionLerpTime = 0.2f;
    [Range(0.001f, 1f)] public float rotationLerpTime = 0.01f;
    [Range(0.001f, 1f)] public float viewpointLerpTime = 0.2f;
    [Range(0.001f, 90f)] public float maxRotationAngle = 85f;
    public float dodgeDistance = 10;
    public float dodgeInterval = 1;
    public Transform lookatPoint;
    public PlayerSpaceship m_spaceship;
    public Camera viewCamera;

    [Header("Particles")]
    public GameObject engineEffects;
    public GameObject dodgeEffect;
    public GameObject boostHolder;
    public GameObject trailsHolder;
    public ParticleSystem[] preSpeedUp;

    public float CurrentSpeed { get; private set; }

    public float MaxMovementSpeed => m_spaceship.defaultMaxMovementSpeed;
    public float MaxRotationSpeed => m_spaceship.defaultMaxRotationSpeed;

    SpaceShipInputActions inputActions;

    Vector2 movementInput;
    bool rightStickInput;
    Vector2 viewInput;
    float accelerateInput;
    float BoostInput;
    bool fireInput;
    bool dodgeInput;
    bool switchEquipmentInput;
    bool upgradeInput;
    bool boosting;

    float spaceshipDefaultMaxSpeed;
    float dodgeTimer;

    TransformState m_TargetState;

    Dictionary<int, AimedTarget> aimDic = new Dictionary<int, AimedTarget>();

    private void Awake()
    {
        inputActions = new SpaceShipInputActions();
        inputActions.PlayerControls.MoveHorizontal.performed += ctx => { if (!rightStickInput) movementInput.x = ctx.ReadValue<float>(); };
        inputActions.PlayerControls.MoveHorizontal.canceled += ctx => { if (!rightStickInput && movementInput.x != 0) movementInput.x = 0; };
        inputActions.PlayerControls.Move.started += ctx => rightStickInput = true;
        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => { movementInput = Vector2.zero; rightStickInput = false; };
        inputActions.PlayerControls.Accelerate.performed += ctx => accelerateInput = ctx.ReadValue<float>();
        inputActions.PlayerControls.Accelerate.canceled += ctx => accelerateInput = 0;
        inputActions.PlayerControls.SpeedBoost.performed += ctx => BoostInput = ctx.ReadValue<float>();
        inputActions.PlayerControls.SpeedBoost.canceled += ctx => BoostInput = 0;
        inputActions.PlayerControls.Fire.performed += ctx => fireInput = true;
        inputActions.PlayerControls.Fire.canceled += ctx => fireInput = false;
        inputActions.PlayerControls.Reload.started += ctx => m_spaceship.Reload();
        inputActions.PlayerControls.Upgrade.performed += ctx => { upgradeInput = !UIManager.Instance.upgradeUI.activeSelf; UIManager.Instance.DisplayUpgradePage(upgradeInput); };
        inputActions.PlayerControls.DPad.started += ctx => {
            if (upgradeInput) { 
                if (ctx.ReadValue<Vector2>().x == 1)
                    UIManager.Instance.SetNextID();
                else if (ctx.ReadValue<Vector2>().x == -1)
                    UIManager.Instance.SetLastID();
            }
        };
        inputActions.PlayerControls.SwitchEquipment.performed += ctx => {
            switchEquipmentInput = !switchEquipmentInput;
            m_spaceship.SwitchEquipment(switchEquipmentInput ? 1 : 0);
        };
        inputActions.PlayerControls.Dodge.performed += ctx => { if (!dodgeInput && dodgeTimer<0) StartCoroutine(DodgeAction()); };
        inputActions.PlayerControls.Confirm.performed += ctx => {
            if (upgradeInput) {
                UIManager.Instance.ApplyUpgrade();
            }
        };
        inputActions.PlayerControls.Cancel.performed += ctx => {
            if (upgradeInput) {
                upgradeInput = false;
                UIManager.Instance.upgradeUI.SetActive(upgradeInput);
            }
        };

        m_TargetState = new TransformState();
        dodgeEffect.transform.parent = null;
        spaceshipDefaultMaxSpeed = m_spaceship.defaultMaxMovementSpeed;
        //m_TargetLookatPointState = new TransformState();
        //m_InterpolatingLookatPointState = new TransformState();
    }

    private void FixedUpdate()
    {
        dodgeTimer -= Time.deltaTime;
        DetectLockableTargets();

        if (fireInput) m_spaceship.OnFireInput();
        else m_spaceship.StopLaser();

        if (accelerateInput == 0 && CurrentSpeed != 0) {
            if (CurrentSpeed > 0) CurrentSpeed = Mathf.Max(0, CurrentSpeed - Time.deltaTime * MaxMovementSpeed);
            else CurrentSpeed = Mathf.Min(0, CurrentSpeed + Time.deltaTime * MaxMovementSpeed);
        }

        if (accelerateInput != 0 && BoostInput != 0 && !boosting && m_spaceship.resources != 0) {
            boostHolder.SetActive(false);
            trailsHolder.SetActive(false);
            StopCoroutine(DisplayBoostEffect());
            StartCoroutine(DisplayBoostEffect());
        }
        else if(BoostInput == 0 || m_spaceship.resources == 0) {
            StopBoostEffect();
        }
        if (boosting) {
            m_spaceship.ImpactResources(-10*Time.deltaTime);
        }

        RotateSpaceship(movementInput);
        TranslateSpaceship();

        ApplyChanges();
    }

    private void TranslateSpaceship()
    {
        if (accelerateInput == 0) {
            engineEffects.SetActive(false);
            shipAnimator.SetBool("Accelerate", false);

            StopBoostEffect();
        }
        else {
            engineEffects.SetActive(true);
            shipAnimator.SetBool("Accelerate", true);
        }

        CurrentSpeed = Mathf.Clamp(CurrentSpeed += accelerateInput, -MaxMovementSpeed, MaxMovementSpeed);

        var scaledMoveSpeed = CurrentSpeed * Time.deltaTime;
        m_TargetState.Translate(Vector3.forward * scaledMoveSpeed);
    }

    //private void TranslateViewpoint(Vector2 input)
    //{
    //    var dir = new Vector2(lookatPoint.localPosition.x - input.x, lookatPoint.localPosition.y + input.y);
    //    if (dir.magnitude > 10) dir = dir.normalized * 10;
    //    m_TargetLookatPointState.x = dir.x;
    //    m_TargetLookatPointState.y = dir.y;
    //}

    void StopBoostEffect()
    {
        StopCoroutine(DisplayBoostEffect());
        m_spaceship.defaultMaxMovementSpeed = Mathf.Max(spaceshipDefaultMaxSpeed, MaxMovementSpeed - Time.deltaTime * 100);
        boostHolder.SetActive(false);
        boosting = false;
    }

    IEnumerator DisplayBoostEffect()
    {
        boosting = true;

        boostHolder.SetActive(true);

        foreach (ParticleSystem part in preSpeedUp) {
            part.Play();
        }

        yield return new WaitForSeconds(1);

        m_TargetState.Translate(Vector3.forward * 1);
        m_spaceship.defaultMaxMovementSpeed = 80;

        trailsHolder.SetActive(true);

    }

    private void RotateSpaceship(Vector2 input)
    {
        shipAnimator.SetFloat("Horizontal", input.x);
        if (input.sqrMagnitude < 0.01)
            return;

        m_TargetState.yaw += input.x * MaxRotationSpeed;
        m_TargetState.pitch -= input.y * MaxRotationSpeed;
        m_TargetState.pitch = Mathf.Clamp(m_TargetState.pitch, -maxRotationAngle, maxRotationAngle);
    }

    private void ApplyChanges()
    {
        var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        m_spaceship.UpdateTransformState(m_TargetState, positionLerpPct, rotationLerpPct);

        //var viewPointLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / viewpointLerpTime) * Time.deltaTime);
        //m_InterpolatingLookatPointState.LerpTowards(m_TargetLookatPointState, viewPointLerpPct, 0);
        //lookatPoint.localPosition = new Vector3(m_InterpolatingLookatPointState.x, m_InterpolatingLookatPointState.y, m_InterpolatingLookatPointState.z);
    }

    IEnumerator DodgeAction()
    {
        dodgeInput = true;
        dodgeTimer = dodgeInterval;
        dodgeEffect.transform.position = transform.position;
        dodgeEffect.transform.rotation = transform.rotation;
        dodgeEffect.SetActive(true);
        //float dir = movementInput.x == 0 ? 1 : movementInput.x;
        m_TargetState.Translate(dodgeDistance * movementInput);
        m_spaceship.SetInvincible(true);
        //shipAnimator.SetFloat("AnimSpeed", 1/m_spaceship.dodgeTime);
        shipAnimator.SetTrigger("Dodge");        
        yield return new WaitForSeconds(m_spaceship.dodgeTime);
        //shipAnimator.SetFloat("AnimSpeed", 1);
        m_spaceship.SetInvincible(false);
        dodgeEffect.SetActive(false);
        dodgeInput = false;
    }

    protected virtual void DetectLockableTargets()
    {
        if (!Physics.CheckSphere(transform.position, m_spaceship.defaultLockRange, enemyLayer)) return;

        Ray detectRay = viewCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(detectRay.origin, detectRay.direction, Color.red);
        RaycastHit[] hits = Physics.SphereCastAll(detectRay, m_spaceship.defaultLockSphereRadius, m_spaceship.defaultLockRange, enemyLayer);
        bool hasLockedTarget = false;
        if (hits.Length != 0) {
            var newDic = new Dictionary<int, AimedTarget>();

            //Debug.Log("-------------------------------");
            //foreach (var hit in hits) {              
            //    Debug.Log(hit.collider.gameObject.name);
            //}
            //Debug.Log("-------------------------------");

            foreach (var hit in hits) {
                int hash = hit.collider.gameObject.name.GetStableHashCode();
                if (aimDic.ContainsKey(hash)) {
                    aimDic[hash].UpdateTimer(Time.deltaTime);
                    newDic.Add(hash, new AimedTarget(aimDic[hash].Timer));
                }
                else {
                    newDic.Add(hash, new AimedTarget());
                }
            }
            aimDic.Clear();
            aimDic = newDic;

            float minDistance = m_spaceship.defaultLockRange * m_spaceship.defaultLockRange;
            float targetHash = 0;
            bool hasWeakPoint = false;
            foreach (var hit in hits) {              
                int hash = hit.collider.gameObject.name.GetStableHashCode();
                if (!hit.collider.gameObject.TryGetComponent(out EntityObject eo)) continue;
                if (aimDic[hash].Timer > m_spaceship.defaultLockTime) {
                    if (!hasLockedTarget) {
                        hasLockedTarget = true;
                    }

                    if (hasWeakPoint) {
                        if(!hit.transform.CompareTag("WeakPoint")|| 
                            Vector3.SqrMagnitude(transform.position - hit.point) >= minDistance) continue;
                    }
                    else {
                        if (hit.transform.CompareTag("WeakPoint")) hasWeakPoint = true;
                        else if(Vector3.SqrMagnitude(transform.position - hit.point) >= minDistance) continue;
                    }

                    m_spaceship.SetShootTargetPosition(true, eo);
                    minDistance = Vector3.SqrMagnitude(transform.position - hit.point);
                    targetHash = hash;
                }
            }
        }
        //else {
        //    var colliders = Physics.OverlapSphere(transform.position, m_spaceship.defaultLockSphereRadius, enemyLayer);
        //    if (hits.Length != 0) {
        //        Debug.Log(111);
        //    }
        //}

        if (!hasLockedTarget) {
            m_spaceship.SetShootTargetPosition(false, null);
        }
    }

    public void OnEnable()
    {
        inputActions.Enable();
        m_TargetState.SetFromTransform(transform);
        //m_TargetLookatPointState.SetPosition(lookatPoint.localPosition);
        //m_InterpolatingLookatPointState.SetPosition(lookatPoint.localPosition);
    }

    public void OnDisable()
    {
        inputActions.Disable();
        movementInput = Vector2.zero;
        viewInput = Vector2.zero;
        accelerateInput = 0;
    }

    void OnDrawGizmosSelected()
    {
        DrawGizmos();
    }

    void DrawGizmos()
    {
        Color colour = Color.grey;
        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, m_spaceship.defaultLockSphereRadius);
    }
}
