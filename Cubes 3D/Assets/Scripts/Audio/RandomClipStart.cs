using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This small script will start the playback of an AudioSource, that is also attached to this GameObject, at a random point.
/// 
/// Useful for ambient sounds, so they are not in sync and sound artifical.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class RandomClipStart : MonoBehaviour {

	void Start () {
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.time = Random.Range(0, audioSource.clip.length);
	}
}
