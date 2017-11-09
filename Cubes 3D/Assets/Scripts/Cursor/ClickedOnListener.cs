using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

/// <summary>
/// A generic listener, that triggers an action, when the user clicks on a collider on the screen.
/// </summary>
[RequireComponent(typeof(Collider))]
public abstract class ClickedOnListener : MonoBehaviour {

	private Collider _collider;

	// Use this for initialization
	void Start () {
		_collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(Inputs.Fire1)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit) && hit.collider == _collider) {
				activate();
			}
		}
	}

	protected abstract void activate();
}