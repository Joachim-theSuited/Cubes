using UnityEngine;

/// <summary>
/// Script to manage usable items.
/// </summary>
public abstract class ItemScript : MonoBehaviour {

    /// <summary>
    /// Will be called after the player equipped the item.
    /// Base implementation resets local position and rotation.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnEquipped(GameObject player) {
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Will be called after the player drops the item.
    /// Base implementation resets rotation and places item in front of the player.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnDropped(GameObject player) {
        transform.localRotation = Quaternion.identity;
        Vector3 pos = player.transform.position + player.transform.forward * 2;
        pos.y = 0.5f;
        transform.position = pos;
    }

    /// <summary>
    /// Use this item.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void Use(GameObject player) {
    }

}
