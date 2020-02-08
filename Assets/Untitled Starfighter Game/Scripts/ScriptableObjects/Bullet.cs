using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "USG/ScriptableObjects/Bullet", order = 2)]
public class Bullet : ScriptableObject
{
    public float damage;
    public float speed;
    public float lifeTime;
}
