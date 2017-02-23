using UnityEngine;

/// <summary>
/// An AbstractSensor is any Behaviour, that can indicate to an observer, that a condition is met.
/// </summary>
public abstract class AbstractSensor : MonoBehaviour {

    protected bool triggered;

    /// <summary>
    /// Getter for the triggered value.
    /// </summary>
    /// <returns>true iff this sensor was triggered</returns>
    public bool getTriggered()
    {
        return triggered;
    }

}
