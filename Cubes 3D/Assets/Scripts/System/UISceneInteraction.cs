using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// User interface scene interaction. Can be used to change the scene using the UI to access menus in specific Scenes.
/// </summary>
public class UISceneInteraction : MonoBehaviour {

	private string sceneToChangeTo;

	/// <summary>
	/// Switchs to scene with the given sceneName.
	/// Will also resume gameplay, setting timeScale to 1.
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
	public void SwitchToScene(string sceneName) {
		GameFlow.ResumeGame();
		SceneManager.LoadScene(sceneName);
		Scene sceneToSwitchTo = SceneManager.GetSceneByName(sceneName);
		SceneManager.SetActiveScene(sceneToSwitchTo);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
