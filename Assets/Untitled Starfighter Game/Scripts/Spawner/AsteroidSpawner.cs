using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public static Transform AsteroidHolder { get; protected set; }

    [Header("Parameters")]
    public float minLeafSize;
    public float maxLeafSize;
    public int count = 5;
    public float minDurability = 50;
    public float maxDurability = 150;
    public Vector3 size = Vector3.one;

    [Header("Gameobject")]
    public Vector3 scale;
    public GameObject[] prefab;


    private void Awake()
    {
        if (!AsteroidHolder) AsteroidHolder = new GameObject("asteroid holder").transform;

        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        Queue<Leaf> leaves = new Queue<Leaf>();
        List<Leaf> splitedSpace = new List<Leaf>();
        leaves.Enqueue(new Leaf(transform.position, size, minLeafSize, maxLeafSize));
        Leaf origin;

        for (int i = 0; i < count;) {
            origin = leaves.Dequeue();
            if (origin.Split()) {               
                i++;
                leaves.Enqueue(origin.boy);
                leaves.Enqueue(origin.girl);
            }
            else {
                splitedSpace.Add(origin);
            }
        }

        int cnt = leaves.Count;
        for (int i = 0; i < cnt; i++) {
            splitedSpace.Add(leaves.Dequeue());
        }

        for (int i = 0; i < splitedSpace.Count; i++) {
            GameObject go = Instantiate(prefab[Random.Range(0, prefab.Length - 1)], splitedSpace[i].pos, Random.rotation, AsteroidHolder);
            go.transform.localScale = new Vector3(splitedSpace[i].size.x * scale.x, splitedSpace[i].size.y * scale.y, splitedSpace[i].size.z * scale.z);
            go.GetComponent<Asteroid>().Initialize(Random.Range(minDurability, maxDurability));
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 0, 0.4f);
        Gizmos.DrawCube(transform.position, size);
    }
}

public class Leaf
{
    public Vector3 size;
    public Vector3 pos;
    public Leaf boy, girl;

    private float minLeafSize;
    private float maxLeafSize;

    public Leaf(Vector3 position, Vector3 size, float minSize, float maxSize)
    {
        this.size = size;
        this.pos = position;
        minLeafSize = minSize;
        maxLeafSize = maxSize;
    }

    public bool Split()
    {
        
        if (boy != null || girl != null)
            return false; // Already splited, abort.

        bool splitX = Random.Range(0, 100) > 50;
        bool splitY = Random.Range(0, 100) > 50;
        if (size.x > Mathf.Max(size.y,size.z) * 1.25f) {
            splitX = true;
            splitY = false;
        }
        else if(size.y > Mathf.Max(size.x, size.z) * 1.25f) {
            splitX = false;
            splitY = true;
        }
        else if (size.z > Mathf.Max(size.x, size.y) * 1.25f) {
            splitX = false;
            splitY = false;
        }

        float maxLength = (splitX ? size.x : (splitY ? size.y : size.z)) - minLeafSize;
        if (maxLength < minLeafSize) return false; // Too small to split any more...

        float split = Random.Range(minLeafSize, maxLength);

        if (splitX) {
            float delta = (split - size.x) / 2;
            boy = new Leaf(new Vector3(pos.x + delta, pos.y, pos.z), new Vector3(split, size.y, size.z), minLeafSize, maxLeafSize);
            girl = new Leaf(new Vector3(pos.x - delta, pos.y, pos.z), new Vector3(size.x - split, size.y, size.z), minLeafSize, maxLeafSize);
        }
        else if (splitY) {
            float delta = (split - size.y) / 2;
            boy = new Leaf(new Vector3(pos.x, pos.y + delta, pos.z), new Vector3(size.x, split, size.z), minLeafSize, maxLeafSize);
            girl = new Leaf(new Vector3(pos.x, pos.y - delta, pos.z), new Vector3(size.x, size.y - split, size.z), minLeafSize, maxLeafSize);
        }
        else {
            float delta = (split - size.z) / 2;
            boy = new Leaf(new Vector3(pos.x, pos.y, pos.z + delta), new Vector3(size.x, size.y, split), minLeafSize, maxLeafSize);
            girl = new Leaf(new Vector3(pos.x, pos.y, pos.z - delta), new Vector3(size.x, size.y, size.z - split), minLeafSize, maxLeafSize);
        }
        return true;
    }
}