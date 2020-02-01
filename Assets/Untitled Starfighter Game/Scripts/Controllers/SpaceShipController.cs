using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipState
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

    public void Translate(Vector3 translation)
    {
        Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

        x += rotatedTranslation.x;
        y += rotatedTranslation.y;
        z += rotatedTranslation.z;
    }

    public void LerpTowards(SpaceshipState target, float positionLerpPct, float rotationLerpPct)
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

[RequireComponent(typeof(Rigidbody),typeof(Spaceship))]
public class SpaceShipController : MonoBehaviour
{
    [Header("Spaceship Parameters")]
    public float moveSpeed = 2;
    public float spinSpeed = 20;
    [Range(0.001f, 1f)] public float positionLerpTime = 0.2f;
    [Range(0.001f, 1f)] public float rotationLerpTime = 0.01f;
    public GameObject particle;
    public RectTransform radarRT;

    Rigidbody m_rg;
    SpaceShipInputActions inputActions;

    Vector2 movementInput;
    Vector2 viewInput;
    bool accelerateInput;

    SpaceshipState m_TargetState = new SpaceshipState();
    SpaceshipState m_InterpolatingState = new SpaceshipState();

    private void Awake()
    {
        m_rg = GetComponent<Rigidbody>();
        inputActions = new SpaceShipInputActions();
        inputActions.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;
        inputActions.PlayerControls.Look.performed += ctx => viewInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Look.canceled += ctx => viewInput = Vector2.zero;
        inputActions.PlayerControls.Accelerate.performed += ctx => accelerateInput = true;
        inputActions.PlayerControls.Accelerate.canceled += ctx => accelerateInput = false;
    }

    private void FixedUpdate()
    {
        Rotate(movementInput);
        Move();

        ApplyChanges();
    }

    private void Move()
    {
        if (!accelerateInput) {
            particle.SetActive(false);
            return;
        }
            
        particle.SetActive(true);
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        m_TargetState.Translate(-Vector3.forward * scaledMoveSpeed);
    }

    private void Rotate(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01)
            return;

        m_TargetState.yaw += input.x * spinSpeed;
        m_TargetState.pitch += input.y * spinSpeed;
    }

    private void ApplyChanges()
    {
        var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        m_InterpolatingState.LerpTowards(m_TargetState, positionLerpPct, rotationLerpPct);

        m_InterpolatingState.UpdateTransform(transform);
    }

    public void OnEnable()
    {
        inputActions.Enable();
        m_TargetState.SetFromTransform(transform);
        m_InterpolatingState.SetFromTransform(transform);
    }

    public void OnDisable()
    {
        inputActions.Disable();
    }
}
