using UnityEngine;
using System.Collections;

/// <summary>
/// A weight that can be put onto a PressurePlate.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Weight : ItemScript {

    /// <summary>
    /// Produces the same effect as dropping the weight.
    /// </summary>
    public override void Use(GameObject player) {
        player.SendMessage(Messages.DROP_EQUIPPED);
    }

}
