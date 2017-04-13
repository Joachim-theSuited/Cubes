using UnityEngine;

/// <summary>
/// A Teleporter can move a Teleportable to any other Teleporter in the same scene.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Teleporter : MonoBehaviour {

    public Teleporter destination;

    /// <summary>
    /// Move a Teleportable that entered to the destination.
    /// </summary>
    /// <param name="other">thing that entered</param>
    void OnTriggerEnter(Collider other) {
        Teleportable otherTeleportable = other.GetComponent<Teleportable>();
        if(otherTeleportable != null && otherTeleportable.lastTeleportedTo != this && destination != null) {
            other.transform.position = destination.transform.position;
            otherTeleportable.lastTeleportedTo = destination;
        }
    }

    /// <summary>
    /// Cleanup Teleportable from last teleportation.
    /// </summary>
    /// <param name="other">thing that is exiting</param>
    void OnTriggerExit(Collider other) {
        Teleportable otherTeleportable = other.GetComponent<Teleportable>();
        if(otherTeleportable != null && otherTeleportable.lastTeleportedTo == this) {
            otherTeleportable.lastTeleportedTo = null;
        }
    }
}
