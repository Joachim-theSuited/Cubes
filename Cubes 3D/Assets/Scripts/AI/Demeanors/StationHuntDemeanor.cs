using UnityEngine;

[RequireComponent(typeof(Hunt))]
[RequireComponent(typeof(Station))]
[RequireComponent(typeof(AbstractSensor))]
public class StationHuntDemeanor : AbstractDemeanor {

    private AbstractSensor _sensor;
    private Hunt _hunt;
    private Station _station;
    
	void Start () {
        _sensor = GetComponent<AbstractSensor>();
        _hunt = GetComponent<Hunt>();
        _station = GetComponent<Station>();
	}
	
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
            _station.enable();
        }
        else if(!_hunt.enabled && _sensor.getTriggered())
        {
            // start hunt
            _station.disable();
            _hunt.enable();
        }
        else
        {
            // stand at station
            // NOP
        }
	}
}
