using UnityEngine;

/// <summary>
/// A simple Sensor, that gets triggered, whenever the player comes close.
/// This sensor is repeatable and will set to untriggered, when the player moves away.
/// </summary>
public class PlayerProximitySensor : AbstractSensor {

    private GameObject player;

    /// <summary>
    /// How close the player has to be to activate this sensor.
    /// </summary>
    public float triggerDistance = 600;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        triggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		if((player.transform.position - transform.position).magnitude < triggerDistance)
        {
            triggered = true;
        } else
        {
            triggered = false;
        }
	}

}
