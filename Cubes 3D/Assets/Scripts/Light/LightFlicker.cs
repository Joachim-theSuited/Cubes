using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {

	private Light _light;
	private float originalIntensity;
	private Vector3 originalPosition;

	public float flickerRange;
	public float positionFlicker;

	// Use this for initialization
	void Start () {
		_light = GetComponent<Light>();
		originalIntensity = _light.intensity;
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_light.intensity = originalIntensity + Random.Range(-flickerRange, flickerRange);
		Vector3 offset = (transform.position + Random.insideUnitSphere * positionFlicker) - originalPosition;
		if(offset.magnitude < 2*positionFlicker) {
			transform.position = originalPosition + offset;
		}
	}
}
