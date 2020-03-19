using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoidSchool : MonoBehaviour
{
    public enum SpawnBoidsType { Sphere, Point };

    [Header("Settings")]
    public Boid prefab;
    public Transform target;

    [Header("Spawn Parameters")]
    public SpawnBoidsType spawnType;
    public bool spawnOnAwake;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public float spawnInterval = 1f;

    public List<Boid> Boids { get; private set; }

    bool initialized;
    UnityAction updateAction;
    BoidSettings settings;

    protected virtual void Awake()
    {
        Boids = new List<Boid>();
    }

    public void RemoveBoid(Boid boid)
    {
        Boids.Remove(boid);
        updateAction?.Invoke();
    }

    public void RegisterUpdateAction(UnityAction action, BoidSettings settings)
    {
        updateAction = action;
        this.settings = settings;

        if (spawnOnAwake) InitializeSchool();
    }

    public void InitializeSchool()
    {
        if (initialized) Clear();

        if (spawnType == SpawnBoidsType.Sphere) SpawnSphereBoids();
        else StartCoroutine(SpawnPointBoids());

        initialized = true;
        updateAction?.Invoke();
    }

    IEnumerator SpawnPointBoids()
    {
        for (int i = 0; i < spawnCount; i++) {
            Boid boid = Instantiate(prefab, transform.position, Quaternion.identity);
            boid.gameObject.name += i;
            boid.GetComponent<EnemyFighter>().RegisterSchool(this);
            boid.Initialize(settings, target);
            Boids.Add(boid);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnSphereBoids()
    {
        for (int i = 0; i < spawnCount; i++) {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            Boid boid = Instantiate(prefab);
            boid.gameObject.name += i;
            boid.GetComponent<EnemyFighter>().RegisterSchool(this);
            boid.transform.position = pos;
            boid.transform.forward = Random.insideUnitSphere;
            boid.Initialize(settings, target);
            Boids.Add(boid);
        }
    }

    void Clear()
    {
        StopAllCoroutines();

        foreach (var boid in Boids) {
            Destroy(boid.gameObject);
        }
        Boids = new List<Boid>();
    }

    void OnDrawGizmosSelected()
    {
        DrawGizmos();
    } 

    void DrawGizmos()
    {
        Color colour = Color.grey;
        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
}
