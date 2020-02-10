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

[RequireComponent(typeof(Spaceship))]
public class SpaceShipController : MonoBehaviour
{
    [Header("Test")]
    public Animator shipAnimator;

    [Header("Spaceship Parameters")]
    [Range(0.001f, 1f)] public float positionLerpTime = 0.2f;
    [Range(0.001f, 1f)] public float rotationLerpTime = 0.01f;
    [Range(0.001f, 1f)] public float viewpointLerpTime = 0.2f;
    public GameObject engineEffects;
    public Transform lookatPoint;
    public Spaceship m_spaceship;

    public float CurrentSpeed { get; private set; }

    public float MaxMovementSpeed => m_spaceship.defaultMaxMovementSpeed;
    public float MaxRotationSpeed => m_spaceship.defaultMaxRotationSpeed;

    SpaceShipInputActions inputActions;

    Vector2 movementInput;
    bool rightStickInput;
    Vector2 viewInput;
    float accelerateInput;
    bool fireInput;

    TransformState m_TargetState;
    //TransformState m_TargetLookatPointState;
    //TransformState m_InterpolatingLookatPointState;

    private void Awake()
    {
        inputActions = new SpaceShipInputActions();
        inputActions.PlayerControls.MoveHorizontal.performed += ctx => { if (!rightStickInput) movementInput.x = ctx.ReadValue<float>(); };
        inputActions.PlayerControls.MoveHorizontal.canceled += ctx => { if (!rightStickInput && movementInput.x!=0) movementInput.x = 0; };
        inputActions.PlayerControls.Move.started += ctx => rightStickInput = true;
        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => { movementInput = Vector2.zero; rightStickInput = false; };
        //inputActions.PlayerControls.Look.performed += ctx => viewInput = ctx.ReadValue<Vector2>();
        //inputActions.PlayerControls.Look.canceled += ctx => viewInput = Vector2.zero;
        inputActions.PlayerControls.Accelerate.performed += ctx => accelerateInput = ctx.ReadValue<float>();
        inputActions.PlayerControls.Accelerate.canceled += ctx => accelerateInput = 0;
        inputActions.PlayerControls.Fire.performed += ctx => fireInput = true;
        inputActions.PlayerControls.Fire.canceled += ctx => fireInput = false;
        inputActions.PlayerControls.Reload.started += ctx => m_spaceship.Reload();
        inputActions.PlayerControls.SwitchEquipment.started += ctx => m_spaceship.SwitchEquipment(ctx.ReadValue<float>() > 0 ? 1 : 0);

        m_TargetState = new TransformState();
        //m_TargetLookatPointState = new TransformState();
        //m_InterpolatingLookatPointState = new TransformState();
    }

    private void FixedUpdate()
    {
        if (fireInput) m_spaceship.Shoot();
        if (accelerateInput == 0 && CurrentSpeed != 0) {
            if (CurrentSpeed > 0) CurrentSpeed = Mathf.Max(0, CurrentSpeed - Time.deltaTime * MaxMovementSpeed);
            else CurrentSpeed = Mathf.Min(0, CurrentSpeed + Time.deltaTime * MaxMovementSpeed);
        }
        //TranslateViewpoint(viewInput);
        RotateSpaceship(movementInput);
        TranslateSpaceship();

        ApplyChanges();
    }

    private void TranslateSpaceship()
    {
        if (accelerateInput == 0) {
            engineEffects.SetActive(false);
            shipAnimator.SetBool("Accelerate", false);
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

    private void RotateSpaceship(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01)
            return;

        m_TargetState.yaw += input.x * MaxRotationSpeed;
        m_TargetState.pitch -= input.y * MaxRotationSpeed;
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
}
