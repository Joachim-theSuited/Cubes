using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExitGameTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag.Equals(Tags.Player)) {
			Debug.Log("Application.Quit() called; not effective in editor");
			Application.Quit();
		}
	}
}
