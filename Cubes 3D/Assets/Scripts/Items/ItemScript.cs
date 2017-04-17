using UnityEngine;

/// <summary>
/// Script to manage usable items.
/// An InteractionTrigger Script is required so that the item can be picked up.
/// </summary>
[RequireComponent(typeof(InteractionTrigger))]
public abstract class ItemScript : MonoBehaviour {

    /// <summary>
    /// Catches the InteractionTrigger's message and lets the player pick up the item.
    /// </summary>
    public void Interact() {
        GameObject player = GameObject.FindWithTag(Tags.Player);
        player.SendMessage(Messages.EQUIP_ITEM, this);
    }


    /// <summary>
    /// Will be called after the player equipped the item.
    /// Base implementation resets local position and rotation.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnEquipped(GameObject player) {
        transform.localRotation = Quaternion.identity;
        Destroy(GetComponent<Rigidbody>());
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
        gameObject.AddComponent<Rigidbody>();
    }

    /// <summary>
    /// Use this item.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void Use(GameObject player) {
    }

}
