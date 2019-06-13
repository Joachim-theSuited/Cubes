using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(InterSceneTeleporter))]
public class GateTriggerBehaviour : MonoBehaviour {

	public bool gateOpen;

	void Start()
	{
		gateOpen = false;
		GetComponent<InterSceneTeleporter>().enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if(gateOpen && other.tag.Equals(Tags.Player)) {

			LevelTime.INSTANCE.stopTimer();

			SaveData save = GameProgressionPersistence.loadProgress();
			LevelConfig currentLevel = LevelConfigManager.Instance.config;
			save.unlockedLevels |= 1 << currentLevel.number;
			int oldLength = save.levelTimes.Length;
			if (oldLength < currentLevel.number)
			{
				Array.Resize(ref save.levelTimes, currentLevel.number + 1);
				for (int i = oldLength; i <= currentLevel.number; ++i)
					save.levelTimes[i] = -1;
			}

			if (save.levelTimes[currentLevel.number] == -1)
				save.levelTimes[currentLevel.number] = (float)LevelTime.INSTANCE.elapsedTime().TotalSeconds;
			else
				save.levelTimes[currentLevel.number] = Math.Min(save.levelTimes[currentLevel.number], (float)LevelTime.INSTANCE.elapsedTime().TotalSeconds);
			
			GameProgressionPersistence.saveProgress(save);

			GetComponent<InterSceneTeleporter>().enabled = true;

			// GameObject levelDoneMenu = GameObject.Find("LevelDoneMenu");
			// if(levelDoneMenu != null) {
			// 	Canvas canvas = levelDoneMenu.GetComponent<Canvas>();
			// 	canvas.enabled = true;
			// 	bool selectedSomething = false;
			// 	foreach(Button child in canvas.GetComponentsInChildren<Button>()) {
			// 		child.enabled = true;
			// 		if(!selectedSomething) {
			// 			child.Select();
			// 			selectedSomething = true;
			// 		}
			// 	}

			// 	Cursor.visible = true;
			// 	Cursor.lockState = CursorLockMode.None;
			// 	GameObject.Find("PauseMenuOverlay").GetComponent<PauseMenuTrigger>().enabled = false;
			// }
		}
	}
}
