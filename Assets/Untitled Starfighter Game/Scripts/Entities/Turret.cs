using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ArmedObject
{

    [Header("Rotation Part")]
    public Transform horizontalPart;
    public Transform verticalPart;

    [Header("Turret Parameters")]
    public bool isIsolated;
    public bool isTest;
    public float detectRange = 100;
    public float defaultMaxRotationSpeed = 10;
    public Transform target;

    public bool LockingTarget { get; protected set; }

    protected float horizontalAngle;
    protected float verticalAngle;

    private Vector3 predictionPosition;
    private Vector3 targetDirection;
    private Vector3 aimingDirection;
    private float distance;
    private float deltaAngle;
    private bool lockable;

    public void PrepareShooting()
    {
        if (isTest) {
            targetDirection = (target.position - shootStartPoints.position).normalized;
            deltaAngle = Vector3.Angle(targetDirection, aimingDirection);
            LockingTarget = deltaAngle < 1;
            lockable = Vector3.Dot(targetDirection, transform.up) >= 0;
        }
        else {
            CalculatePredictionPosition();
        }

        if (LockingTarget) {
            base.TryShoot();
        }
        else {
            if (lockable) RotateTurret(); // Try to lock the target.

            // Set each part local rotation
            horizontalPart.localEulerAngles = new Vector3(horizontalPart.localEulerAngles.x, horizontalAngle, horizontalPart.localEulerAngles.z);
            verticalPart.localEulerAngles = new Vector3(verticalAngle, verticalPart.localEulerAngles.y, verticalPart.localEulerAngles.z);
        }
        //Debug.DrawLine(shootStartPoints.position, shootStartPoints.position + 3*aimingDirection);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void InitializeDefaultParameters()
    {
        base.InitializeDefaultParameters();

        aimingDirection = shootStartPoints.forward;
    }

    private void FixedUpdate()
    {
        if (!isIsolated) return;
        CalculateDistanceBetweenTarget();
        if (distance < detectRange) {
            //Debug.Log("Player Enter Attack Zone");
            PrepareShooting();
        }        
    }

    private void CalculateDistanceBetweenTarget()
    {
        distance = Vector3.Distance(shootStartPoints.position, PlayerSpaceship.MainCharacter.transform.position);
    }

    private void CalculatePredictionPosition()
    {
        predictionPosition = PlayerSpaceship.MainCharacter.transform.position +
            PlayerSpaceship.MainCharacter.transform.forward *
            PlayerSpaceship.MainCharacter.Controller.CurrentSpeed *
            (distance / (MainEquipment.Template as Weapon).bullet.speed);

        targetDirection = (predictionPosition - shootStartPoints.position).normalized;
        deltaAngle = Vector3.Angle(targetDirection, aimingDirection);

        LockingTarget = deltaAngle < 1;
        lockable = Vector3.Dot(targetDirection, transform.up) >= 0;
    }

    private void RotateTurret()
    {
        aimingDirection = Vector3.RotateTowards(aimingDirection, targetDirection, defaultMaxRotationSpeed * Time.deltaTime / deltaAngle, 1);
        Vector3 projectV = Vector3.ProjectOnPlane(aimingDirection, transform.up);
        horizontalAngle = (Vector3.Dot(transform.right, aimingDirection) > 0 ? 1 : -1) * Vector3.Angle(transform.forward, projectV);
        verticalAngle = -Vector3.Angle(projectV, aimingDirection);     
    }

    protected override void OnDestoryed()
    {
        base.OnDestoryed();
    }
}
