using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(SpaceShipController))]
public class PlayerSpaceship : Spaceship
{
    public static PlayerSpaceship MainCharacter { get; protected set; }

    public bool IsOutsideBoundary { get; protected set; }

    public UnityEvent OnBoundaryEvent;

    private SpaceShipController m_controller;
    private Rigidbody m_rb;

    private void Awake()
    {
        if (!MainCharacter) MainCharacter = this;
        m_controller = GetComponent<SpaceShipController>();
        m_rb = GetComponent<Rigidbody>();

        InitializeStatus();
    }

    private void FixedUpdate()
    {
        if (IsDeath) return;
        CheckIsInsideBoundary();
    }

    private void CheckIsInsideBoundary()
    {
        bool rt = LevelManager.Instance.CheckIsInsideBoundary(transform.position);
        if (!rt) {
            Vector3 toBoundary = LevelManager.Instance.GetToBoundaryVector(transform.position);
            m_rb.AddForce(100*toBoundary);
            ImpactDurability(-toBoundary.magnitude * Time.deltaTime);
            Debug.Log("Out Boundary");
            if (!IsOutsideBoundary) {
                IsOutsideBoundary = true;
                OnBoundaryEvent?.Invoke();
            }
        }
        else if (IsOutsideBoundary) {
            IsOutsideBoundary = false;
            OnBoundaryEvent?.Invoke();
        }
    }

    protected override void OnDestoryed()
    {
        m_controller.enabled = false;
        base.OnDestoryed();
    }
}
