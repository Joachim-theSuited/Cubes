using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Point the transform of this object at the cursor registered by the main camera.
/// </summary>
public class PointAtCursor : MonoBehaviour {

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)) {
			transform.LookAt(hit.point);
		}
	}
}
