using UnityEngine;
using System;

/// <summary>
/// Basic controls for the player handles movement and rotation. Additional interaction with objects may be registered here but should be handled elsewhere.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerControls : MonoBehaviour {
    public float movementSpeed;
    public Camera referenceCamera;

    private Rigidbody _rigidbody;
    private AudioSource _wooshSource;
    public float wooshVolumeFactor;

	private Vector3 lastMovement;

	/// <summary>
	/// The camera rotation speed. Could be modified by user preferences.
	/// </summary>
	public float cameraRotationSpeed = 5f;

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _wooshSource = GetComponent<AudioSource>();

		// hide and lock cursor for player rotation
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F12))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        // get inputs; movement relative to camera
		Vector3 inputVector = referenceCamera.transform.right * Input.GetAxis(Inputs.Horizontal) * 10 / 3f
			+ referenceCamera.transform.forward * Input.GetAxis(Inputs.Vertical);
        inputVector.y = 0; //move horizontally only
        // set inputVector /= max(1, inputVector.magnitude) to disallow movement faster than 1 but allow smooth transitions
        if(inputVector.magnitude > 0) {
            inputVector /= Mathf.Max(1, inputVector.magnitude);
        }

        // factor in set speed and deltaTime to get translation for this tick
        Vector3 moveVector = inputVector * movementSpeed * Time.deltaTime;

        // move it
        _rigidbody.MovePosition(_rigidbody.position + moveVector);

		// activate animation
		Animator modelAnimator = GetComponentInChildren<Animator>();
        if(modelAnimator != null) {
            modelAnimator.SetInteger("X", Mathf.CeilToInt(Input.GetAxis(Inputs.Horizontal)));
    		modelAnimator.SetInteger("Y", Mathf.CeilToInt(Input.GetAxis(Inputs.Vertical)));
        }

        // rotate player with mouse movement
		Vector3 rotateBy = new Vector3(0, Input.GetAxis(Inputs.LookX), 0) * cameraRotationSpeed;
		_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotateBy));

		lastMovement = moveVector;

        // create wooshSound while moving
        _wooshSource.volume = (moveVector.magnitude / Time.deltaTime / movementSpeed) * wooshVolumeFactor;
    }

	public Vector3 getLastMovement() {
		return lastMovement;
	}
}
