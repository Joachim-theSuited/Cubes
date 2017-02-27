using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Generic door, that invokes the configured events when un-/locked.
/// </summary>
public class GenericDoor : MonoBehaviour {

    public UnityEvent openEvent;

    public UnityEvent closeEvent;

    virtual public void Lock() {
        closeEvent.Invoke();
    }

    virtual public void Unlock() {
        openEvent.Invoke();
    }

}
