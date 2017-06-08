using UnityEngine;

/// <summary>
/// Basic controls for the player handles movement and rotation. Additional interaction with objects may be registered here but should be handled elsewhere.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerControls : MonoBehaviour {
    public float movementSpeed;
    public Camera referenceCamera;

    Rigidbody _rigidbody;

	/// <summary>
	/// The camera rotation speed. Could be modified by user preferences.
	/// </summary>
	public float cameraRotationSpeed = 5f;

    // Use this for initialization
    void Start() {
        _rigidbody = GetComponent<Rigidbody>();

		// hide and lock cursor for player rotation
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        // get inputs; movement relative to camera
        Vector3 inputVector = referenceCamera.transform.right * Input.GetAxis("Horizontal") * 10 / 3f
                              + referenceCamera.transform.forward * Input.GetAxis("Vertical");
        inputVector.y = 0; //move horizontally only
        // set inputVector /= max(1, inputVector.magnitude) to disallow movement faster than 1 but allow smooth transitions
        if(inputVector.magnitude > 0) {
            inputVector /= Mathf.Max(1, inputVector.magnitude);
        }

        // factor in set speed and deltaTime to get translation for this tick
        Vector3 moveVector = inputVector * movementSpeed * Time.deltaTime;

        // move it
        _rigidbody.MovePosition(_rigidbody.position + moveVector);

        // rotate player with mouse movement
		Vector3 rotateBy = new Vector3(0, Input.GetAxis(Inputs.MouseX), 0) * cameraRotationSpeed;
		_rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotateBy));

    }
}
