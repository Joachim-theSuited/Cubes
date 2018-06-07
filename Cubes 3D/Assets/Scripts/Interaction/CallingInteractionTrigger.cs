using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(Collider))]
public abstract class CallingInteractionTrigger : InteractionTrigger {

    protected abstract void callback();

    void Update() {
        if(canInteract && Input.GetButtonDown(Inputs.Interact)) {
            callback();
        }
    }
}