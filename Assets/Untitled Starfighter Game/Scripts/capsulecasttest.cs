using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capsulecasttest : MonoBehaviour
{
    public LayerMask mask;
    public float distance;
    public float radius;

    private void Update()
    {
        var pos1 = transform.position + 0.1f*Vector3.left;
        var pos2 = transform.position - 0.1f * Vector3.left;
        if (Physics.CapsuleCast(pos1, pos2, radius, transform.forward, distance, mask)) Debug.Log(1);
    }

}
