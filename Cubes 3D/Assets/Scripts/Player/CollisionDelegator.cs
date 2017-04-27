using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Forwards collision events from the player's Rgidbody to child objects.
/// Useful e.g. for items.
/// </summary>
//FIXME will forward several time to the same child, if multiple contacts exist
public class CollisionDelegator : MonoBehaviour {

    void OnCollisionEnter(Collision coll) {
        foreach(ContactPoint contact in  coll.contacts) {
            if(contact.thisCollider.gameObject != gameObject) {
                contact.thisCollider.SendMessage("OnCollisionEnter", coll, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void OnCollisionExit(Collision coll) {
        foreach(ContactPoint contact in  coll.contacts) {
            if(contact.thisCollider.gameObject != gameObject)
                contact.thisCollider.SendMessage("OnCollisionExit", coll, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnCollisionStay(Collision coll) {
        foreach(ContactPoint contact in  coll.contacts) {
            if(contact.thisCollider.gameObject != gameObject)
                contact.thisCollider.SendMessage("OnCollisionStay", coll, SendMessageOptions.DontRequireReceiver);
        }
    }

}
