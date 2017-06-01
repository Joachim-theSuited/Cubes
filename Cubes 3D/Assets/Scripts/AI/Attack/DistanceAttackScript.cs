using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows AI to use distance weapons like bows.
/// They will rain down a hail of missiles as fast as they are allowed and not aim very accurately.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DistanceAttackScript : MonoBehaviour {

	public AbstractSensor trigger;
	public float attackCooldown;

	public ItemScript weapon;

	private float runningCooldown;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		runningCooldown += Time.deltaTime;
		if(trigger.getTriggered()) {
			if(runningCooldown > attackCooldown) {
				weapon.Use(gameObject);
				runningCooldown = 0;
			}
		}
	}
}
