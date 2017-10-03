using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Player jump behaviour.
/// Allows controlled player to jump to a certain height based on global gravity; the higher the gravity, the higher you can jump
/// </summary>
[RequireComponent(typeof(PlayerControls))]
public class PlayerJumpBehaviour : MonoBehaviour {

	private Rigidbody _rigidbody;
	private bool canJump = true;

	public float jumpForce = 250f;

	// Use this for initialization
	void Start () {
		_rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(canJump && Input.GetButtonDown(Inputs.Jump)) {
			_rigidbody.AddForce(Vector3.up * jumpForce);
			canJump = false;
		}
	}

	void OnCollisionEnter(Collision collision) {
		// when landing on a floor reenable jumping
		if(1 << collision.gameObject.layer == LayerMask.GetMask("Floors")){
			canJump = true;
		}
	}


}
