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

    private void FixedUpdate()
    {
//        if (Input.GetKeyDown(KeyCode.Escape)) {  
//#if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#else
//            Application.Quit();
//#endif
//        }
    }

    public bool CheckIsInsideBoundary(Vector3 position)
    {
        return (position-transform.position).magnitude < boundaryRadius + Mathf.Epsilon;
    }

    public Vector3 GetToBoundaryVector(Vector3 position)
    {
        return (position - transform.position).normalized * boundaryRadius - position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.4f);
        Gizmos.DrawSphere(transform.position, boundaryRadius);
    }
}
