using UnityEngine;
using System.Reflection;

/// <summary>
/// Script to manage usable items.
/// An InteractionTrigger Script is required so that the item can be picked up.
/// </summary>
[RequireComponent(typeof(InteractionTrigger))]
public abstract class ItemScript : MonoBehaviour {

    /// <summary>
    /// The properties of the Rigidbody to back up.
    /// We cannot simply use GetProperties(), as this would include position, for example.
    /// </summary>
    static readonly PropertyInfo[] backupProps = {
        typeof(Rigidbody).GetProperty("mass"),
        typeof(Rigidbody).GetProperty("drag"),
        typeof(Rigidbody).GetProperty("angularDrag"),
        typeof(Rigidbody).GetProperty("useGravity"),
        typeof(Rigidbody).GetProperty("isKinematic"),
        typeof(Rigidbody).GetProperty("interpolation"),
        typeof(Rigidbody).GetProperty("collisionDetectionMode"),
        typeof(Rigidbody).GetProperty("constraints")
    };

    /// <summary>
    /// A Rigidbody used for storing the properties of the actual RB.
    /// This is necessary, as the actual RB is removed while the player holds an item.
    /// </summary>
    Rigidbody rbBackup;

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
    /// It also removes the Rigidbody (if present) and backs up its properties.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnEquipped(GameObject player) {
        transform.localRotation = Quaternion.identity;

        Rigidbody oldRB = GetComponent<Rigidbody>();
        if(oldRB != null) {
            if(rbBackup == null) {
                //the backup RB must belong to a GameObject to avoid NPEs
                GameObject backup = new GameObject("rbBackup");
                backup.transform.SetParent(transform);
                backup.SetActive(false); //the Object is not for actual use
                rbBackup = backup.AddComponent<Rigidbody>();
            }

            foreach(PropertyInfo field in backupProps) {
                field.SetValue(rbBackup, field.GetValue(oldRB, null), null);
            }

            Destroy(oldRB);
        }
    }

    /// <summary>
    /// Will be called after the player drops the item.
    /// Base implementation resets rotation and places item in front of the player.
    /// It also restores the Rigidbody.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void OnDropped(GameObject player) {
        transform.localRotation = Quaternion.identity;
        Vector3 pos = player.transform.position + player.transform.forward * 2;
        pos.y = 0.5f;
        transform.position = pos;

        Rigidbody newRB = gameObject.AddComponent<Rigidbody>();
        if(rbBackup != null)
            foreach(PropertyInfo field in backupProps) {
                field.SetValue(newRB, field.GetValue(rbBackup, null), null);
            }
    }

    /// <summary>
    /// Use this item.
    /// </summary>
    /// <param name="player">The Player GameObject.</param>
    public virtual void Use(GameObject player) {
    }

}
