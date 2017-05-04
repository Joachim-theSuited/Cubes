using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MainMenuTrigger : MonoBehaviour {
	
	void Update () {
		// TODO: does not use generic Input Axis; only viable on computers
		if (Input.GetButtonDown(Inputs.Cancel)) {
			GetComponent<Canvas>().enabled = true;
			Time.timeScale = 0;
		}
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		GetComponent<Canvas>().enabled = false;
	}

	public void ExitGame() {
		Application.Quit();
	}
}
