using UnityEngine;

/// <summary>
/// Basic controls for the player handles movement and rotation. Additional interaction with objects may be registered here but should be handled elsewhere.
/// </summary>
[RequireComponent (typeof (Rigidbody))]
public class PlayerControls : MonoBehaviour {
    public float movementSpeed;

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        // get inputs
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal") * 10 / 3f, 0f, Input.GetAxis("Vertical"));
        // set inputVector /= max(1, inputVector.magnitude) to disallow movement faster than 1 but allow smooth transitions
        if (inputVector.magnitude > 0)
        {
            inputVector /= Mathf.Max(1, inputVector.magnitude);
        }

        // factor in set speed and deltaTime to get translation for this tick
        Vector3 moveVector = inputVector * movementSpeed * Time.deltaTime;

        // move it
        _rigidbody.MovePosition(_rigidbody.position + moveVector);
    }
}
