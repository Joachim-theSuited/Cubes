using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Point the transform of this object at the cursor registered by the main camera.
/// </summary>
public class PointAtCursor : MonoBehaviour {

	public Vector3 cursorPosition;
	public Vector3 lastMousePosition;
	public float cursorSpeed = 1;

	void Start() {
		lastMousePosition = Input.mousePosition;
		cursorPosition = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0);
	}

	void Update () {
		if(Input.mousePosition != lastMousePosition) {
			lastMousePosition = Input.mousePosition;
			cursorPosition = Input.mousePosition;
		} else if(Input.GetJoystickNames().Length > 0) {
			cursorPosition += new Vector3(Input.GetAxis(Inputs.Horizontal), Input.GetAxis(Inputs.Vertical), 0) * cursorSpeed;
		}

		RaycastHit hit;
		if(cursorRaycast(out hit)) {
			transform.LookAt(hit.point);
		}
	}

	public bool cursorRaycast(out RaycastHit hit) {
		return Physics.Raycast(Camera.main.ScreenPointToRay(cursorPosition), out hit);
	}
}
