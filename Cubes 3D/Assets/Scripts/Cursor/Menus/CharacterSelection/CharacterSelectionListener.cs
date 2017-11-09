using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Sets the selected player model as DontDestroyOnLoad and loads the next scene.
/// </summary>
[RequireComponent(typeof(Collider))]
public class CharacterSelectionListener : ClickedOnListener {

	protected override void activate() {
		Transform firstChild = transform.GetChild(0);
		firstChild.parent = null;
		DontDestroyOnLoad(firstChild);

		const string gameScene = "Sandbox";
		SceneManager.LoadScene(gameScene);
		Scene sceneToSwitchTo = SceneManager.GetSceneByName(gameScene);

		SceneManager.SetActiveScene(sceneToSwitchTo);
	}
}