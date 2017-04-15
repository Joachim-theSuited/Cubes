using UnityEngine;
using System.Collections;

/// <summary>
/// A weight that can be put onto a PressurePlate.
/// </summary>
//[RequireComponent(typeof(Rigidbody))]
public class Weight : ItemScript {

    public GameObject dropAs;

    /// <summary>
    /// Produces the same effect as dropping the weight.
    /// </summary>
    public override void Use(GameObject player) {
        player.SendMessage(Messages.DROP_EQUIPPED);
    }

    public override void OnDropped(GameObject player) {
        base.OnDropped(player);
        Instantiate(dropAs, transform.position, transform.rotation, transform.parent);
        Destroy(this.gameObject);
    }

}
