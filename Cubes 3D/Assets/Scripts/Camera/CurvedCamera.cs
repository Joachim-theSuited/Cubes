using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

/// <summary>
/// Script for a camera that follows an object.
/// The camera can be zoomed in and out through the mouse wheel.
/// The near and far point of the camera are interpolated with a parabola.
/// The viewing direction is determined by the derivative along the camera path.
/// </summary>
public class CurvedCamera : MonoBehaviour {

    public Transform objectToFollow;

    public Vector3 nearPoint;
    public Vector3 farPoint;
    public Vector3 sideOffset;

    [Range(0, 1)]
    public float fadeOutFollowedDistance;

    Vector3 delta {
        get { return farPoint - nearPoint; }
    }

    public float zoomSpeed = 100;

    [Range(0, 1)]
    public float zoom;

    void Update() {
        zoom -= Input.GetAxis(Inputs.LookY) * zoomSpeed / 100;
        zoom = Mathf.Clamp(zoom, 0, 1);

        transform.position = objectToFollow.position + getOffset(zoom);
        transform.LookAt(transform.position - objectToFollow.rotation * new Vector3(delta.x, delta.y * zoom, delta.z));


        if(zoom < fadeOutFollowedDistance) {
            foreach(Renderer r in objectToFollow.GetComponentsInChildren(typeof(MeshRenderer))) {
                r.enabled = false;
            }
        } else {
            foreach(Renderer r in objectToFollow.GetComponentsInChildren(typeof(MeshRenderer))) {
                r.enabled = true;
            }
        }
    }

    public Vector3 getOffset(float zoomFactor) {
        return objectToFollow.rotation * (sideOffset + nearPoint + new Vector3(delta.x * zoomFactor, delta.y * zoomFactor * zoomFactor, delta.z * zoomFactor));
    }

}

#if (UNITY_EDITOR)
[CustomEditor(typeof(CurvedCamera))]
public class CurvedCameraeditor : Editor {

    void OnSceneGUI() {
        CurvedCamera camera = target as CurvedCamera;

        if(camera == null || camera.objectToFollow == null)
            return;

        Handles.color = Color.green;

        camera.nearPoint = Handles.PositionHandle(camera.nearPoint, Quaternion.identity);
        camera.farPoint = Handles.PositionHandle(camera.farPoint, Quaternion.identity);

        for(int i = 0; i < 10; i++)
            Handles.DrawLine(camera.objectToFollow.position + camera.getOffset(i / 10f), camera.objectToFollow.position + camera.getOffset((i + 1) / 10f));
    }

}
#endif