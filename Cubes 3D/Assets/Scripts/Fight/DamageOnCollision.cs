using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damages other objects on (trigger-)collision.
/// The Damage is only applied, if the other object faces this one.
/// </summary>
[RequireComponent(typeof(Collider))]
public class DamageOnCollision : MonoBehaviour {

    public float damage;

    /// <summary>
    /// Controls, up to what angle the objects are considered facing each other.
    /// </summary>
    public float maxAngle;

    void reaction(Collider other) {
        if(maxAngle > Vector3.Angle(other.transform.forward, transform.position - other.transform.position)) {
            other.SendMessage(Messages.DAMAGE, new DamageInfo(damage, gameObject), SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter(Collider other) {
        reaction(other);
    }

    void OnCollisionEnter(Collision collision) {
        reaction(collision.collider);
    }
}