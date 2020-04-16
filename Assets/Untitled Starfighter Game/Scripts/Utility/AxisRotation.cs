using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisRotation : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Vector3 offset;

    [Tooltip("Vector Up as Axis")]
    public Transform axisTransform;

    private void Update()
    {
        if (axisTransform) {
            transform.RotateAround(axisTransform.position, axisTransform.up, Time.deltaTime * speed);
        }
        else {
            transform.RotateAround(transform.position + offset, direction, Time.deltaTime * speed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + offset, direction);
    }
}
