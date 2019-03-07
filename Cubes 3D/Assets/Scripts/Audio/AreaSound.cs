using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AreaSound : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.Player) {
            GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<AmbiantSoundManager>().enterZone(gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == Tags.Player) {
            GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<AmbiantSoundManager>().leaveZone(gameObject);
        }
    }
}