using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Disables cpu culling for the camera, as we position objects inside the shader.
/// </summary>
[RequireComponent(typeof(Camera))]
public class CompassIconCamera : MonoBehaviour {

    void Start() {
        var cam = GetComponent<Camera>();
        cam.cullingMatrix = Matrix4x4.zero;
    }

}
