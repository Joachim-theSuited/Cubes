using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleLeash : MonoBehaviour {

	[Tooltip("Maximum allowed distance for the player")]
	[NonNegative]
	public float maximumReach;

	[Tooltip("Up to which distance the movement is unrestricted. After this the leash starts pulling back.")]
	[Range(0, 1)]
	public float freeReachRatio = .5f;

	float _freeReach {
		get { return maximumReach * freeReachRatio; }
	}

	Rigidbody _playerRB;

	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
		_playerRB = player.GetComponent<Rigidbody>();
		Debug.Assert(_playerRB != null, "No Rigidbody found on Player.");
	}
	
	void Update () {
		// if player is out of bounds -> hard reset
		if(Vector3.Distance(_playerRB.position, transform.position) > maximumReach) {
			Vector3 playerDirection =  _playerRB.position - transform.position;
			playerDirection.Normalize();
			playerDirection *= maximumReach;
			_playerRB.MovePosition(transform.position + playerDirection);
		}
		
		// if player is past free reach -> pull back
		float playerDistance = Vector3.Distance(_playerRB.position, transform.position);
		if(playerDistance > _freeReach) {
			// first determine how much of the available stretch is used
			float stretchDistance = playerDistance - _freeReach;
			float availableStretch = maximumReach - _freeReach;
			float stretchRatio = stretchDistance / availableStretch;
			// then we pull back accordingly
			Vector3 playerDirection =  _playerRB.position - transform.position;
			playerDirection.Normalize();
			float force = 10 + stretchRatio*50; //magic numbers
			// instead of force, reducing velcity might be an option, too
			_playerRB.AddForce(-force * playerDirection);
		}
	}

	// indicate reach with spheres
	void OnDrawGizmosSelected() {
		Gizmos.color = new Color(1, 1, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, _freeReach);
		Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawSphere(transform.position, maximumReach);
    }
}
