﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

/// <summary>
/// Switches the selected character model to the PersistentPlayers below this GameObject.
/// </summary>
public class CharacterSelectionListener : CallingInteractionTrigger {

	public Avatar avatar;
	public List<GameObject> toEnable;

	private void select() {
		// delete old persistent player to prevent them littering DontDestroyOnLoad
		if(AcquirePersistentPlayer.PERSISTENT_PLAYER != null) {
			Destroy(AcquirePersistentPlayer.PERSISTENT_PLAYER);
		} 

		GameObject player = GameObject.FindWithTag(Tags.Player);

		Transform selectedChar = Instantiate(transform.GetChild(0));

		Destroy(player.transform.GetChild(player.transform.childCount-1).gameObject);

		selectedChar.parent = player.transform;

		selectedChar.transform.localPosition = Vector3.zero;
		selectedChar.transform.localRotation = Quaternion.identity;

		AcquirePersistentPlayer.PERSISTENT_PLAYER = Instantiate(transform.GetChild(0)).gameObject;
		AcquirePersistentPlayer.PERSISTENT_PLAYER.SetActive(false);
		Animator playerAnimator = player.GetComponent<Animator>();
		playerAnimator.avatar = avatar;
		playerAnimator.Rebind();
	}

	private void enableLevelChanges() {
		foreach(GameObject gameObject in toEnable) {
			gameObject.SetActive(true);
		}
	}

    protected override void callback() {
        select();
		enableLevelChanges();
    }
}