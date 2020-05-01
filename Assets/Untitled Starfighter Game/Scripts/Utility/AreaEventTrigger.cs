using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class AreaEventTrigger : MonoBehaviour
{
    public UnityEvent trigger;

    bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered && other.CompareTag("Player")) {
            isTriggered = true;
            trigger?.Invoke();
        }
    }
}
