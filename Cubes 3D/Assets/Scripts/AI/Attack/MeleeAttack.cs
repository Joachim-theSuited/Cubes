using UnityEngine;

/// <summary>
/// Allows AI to use weapons in melee combat. They are not very clever ans more or less just swing wildly.
/// </summary>
public class MeleeAttack : MonoBehaviour {

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
