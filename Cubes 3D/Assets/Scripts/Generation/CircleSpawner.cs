using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour {

    public float timeBetweenSpawns = 10f;
    public float shrinkSpeed;
    public float spawnScale;
    public Transform circleTemplate;

    /// <summary>
    /// Function for mapping a random value to a height, at which the object will be spawned
    /// </summary>
    public AnimationCurve heightCDF;

    void Start() {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while(true) {
            Transform circle = (Transform)Instantiate(circleTemplate, transform.position, Quaternion.LookRotation(transform.forward));
            circle.localScale = new Vector3(spawnScale, 1f, spawnScale);
            circle.Translate(0, heightCDF.Evaluate(Random.value), 0);

            StartCoroutine(shrinkCircle(circle.gameObject, circle.localScale, Vector3.one, shrinkSpeed));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private IEnumerator shrinkCircle(GameObject thingToScale, Vector3 scaleFrom, Vector3 scaleTo, float scaleTime) {
        float timePassed = 0f;

        while(timePassed < scaleTime) {
            thingToScale.transform.localScale = Vector3.Lerp(scaleFrom, scaleTo, timePassed / scaleTime);

            yield return new WaitForFixedUpdate();
            timePassed += Time.deltaTime;
        }

        Destroy(thingToScale);
    }
}
