using UnityEngine;

/// <summary>
/// An input listener that can activate the main game menu and pause/unpause the game.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class MainMenuTrigger : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown(Inputs.Cancel)) {
			GameFlow.PauseGame();
			GetComponent<Canvas>().enabled = true;
		}
	}

	/// <summary>
	/// Resumes the game.
	/// </summary>
	public void ResumeGame() {
		GameFlow.ResumeGame();
		GetComponent<Canvas>().enabled = false;
	}

	/// <summary>
	/// Exits the game.
	/// </summary>
	public void ExitGame() {
		Application.Quit();
	}
}
