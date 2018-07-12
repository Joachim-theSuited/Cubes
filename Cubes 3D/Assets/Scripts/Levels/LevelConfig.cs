using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="level.asset", menuName="Custom/Level Config")]
public class LevelConfig : ScriptableObject {

	public string sceneName;

	public int randomSeedOffset;

	public Vector3 playerStartPosition;

}
