using UnityEngine;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// This script requires a 'trigger' collider to be attached.
/// The interaction can then be triggered by pressing 'E'.
/// This causes the message 'Interact' to be sent to other Script on its GameObject.
/// </summary>
[RequireComponent(typeof(Collider))]
public class InteractionTrigger : MonoBehaviour {

    /// <summary>
    /// GameObject that will be enabled iff the InteractionTrigger can be interacted with.
    /// Can be null.
    /// </summary>
    public GameObject interactionIndicator;

    protected bool canInteract = false;

    public void Start() {
        if(interactionIndicator != null)
            interactionIndicator.SetActive(false);
    }

    void Update() {
		if(canInteract && Input.GetButtonDown(Inputs.Interact)) {
            SendMessage(Messages.INTERACT, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.Player && !transform.IsChildOf(other.transform) && AcquireLock(other.transform.position)) {
            EnableInteraction();
        } else if(other.tag == Tags.Player && transform.IsChildOf(other.transform)) {
            DisableInteraction();
            ReleaseLock();
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.tag == Tags.Player && !transform.IsChildOf(other.transform) && AcquireLock(other.transform.position)) {
            EnableInteraction();
        } else if(other.tag == Tags.Player && transform.IsChildOf(other.transform)) {
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
        if(interactionIndicator != null)
            interactionIndicator.SetActive(true);
    }

    public void DisableInteraction() {
        canInteract = false;
        if(interactionIndicator != null)
            interactionIndicator.SetActive(false);
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
