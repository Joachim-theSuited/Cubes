using UnityEngine;

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
				// TODO: swing weapon
				weapon.Use(gameObject);
				runningCooldown = 0;
			}
		}
	}
}
