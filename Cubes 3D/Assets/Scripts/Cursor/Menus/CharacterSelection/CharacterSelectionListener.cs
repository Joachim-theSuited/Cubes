using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Sets the selected player model as DontDestroyOnLoad and loads the next scene.
/// </summary>
public class CharacterSelectionListener : CallingInteractionTrigger {
	private void select() {
		print("selected " + transform);
		Transform firstChild = transform.GetChild(0);
		firstChild.parent = null;
		DontDestroyOnLoad(firstChild);
	}

    protected override void callback() {
        select();
    }
}