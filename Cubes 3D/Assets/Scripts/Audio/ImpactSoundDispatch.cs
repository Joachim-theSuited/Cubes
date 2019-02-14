using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ImpactSoundDispatch : MonoBehaviour {

	public AudioClip[] clips;

	private AudioSource audioSource;

	void Start() {
		this.audioSource = GetComponent<AudioSource>();
	}
	
	public void playRandomlyDrawn() {
		audioSource.clip = clips[Random.Range(0, clips.Length)];
		audioSource.Play();
	}

}
