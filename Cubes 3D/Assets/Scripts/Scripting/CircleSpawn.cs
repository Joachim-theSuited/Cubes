using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawn : MonoBehaviour {

    public GameObject[] objects;

    public float radius;
    public float intervall;

    // Use this for initialization
    void Start() {
        InvokeRepeating("Spawn", intervall, intervall);
    }

    void Spawn() {
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector3 position = new Vector3(direction.x, 0, direction.y) * radius;
        GameObject toSpawn = objects[Random.Range(0, objects.Length - 1)];
        Instantiate(toSpawn, position, Quaternion.identity, transform);
    }

}
