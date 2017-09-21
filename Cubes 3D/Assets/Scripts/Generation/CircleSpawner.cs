using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class CircleSpawner : MonoBehaviour {

	public float timeBetweenSpawns = 10f;
	public Transform circleTemplate;

	private float timeSinceLastSpawn = 0f;


	void Start() {
		timeSinceLastSpawn = timeBetweenSpawns;
	}

	// Update is called once per frame
	void Update () {
		timeSinceLastSpawn += Time.deltaTime;

		if(timeSinceLastSpawn >= timeBetweenSpawns) {
			Transform circle = (Transform)Instantiate(circleTemplate, transform.position, Quaternion.LookRotation(transform.forward));
			circle.localScale = new Vector3(1000f, 1f, 1000f);
			timeSinceLastSpawn = 0f;

			StartCoroutine(shrinkCircle(circle.gameObject, circle.localScale, Vector3.one, 60f));
		}
	}

	private IEnumerator shrinkCircle(GameObject thingToScale, Vector3 scaleFrom, Vector3 scaleTo, float scaleTime) {
		float timePassed = 0f;

		while(timePassed < scaleTime) {
			thingToScale.transform.localScale = Vector3.Lerp(scaleFrom, scaleTo, timePassed/scaleTime);

			yield return new WaitForFixedUpdate();
			timePassed += Time.deltaTime;
		}

		Destroy(thingToScale);
	}
}
