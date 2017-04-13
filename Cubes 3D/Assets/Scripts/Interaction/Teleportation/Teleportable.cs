using UnityEngine;

/// <summary>
/// A Teleportable is a thing that can be moved by Teleporters.
/// </summary>
public class Teleportable : MonoBehaviour {

    /// <summary>
    /// This is set to the last Teleporter that was used to prevent instantly teleporting back and forth.
    /// Should be set to null when exiting this Teleporter.
    /// </summary>
    public Teleporter lastTeleportedTo;

}
