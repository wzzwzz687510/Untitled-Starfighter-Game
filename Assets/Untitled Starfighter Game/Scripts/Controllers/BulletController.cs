using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BulletController : MonoBehaviour
{
    public Bullet Template { get; private set; }
    public Vector3 Direction { get; private set; }

    private float timer;
    private LayerMask layerMask;

    public void InitializeBullet(LayerMask mask,Bullet template, Vector3 direction)
    {
        layerMask = mask;
        Template = template;
        timer = template.lifeTime;
        Direction = direction.normalized;
    }

    private void FixedUpdate()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            transform.position += Direction * Template.speed * Time.deltaTime;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(layerMask.ToString()+", "+ other.gameObject.layer.ToString());
        if (layerMask == other.gameObject.layer) return;
        if (other.gameObject.TryGetComponent(out EntityObject eo)) {
            eo.OnShooted((transform.position - other.transform.position).normalized, Template.damage);
            Destroy(gameObject);
        }
    }
}
