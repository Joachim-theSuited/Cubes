using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericLauncher : ItemScript {

	public Rigidbody missile;

	public override void Use(GameObject player)
	{
		base.Use(player);
		Rigidbody missileClone = (Rigidbody)Instantiate(missile, transform.position, transform.rotation);
		missileClone.velocity = transform.forward * 30f;
	}

}
