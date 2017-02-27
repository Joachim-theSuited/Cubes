using UnityEngine;

[RequireComponent(typeof(Chase))]
[RequireComponent(typeof(Station))]
[RequireComponent(typeof(AbstractSensor))]
public class StationChaseDemeanor : AbstractDemeanor {

    private AbstractSensor _sensor;
    private Chase _chase;
    private Station _station;

    void Start()
    {
        _sensor = GetComponent<AbstractSensor>();
        _chase = GetComponent<Chase>();
        _station = GetComponent<Station>();
    }

    void Update ()
    {
        if(_chase.enabled && _sensor.getTriggered())
        {
            // chase enemy; handled in active Chase
            // NOP
        }
        else if(_chase.enabled && !_sensor.getTriggered())
        {
            // return to station
            _chase.disable();
            _station.enable();
        }
        else if(!_chase.enabled && _sensor.getTriggered())
        {
            // start chase
            _station.disable();
            _chase.enable();
        }
        else
        {
            // stand at station
            // NOP
        }
    }
	
}
