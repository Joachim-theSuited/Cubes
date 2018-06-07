﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Switches the selected character model to the PersistentPlayers below this GameObject.
/// </summary>
public class CharacterSelectionListener : CallingInteractionTrigger {

	private void select() {
		GameObject player = GameObject.FindWithTag(Tags.Player);

		Transform selectedChar = Instantiate(transform.GetChild(0));

		Destroy(player.transform.GetChild(player.transform.childCount-1).gameObject);

		selectedChar.parent = player.transform;

		selectedChar.transform.localPosition = Vector3.zero;
		selectedChar.transform.localRotation = Quaternion.identity;
	}

    protected override void callback() {
        select();
    }
}