using UnityEngine;

/// <summary>
/// This demeanor combines the Patrol and Chase behaviours so a unit will walk its assigned patrol
/// but pause it when a given Sensor is activated and activate the Chase behaviour.
/// </summary>
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(AbstractSensor))]
[RequireComponent(typeof(Chase))]
public class PatrolChaseDemeanor : MonoBehaviour {

    private AbstractSensor _sensor;
    private Patrol _patrol;
    private Chase _chase;

    // Use this for initialization
    void Start () {
        _sensor = GetComponent<AbstractSensor>();
        _patrol = GetComponent<Patrol>();
        _chase = GetComponent<Chase>();
        _chase.disable();
	}
	
	// Update is called once per frame
	void Update () {
		if(_chase.enabled && _sensor.getTriggered())
        {
            // chase enemy
            // NOP
        }
        else if(_chase.enabled && !_sensor.getTriggered())
        {
            // return to patrol
            _chase.disable();
            _patrol.enable();
        }
        else if(!_chase.enabled && _sensor.getTriggered())
        {
            // start chase
            _patrol.disable();
            _chase.enable();
        }
        else
        {
            // follow patrol
            // NOP
        }
	}
}
