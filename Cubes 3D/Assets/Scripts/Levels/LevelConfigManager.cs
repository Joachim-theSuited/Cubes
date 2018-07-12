using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConfigManager : Singleton<LevelConfigManager> {

	public LevelConfig config;

	public enum LoadOption {
		PushConfig,
		ClearLevelHistory,
	}

	public void Load(LevelConfig newConf, LoadOption opt) {
		var player = GameObject.FindWithTag(Tags.Player);
		if(player) {
			player.transform.position = newConf.playerStartPosition;
		}

		if(opt == LoadOption.PushConfig) {
			history.Push(config);
		} else if(opt == LoadOption.ClearLevelHistory) {
			history.Clear();
		}
		config = newConf;
		
		SceneManager.LoadScene(newConf.sceneID);
	}

	public void PopLevel() {
		Debug.Assert(history.Count > 0, "No level to return to was found.");

		config = history.Pop();
		
		var player = GameObject.FindWithTag(Tags.Player);
		if(player) {
			player.transform.position = config.playerStartPosition;
		}

		SceneManager.LoadScene(config.sceneID);
	}
	
	protected LevelConfigManager() {}

	Stack<LevelConfig> history;

	void Awake() {
		config = (LevelConfig)ScriptableObject.CreateInstance(typeof(LevelConfig));
		config.sceneID = SceneManager.GetActiveScene().buildIndex;
		var player = GameObject.FindWithTag(Tags.Player);
		if(player) {
			config.playerStartPosition = player.transform.position;
		}

		history = new Stack<LevelConfig>();
	}

}
