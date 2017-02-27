using UnityEngine;

[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(AbstractSensor))]
[RequireComponent(typeof(Hunt))]
public class PatrolHuntDemeanor : AbstractDemeanor {

    private AbstractSensor _sensor;
    private Patrol _patrol;
    private Hunt _hunt;

	// Use this for initialization
	void Start () {
        _sensor = GetComponent<AbstractSensor>();
        _patrol = GetComponent<Patrol>();
        _hunt = GetComponent<Hunt>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_hunt.enabled && _sensor.getTriggered())
        {
            // hunt target; handled in active Hunt
            // NOP
        }
        else if(_hunt.enabled && !_sensor.getTriggered())
        {
            // return to station
            _hunt.disable();
            _patrol.enable();
        }
        else if(!_hunt.enabled && _sensor.getTriggered())
        {
            // start hunt
            _patrol.disable();
            _hunt.enable();
        }
        else
        {
            // stand at station
            // NOP
        }
	}
}
