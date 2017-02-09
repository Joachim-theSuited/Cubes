using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// This script requires a 'trigger' collider to be attached.
/// When a GameObject tagged with Tags.Player is inside the collider, the attached Renderer will be enabled.
/// The interaction can then be triggered by pressing 'E'.
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]
public class InteractionTrigger : MonoBehaviour {

    public UnityEvent triggerOnInteraction;

    bool canInteract = false;

    void Update() {
        if(canInteract && Input.GetKeyDown(KeyCode.E)) {
            triggerOnInteraction.Invoke();
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.tag == Tags.Player && !transform.IsChildOf(other.transform) && AcquireLock(other.transform.position)) {
            EnableInteraction();
        } else {
            DisableInteraction();
            ReleaseLock();
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == Tags.Player) {
            DisableInteraction();
            ReleaseLock();
        }
    }

    public void EnableInteraction() {
        canInteract = true;
        GetComponent<Renderer>().enabled = true;
    }

    public void DisableInteraction() {
        canInteract = false;
        GetComponent<Renderer>().enabled = false;
    }

    /// <summary>
    /// The active InteractionTrigger.
    /// Works as a lock variable to ensure only one object can be interacted with at a time.
    /// </summary>
    protected static InteractionTrigger active = null;

    /// <summary>
    /// Tries to make this the active InteractionTrigger.
    /// Will only succeed, if no other is active or this one is closer to the player than the currently active one.
    /// </summary>
    bool AcquireLock(Vector3 playerPosition) {
        if(active == null) {
            active = this;
            return true;
        }
        if(active == this) {
            return true;
        }
        if(Vector3.SqrMagnitude(transform.position - playerPosition) < Vector3.SqrMagnitude(active.transform.position - playerPosition)) {
            active.DisableInteraction(); //active!=null is certain
            active = this;
            return true;
        }
        return false;
    }

    void ReleaseLock() {
        active = null;
    }

}
