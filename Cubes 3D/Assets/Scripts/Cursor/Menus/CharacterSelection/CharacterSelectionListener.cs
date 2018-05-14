using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Sets the selected player model as DontDestroyOnLoad and loads the next scene.
/// </summary>
[RequireComponent(typeof(Collider))]
public class CharacterSelectionListener : MonoBehaviour {

	public PointAtCursor pointAtCursor;

	void Update() {
		if(Input.GetButtonDown(Inputs.Interact)) {
			RaycastHit hit;
			if(pointAtCursor.cursorRaycast(out hit) && hit.collider.gameObject.Equals(gameObject)) {
				selectAndChangeScene();
			}
		}
	}
	
	void OnMouseDown() {
		selectAndChangeScene();
	}

	private void selectAndChangeScene() {
		Transform firstChild = transform.GetChild(0);
		firstChild.parent = null;
		DontDestroyOnLoad(firstChild);

		const string gameScene = "Sandbox";
		SceneManager.LoadScene(gameScene);
	}

}