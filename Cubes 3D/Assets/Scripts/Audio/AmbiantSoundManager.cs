using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbiantSoundManager : MonoBehaviour {

	private HashSet<GameObject> enteredZones = new HashSet<GameObject>();
	private AudioSource audioSource;
	private int lastCount = 0;

	public void enterZone(GameObject zone) {
		print("entered zone: " + zone.name);
		enteredZones.Add(zone);
	}

	public void leaveZone(GameObject zone) {
		print("left zone: " + zone.name);
		enteredZones.Remove(zone);
	}

	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		if(enteredZones.Count != lastCount) {
			if(enteredZones.Count > 0) {
				StartCoroutine(FadeAudioSource.FadeAudioSourceFromTo(audioSource, 1, 0, 3, () => {
					audioSource.Pause();
				}));
			} else {
				audioSource.UnPause();
				StartCoroutine(FadeAudioSource.FadeAudioSourceFromTo(audioSource, 0, 1, 3, () => {}));
			}
		}
		lastCount = enteredZones.Count;
	}
}
