using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rotates the HUD compass when the player rotates.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class CompassRotation : MonoBehaviour {

	public Transform player;
	public float spriteWidth = 1024 * 0.75f;
	public float zoomFactor;

	private Vector3 initialPosition;
	private Vector3 angularShift;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		angularShift = new Vector3(spriteWidth * zoomFactor / 360f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = initialPosition - player.eulerAngles.y * angularShift;
	}
}
