using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterLevelTeleporter : MonoBehaviour {

	public LevelConfig targetLevel;

	public LevelConfigManager.LoadOption option;

	void Interact() {
		LevelConfigManager.Instance.Load(targetLevel, option);
	}
}
