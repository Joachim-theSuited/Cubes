using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCenter : MonoBehaviour {

    public float speed = .5f;

    void Update() {
        Vector2 planePos = new Vector2(transform.position.x, transform.position.z);
        float deltaSpeed = speed * Time.deltaTime;

        if(planePos.magnitude < 1) {
            Destroy(gameObject);
        } else if(planePos.magnitude < deltaSpeed) {
            transform.position = new Vector3(0, transform.position.y, 0);
        } else {
            planePos = planePos.normalized;
            transform.position -= new Vector3(planePos.x, 0, planePos.y) * deltaSpeed;
        }
    }
}
