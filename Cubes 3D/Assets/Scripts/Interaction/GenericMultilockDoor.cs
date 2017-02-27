using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A GenericDoor that needs multiple unlocks to open.
/// </summary>
public class GenericMultilockDoor : GenericDoor {

    /// <summary>
    /// The number of unlocks required to open this door.
    /// </summary>
    public uint numberOfLocks;

    /// <summary>
    /// Internal counter of how many unlocks are still needed.
    /// </summary>
    uint lockCounter;

    void Awake() {
        lockCounter = numberOfLocks;
    }

    override public void Lock() {
        if(lockCounter == 0) {
            base.Lock();
        }
        lockCounter++;
        if(lockCounter > numberOfLocks) {
            Debug.LogError("Too many locks!");
            lockCounter = numberOfLocks;
        }
    }

    override public void Unlock() {
        if(lockCounter == 0) {
            Debug.LogError("Too many unlocks!");
        } else {
            lockCounter--;
            if(lockCounter == 0) {
                base.Unlock();
            }
        }
    }

}
