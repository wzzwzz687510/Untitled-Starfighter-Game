using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct VisualEffects
{
    public GameObject destoryEffect;
}

[RequireComponent(typeof(Collider),typeof(Rigidbody))]
public class Spaceship : ArmedObject
{
    [Header("SpaceShip Parameters")]
    public VisualEffects particleEffects;
    public float defaultMaxMovementSpeed = 1;
    public float defaultAcrMovementSpeed = 1;
    public float defaultMaxRotationSpeed = 10;
    public float defaultAcrRotationSpeed = 1;
    public float defaultLockRange = 50;
    public float defaultLockTime = 0.2f;
    public float defaultLockSphereRadius = 2;

    public TransformState State { get; protected set; }

    public delegate void SpaceshipDelegate(Spaceship id);

    protected override void InitializeDefaultParameters()
    {
        base.InitializeDefaultParameters();

        State = new TransformState();
        State.SetFromTransform(transform);
    }

    public virtual void UpdateTransformState(TransformState target, float positionLerpPct, float rotationLerpPct)
    {
        State.LerpTowards(target, positionLerpPct, rotationLerpPct);

        State.UpdateTransform(transform);
    }
}
