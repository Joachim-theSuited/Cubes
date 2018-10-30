using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ExitGameTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.tag.Equals(Tags.Player)) {
			GameFlow.PauseGame();
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			GetComponentInChildren<Canvas>().enabled = true;
			foreach(Button child in GetComponentsInChildren<Button>()) {
				child.enabled = true;
			}
		}
	}

	public void ResumeGame() {
		GameFlow.ResumeGame();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		GetComponentInChildren<Canvas>().enabled = false;
		foreach(Button child in GetComponentsInChildren<Button>()) {
			child.enabled = false;
		}
	}
}
