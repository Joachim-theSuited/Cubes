using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// A variant of the InteractionTrigger that does direct calls instead of using messages.
/// </summary>
[RequireComponent(typeof(Collider))]
public abstract class CallingInteractionTrigger : InteractionTrigger {

    protected abstract void callback();

    void Update() {
        if(canInteract && Input.GetButtonDown(Inputs.Interact)) {
            callback();
        }
    }
}