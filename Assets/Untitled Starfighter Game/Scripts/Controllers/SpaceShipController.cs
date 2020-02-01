using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class SpaceShipController : MonoBehaviour
{
    [Header("Spaceship Parameters")]
    public float moveSpeed = 2;
    public float spinSpeed = 20;
    public GameObject particle;
    public RectTransform radarRT;

    Rigidbody m_rg;
    SpaceShipInputActions inputActions;

    Vector2 movementInput;
    Vector2 viewInput;
    bool accelerateInput;

    private void Awake()
    {
        m_rg = GetComponent<Rigidbody>();
        inputActions = new SpaceShipInputActions();
        inputActions.PlayerControls.Move.started += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Move.canceled += ctx => movementInput = Vector2.zero;
        inputActions.PlayerControls.Look.performed += ctx => viewInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerControls.Accelerate.started += ctx => accelerateInput = true;
        inputActions.PlayerControls.Accelerate.canceled += ctx => accelerateInput = false;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate(movementInput);
    }

    private void Move()
    {
        if (!accelerateInput) {
            particle.SetActive(false);
            return;
        }
            
        particle.SetActive(true);
        var scaledMoveSpeed = moveSpeed * Time.deltaTime;
        transform.position += transform.forward * scaledMoveSpeed;
    }

    private void Rotate(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01)
            return;
        float yaw = -input.y * spinSpeed+ transform.rotation.eulerAngles.x;
        float pitch = input.x * spinSpeed + transform.rotation.eulerAngles.y;
        Debug.Log(yaw + ", " + pitch);
        transform.rotation = Quaternion.Euler(yaw, pitch, 0);
        radarRT.rotation = Quaternion.Euler(0, 0, -transform.rotation.eulerAngles.y);
    }

    public void OnEnable()
    {
        inputActions.Enable();
    }

    public void OnDisable()
    {
        inputActions.Disable();
    }
}
