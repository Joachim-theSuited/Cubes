using UnityEngine;
using System;

/// <summary>
/// Basic controls for the player handles movement and rotation. Additional interaction with objects may be registered here but should be handled elsewhere.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour {
    public float turnSpeed;
    public Camera referenceCamera;

    Rigidbody _rigidbody;
    Animator modelAnimator;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        modelAnimator = GetComponentInChildren<Animator>();

        // hide and lock cursor for player rotation
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // rotate player with mouse movement
        float angle = Input.GetAxis(Inputs.MouseX) * turnSpeed * Time.deltaTime;
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, angle, 0));

        Vector3 moveVector = (Mathf.Cos(Mathf.Deg2Rad * angle) - 1) * transform.forward + Mathf.Sin(Mathf.Deg2Rad * angle) * transform.right;
        _rigidbody.MovePosition(_rigidbody.position + moveVector);

        // activate animation
        modelAnimator.SetInteger("X", (int)angle);
    }
}
