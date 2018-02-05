using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When staying still on open water, the player should sink.
/// This is achieved, by moving the player to a layer that does not collide with water.
/// </summary>
[RequireComponent(typeof(PlayerControls), typeof(Rigidbody))]
public class PlayerSinkBehaviour : MonoBehaviour {

    public float sinkAfterSeconds;

    [Layer]
    public int sinkingLayer;

    private float startSinkingTime;
    private PlayerControls _controls;

    void Start() {
        _controls = GetComponent<PlayerControls>();
        // allow the player some extra time at game start
        startSinkingTime = Time.time + 5 * sinkAfterSeconds;
    }

    void FixedUpdate() {
        if(_controls.getLastMovement().sqrMagnitude != 0) {
            startSinkingTime = Time.time + sinkAfterSeconds;
            gameObject.layer = 0; // Default
        }
        if(Time.time > startSinkingTime) {
            gameObject.layer = sinkingLayer;
        }
    }
}
