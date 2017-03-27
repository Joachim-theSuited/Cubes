using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A pressure plate that is triggered by the player and/or Weight items.
/// </summary>
[RequireComponent(typeof(Collider))]
public class PressurePlate : MonoBehaviour {

    /// <summary>
    /// Will be invoked, when weight is put onto a free plate.
    /// </summary>
    public UnityEvent OnDown;

    /// <summary>
    /// Will be invoked, when the plate becomes free.
    /// </summary>
    public UnityEvent OnUp;

    /// <summary>
    /// All weights currently on this plate.
    /// </summary>
    private List<Weight> weights = new List<Weight>();

    /// <summary>
    /// False, if the player or any weight is on the plate
    /// </summary>
    public bool isFree { get { return weights.Count == 0; } }

    /// <summary>
    /// Adds a weight onto the trigger.
    /// </summary>
    public void AddWeight(Weight weight) {
        if(weight == null)
            return;
        
        if(!weights.Contains(weight)) {
            if(isFree) {
                OnDown.Invoke();
            }
            weights.Add(weight);
        }
    }

    /// <summary>
    /// Remove a weight from the trigger.
    /// </summary>
    public void RemoveWeight(Weight weight) {
        if(weight == null)
            return;

        if(weights.Contains(weight)) {
            weights.Remove(weight);
            if(isFree) {
                OnUp.Invoke();
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if(!other.isTrigger) //only 'solid' colliders should trigger
            AddWeight(other.GetComponent<Weight>());
    }

    void OnTriggerExit(Collider other) {
        RemoveWeight(other.GetComponent<Weight>());
    }

}
