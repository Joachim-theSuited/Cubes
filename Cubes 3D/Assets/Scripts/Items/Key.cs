using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script for an item, that can interact with a defined set of other items.
/// E.g. a key that can open certain locks.
/// Needs a trigger collider to check whether a target is available.
/// To unlock a target the player must Use the key.
/// </summary>
[RequireComponent(typeof(Collider))]
public class Key : ItemScript {
    
    /// <summary>
    /// The targets this key can be used with.
    /// When this GameObjects trigger enters one of the target colliders, the message "Unlock" is sent to it.
    /// The instance of this Script will be sent as parameter to the unlock message.
    /// </summary>
    public List<Collider> targets;

    /// <summary>
    /// If this flag is set to true, the unlock message will only be sent on the first contact with each target.
    /// </summary>
    public bool singleUnlock = true;

    /// <summary>
    /// The number of uses.
    /// Every sending of the unlock message counts as one use.
    /// If 0 uses remain, no more targets will be unlocked.
    /// Set to a negative number to allow unlimited use.
    /// </summary>
    public int numberOfUses = 1;

    /// <summary>
    /// If set to true, the gameobject will be destroyed after its last use.
    /// </summary>
    public bool destroyWhenUsedUp;

    public void OnTriggerEnter(Collider coll) {
        tryUnlock(coll);
    }

    /// <summary>
    /// Checks whether the given Rigidbody is in range of any target(s) and unlocks it/them.
    /// </summary>
    protected void tryUnlock(Collider target) {
        if(numberOfUses == 0)
            return;

        if(targets.Contains(target)) {
            target.SendMessage(Messages.UNLOCK, this);
            numberOfUses--;
            if(singleUnlock) {
                targets.Remove(target);
            }
        }

        if(numberOfUses == 0 && destroyWhenUsedUp) {
            GameObject.FindWithTag(Tags.Player).SendMessage(Messages.UNEQUIP_ITEM, this);
            Destroy(gameObject);
        }
    }

}
