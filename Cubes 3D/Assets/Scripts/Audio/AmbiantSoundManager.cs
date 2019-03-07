using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AmbiantSoundManager : MonoBehaviour {

	private HashSet<GameObject> enteredZones = new HashSet<GameObject>();
	private AudioSource audioSource;

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
	
	// Update is called once per frame
	void Update () {
		if(enteredZones.Count > 0) {
			audioSource.Pause();
		} else {
			audioSource.UnPause();
		}
	}
}
