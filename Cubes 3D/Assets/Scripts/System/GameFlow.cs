using UnityEngine;

/// <summary>
/// A wrapper class to handle all actions to be done when any part of the game wants to pause.s
/// </summary>
public static class GameFlow {

	/// <summary>
	/// Pauses the game. Reveals and unlocks cursor.
	/// </summary>
	public static void PauseGame() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
		Time.timeScale = 0;
	}

	/// <summary>
	/// Resumes the game. Hides and locks cursor.
	/// </summary>
	public static void ResumeGame() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		Time.timeScale = 1;
	}

}