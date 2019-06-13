using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterLevelTeleporter : MonoBehaviour {

	public LevelConfig targetLevel;

	public LevelConfigManager.LoadOption option;

	void Start()
	{
		if ((GameProgressionPersistence.loadProgress().unlockedLevels & (1 << targetLevel.number)) != 0)
			gameObject.SetActive(true);
		else
			gameObject.SetActive(false);
	}

	void Interact() {
		LevelConfigManager.Instance.Load(targetLevel, option);
	}
}
