using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This simple behaviour lets interaction indicators rotate and bob in place to be more visible.
/// </summary>
public class RotateBobIndicator : MonoBehaviour {

	public Vector3 rotationPerFrame = new Vector3(0, 0, 1);
	public Vector3 translationPerFrame = new Vector3(0, 0, 0.001f);
	
	private Vector3 startTranslation;

	void Start() {
		startTranslation = transform.localPosition;
	}


	// Update is called once per frame
	void Update () {
		transform.Rotate(rotationPerFrame);
		transform.localPosition = startTranslation + translationPerFrame * Mathf.Sin(Time.time);
	}
}
