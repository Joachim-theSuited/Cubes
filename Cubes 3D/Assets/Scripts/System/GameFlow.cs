using UnityEngine;

/// <summary>
/// A wrapper class to handle all actions to be done when any part of the game wants to pause.
/// </summary>
public class GameFlow : MonoBehaviour {

	/// <summary>
	/// Pauses the game. Reveals and unlocks cursor.
	/// </summary>
	public static void PauseGame() {
		Time.timeScale = 0;
	}

	/// <summary>
	/// Resumes the game. Hides and locks cursor.
	/// </summary>
	public static void ResumeGame() {
		Time.timeScale = 1f;
	}

}