using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterLevelReturnTeleporter : MonoBehaviour {

	void Interact() {
		LevelConfigManager.Instance.PopLevel();
	}

}
