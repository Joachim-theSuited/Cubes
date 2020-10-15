using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericInteraction : MonoBehaviour {

    public UnityEvent onInteractEvents;

	void Interact(){
        onInteractEvents.Invoke();
    }

}
