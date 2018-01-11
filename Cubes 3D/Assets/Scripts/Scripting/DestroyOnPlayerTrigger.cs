using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DestroyOnPlayerTrigger : MonoBehaviour {

    public bool onEnter;
    public bool onExit;

    public GameObject objectToDestroy;

    void OnTriggerEnter(Collider other) {
        if(onEnter && other.tag == Tags.Player)
            Destroy(objectToDestroy);
    }

    void OnTriggerExit(Collider other) {
        if(onExit && other.tag == Tags.Player)
            Destroy(objectToDestroy);
    }
}
