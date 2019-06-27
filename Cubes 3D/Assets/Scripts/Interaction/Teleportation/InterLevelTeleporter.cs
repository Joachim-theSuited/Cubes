using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class InterLevelTeleporter : MonoBehaviour {

	public LevelConfig targetLevel;
	public LevelConfigManager.LoadOption option;

	void Start()
	{
		SaveData saveData = GameProgressionPersistence.loadProgress();

		if (saveData.IsUnlocked(targetLevel.number))
		{
			gameObject.SetActive(true);

			TextMesh tm = gameObject.GetComponentInChildren<TextMesh>();
			if (tm)
			{
				string text = "Level " + targetLevel.number;
				if (saveData.GetBestTime(targetLevel.number) > 0)
				{
					text += "\n " + saveData.GetBestTime(targetLevel.number) + "s";
				}
				tm.text = text;
			}
			else
				Debug.Log("No text mesh found");
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(AcquirePersistentPlayer.PERSISTENT_PLAYER != null)
		{
			DontDestroyOnLoad(AcquirePersistentPlayer.PERSISTENT_PLAYER);

			AudioSource audioSource = GetComponent<AudioSource>();
			audioSource.Play();
			StartCoroutine(WaitForAudioPlayed(audioSource.clip.length));
			
			GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
			player.GetComponent<PlayerControls>().enabled = false;
			player.GetComponent<PlayerJumpBehaviour>().enabled = false;
			player.GetComponent<PlayerSprintBehaviour>().enabled = false;
		}
    }

    IEnumerator WaitForAudioPlayed(float toWait)
	{
        float waited = 0;
        Camera mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<Camera>();
        float originalFOV = mainCamera.fieldOfView;
        while (waited < toWait)
		{
            mainCamera.fieldOfView = Mathf.Lerp(originalFOV, 1, Mathf.Pow(waited / toWait, 2));
            yield return new WaitForFixedUpdate();
            waited += Time.deltaTime;
        }

		LevelConfigManager.Instance.Load(targetLevel, option);
    }
}
