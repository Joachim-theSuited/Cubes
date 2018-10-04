using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class GateTriggerBehaviour : MonoBehaviour {

	public bool gateOpen = true;

	void OnTriggerEnter(Collider other) {
		if(gateOpen && other.tag.Equals(Tags.Player)) {
			GameObject levelDoneMenu = GameObject.Find("LevelDoneMenu");
			if(levelDoneMenu != null) {
				Canvas canvas = levelDoneMenu.GetComponent<Canvas>();
				canvas.enabled = true;
				bool selectedSomething = false;
				foreach(Button child in canvas.GetComponentsInChildren<Button>()) {
					child.enabled = true;
					if(!selectedSomething) {
						child.Select();
						selectedSomething = true;
					}
				}

				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				GameObject.Find("PauseMenuOverlay").GetComponent<PauseMenuTrigger>().enabled = false;
			}
		}
	}
}
