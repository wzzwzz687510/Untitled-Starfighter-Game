using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Setting")]
    public float boundaryRadius = 100;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    public bool CheckIsInsideBoundary(Vector3 position)
    {
        return position.magnitude < boundaryRadius + Mathf.Epsilon;
    }

    public Vector3 GetToBoundaryVector(Vector3 position)
    {
        return position.normalized * boundaryRadius - position;
    }
}
