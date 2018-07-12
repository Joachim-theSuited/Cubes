using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
[CreateAssetMenu(fileName="level.asset", menuName="Custom/Level Config")]
public class LevelConfig : ScriptableObject {

	[Scene]
	public string scenePath;

	public int sceneID {
		get { return SceneUtility.GetBuildIndexByScenePath(scenePath); }
		set {scenePath = SceneUtility.GetScenePathByBuildIndex(value); }
	}

	public int randomSeedOffset;

	public Vector3 playerStartPosition;

}
