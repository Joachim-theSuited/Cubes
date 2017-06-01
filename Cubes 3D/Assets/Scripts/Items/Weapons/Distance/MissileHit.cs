using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple script to notify a target of an attack, when he was hit.
/// </summary>
[RequireComponent(typeof(Collider))]
public class MissileHit : MonoBehaviour {

	public float damage;

	void OnCollisionEnter(Collision coll) {
		coll.gameObject.SendMessage(Messages.DAMAGE, damage, SendMessageOptions.DontRequireReceiver);
	}
}
