﻿using UnityEngine;

/// <summary>
/// An input listener that can activate the main game menu and pause/unpause the game.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class PauseMenuTrigger : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown(Inputs.Cancel)) {
			GameFlow.PauseGame();
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			GetComponent<Canvas>().enabled = true;
		}
	}

	/// <summary>
	/// Resumes the game.
	/// </summary>
	public void ResumeGame() {
		GameFlow.ResumeGame();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		GetComponent<Canvas>().enabled = false;
	}
}