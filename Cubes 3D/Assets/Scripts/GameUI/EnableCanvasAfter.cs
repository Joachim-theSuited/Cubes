﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Causes an attached Canvas component to be enabled, after the given timeToEnable has elapsed.
/// </summary>
[RequireComponent(typeof(Canvas))]
public class EnableCanvasAfter : MonoBehaviour {

	public float timeToEnable;

	private Canvas _canvas;
	private float timeElapsed;

	private GameObject pauseMenuOverlay;

	void Start () {
		_canvas = GetComponent<Canvas>();
		timeElapsed = 0f;

		pauseMenuOverlay = GameObject.Find("PauseMenuOverlay");
	}

	void Update () {
		timeElapsed += Time.deltaTime;
		if(timeElapsed > timeToEnable) {
			_canvas.enabled = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			bool selectedSomething = false;
			foreach(Button child in _canvas.GetComponentsInChildren<Button>()) {
				child.enabled = true;
				if(!selectedSomething) {
					child.Select();
					selectedSomething = true;
				}
			}

			if(pauseMenuOverlay != null) {
				// disable pause menu; would be redundant and may cause overlying UI elements
				pauseMenuOverlay.GetComponent<PauseMenuTrigger>().enabled = false;
			}

			Destroy(this);
		}
	}
}
