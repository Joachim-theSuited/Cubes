using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConfigManager : Singleton<LevelConfigManager> {

	public LevelConfig config;

	public enum LoadOption {
		PushConfig,
		ClearLevelHistory
	}

	public void Load(LevelConfig newConf, LoadOption opt) {
		if(opt == LoadOption.PushConfig) {
			var player = GameObject.FindWithTag(Tags.Player);
			var pushConfig = ScriptableObject.Instantiate(config);
			pushConfig.playerStartPosition = player.transform.position;
			history.Push(pushConfig);	
		} else if(opt == LoadOption.ClearLevelHistory) {
			history.Clear();
		}
		
		config = newConf;
		SceneManager.LoadScene(newConf.sceneID);
	}

	public void PopLevel() {
		Debug.Assert(history.Count > 0, "No level to return to was found.");
		config = history.Pop();
		SceneManager.LoadScene(config.sceneID);
	}
	
	protected LevelConfigManager() {}

	Stack<LevelConfig> history;

	void Awake() {
		config = ScriptableObject.CreateInstance<LevelConfig>();
		config.sceneID = SceneManager.GetActiveScene().buildIndex;
		var player = GameObject.FindWithTag(Tags.Player);
		config.playerStartPosition = player.transform.position;
		
		history = new Stack<LevelConfig>();
	}

	void UpdatePlayerPositionCallback(Scene fromScene, Scene toScene) {
		var player = GameObject.FindWithTag(Tags.Player);
		player.transform.position = config.playerStartPosition;
	
	}

	void OnEnable() {
		SceneManager.activeSceneChanged += UpdatePlayerPositionCallback;
	}

	void OnDisable() {
		SceneManager.activeSceneChanged -= UpdatePlayerPositionCallback;
	}

}
