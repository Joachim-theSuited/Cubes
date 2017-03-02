using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLogger : MonoBehaviour {

    /// <summary>
    /// Should trigger events be logged?
    /// </summary>
    public bool logTrigger = true;

    /// <summary>
    /// Should collision events be logged?
    /// </summary>
    public bool logCollision = true;

    /// <summary>
    /// Should TriggerStay/CollisionStay be logged?
    /// </summary>
    public bool logStay = false;

    void OnCollisionEnter(Collision coll) {
        if(logCollision) {
            Debug.Log("At: " + name + ", Collision Enter From: " + coll.collider.name);
        }
    }

    void OnCollisionExit(Collision coll) {
        if(logCollision) {
            Debug.Log("At: " + name + ", Collision Exit From: " + coll.collider.name);
        }
    }

    void OnCollisionStay(Collision coll) {
        if(logStay && logCollision) {
            Debug.Log("At: " + name + ", Collision Stay From: " + coll.collider.name);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(logTrigger) {
            Debug.Log("At: " + name + ", Trigger Enter From: " + other.name);
        }
    }

    void OnTriggerExit(Collider other) {
        if(logTrigger) {
            Debug.Log("At: " + name + ", Trigger Exit From: " + other.name);
        }
    }

    void OnTriggerStay(Collider other) {
        if(logStay && logTrigger) {
            Debug.Log("At: " + name + ", Trigger Stay From: " + other.name);
        }
    }

}
