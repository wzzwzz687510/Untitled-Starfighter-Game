using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody),typeof(SpaceShipController))]
public class PlayerSpaceship : Spaceship
{
    public static PlayerSpaceship MainCharacter { get; protected set; }

    [Header("Addon Setting")]
    public Laser defaultLaser;
    public float resources;

    public bool IsOutsideBoundary { get; protected set; }
    public SpaceShipController Controller { get; protected set; }

    public ValueChangeDelegate OnResourceChangedEvent;
    [HideInInspector] public UnityEvent OnBoundaryEvent;

    private Rigidbody m_rb;

    private void Awake()
    {
        if (!MainCharacter) MainCharacter = this;
        Controller = GetComponent<SpaceShipController>();
        m_rb = GetComponent<Rigidbody>();

        InitializeStatus();
        EquipmentObjects.Add(new EquipmentObject(defaultLaser.Hash));
        resources = 0;
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
        Controller.enabled = false;
        base.OnDestoryed();
    }

    protected override GameObject ShootWithLaser(Laser laser)
    {
        GameObject asteroidGO = base.ShootWithLaser(laser);
        if (asteroidGO != null) {
            asteroidGO.GetComponent<Asteroid>().ImpactDurability(-100*Time.deltaTime);
            resources += 100*Time.deltaTime;
            OnResourceChangedEvent?.Invoke((int)resources,0);
        }
        return asteroidGO;
    }
}
