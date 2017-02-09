using UnityEngine;

/// <summary>
/// Script to manage usable items.
/// </summary>
public abstract class ItemScript : MonoBehaviour {

    /// <summary>
    /// Will be called after the player equipped the item.
    /// Base implementation resets local rotation and sets the shader to Sprites/Default.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnEquipped(GameObject player) {
        /* TODO
        transform.localRotation = Quaternion.identity;
        Shader shader = Shader.Find(Shading.DEFAULT_SHADER);
        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true)){
            int id = sr.material.GetInt(Shading.PARAM_ROOM_ID);
            sr.material = new Material(shader);
            sr.material.SetInt(Shading.PARAM_ROOM_ID, id);
        }
        */
    }

    /// <summary>
    /// Will be called after the player drops the item.
    /// Base implementation sets the Shader to FadingShader.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnDropped(GameObject player) {
        /* TODO
        Shader shader = Shader.Find(Shading.FADING_SHADER);
        foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true)){
            int id = sr.material.GetInt(Shading.PARAM_ROOM_ID);
            sr.material = new Material(shader);
            sr.material.SetInt(Shading.PARAM_ROOM_ID, id);
        }
        */
    }

    /// <summary>
    /// Sends a message to the player, triggering the picking up of this item.
    /// </summary>
    public virtual void PickUp() {
        GameObject player = GameObject.FindWithTag(Tags.Player);
        player.SendMessage(Messages.EQUIP_ITEM, this);
    }

    /// <summary>
    /// Use this item.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void Use(GameObject player) {
    }

}
