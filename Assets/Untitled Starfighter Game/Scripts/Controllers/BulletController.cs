using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class BulletController : MonoBehaviour
{
    public Bullet Template { get; private set; }
    public Vector3 Direction { get; private set; }

    private float timer;

    public void InitializeBullet(Bullet template, Vector3 direction)
    {
        Template = template;
        timer = template.lifeTime;
        Direction = direction;
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

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Spaceship>().ImpactDurability(Template.damage);
        Destroy(gameObject);
    }
}
