using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic launcher is anything that can spawn an object as a projectile to hit a target.
/// </summary>
public class GenericLauncher : ItemScript {

	public Rigidbody missile;

	public float missileDespawnTime;

	public float force = 30f;

	public override void Use(GameObject player)
	{
		base.Use(player);
		Rigidbody missileClone = (Rigidbody)Instantiate(missile, transform.position, transform.rotation);
		missileClone.velocity = transform.forward * force;
		Destroy(missileClone.gameObject, missileDespawnTime);
	}

}
