using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Finds a game object marked as Persistent and sets it as the player object.
/// </summary>
public class AcquirePersistentPlayer : MonoBehaviour {

	void Start () {
		GameObject foundObject = GameObject.FindWithTag(Tags.Persistent);
		if (foundObject != null) {

			// this is a semi-dirty hack, that resets the mesh
			// necessary to reset cloth components, which went crazy on scene change
			Destroy(GameObject.FindWithTag(Tags.DebugDefault));

			GameObject newInstance = Instantiate(foundObject);
			newInstance.transform.parent = transform;
			newInstance.transform.localPosition = Vector3.zero;
			newInstance.transform.localRotation = Quaternion.identity;

			Destroy(foundObject);
		}
	}
}
