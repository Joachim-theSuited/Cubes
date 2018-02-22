using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When staying still on open water, the player should sink.
/// This is achieved, by moving the player to a layer that does not collide with water.
/// </summary>
[RequireComponent(typeof(PlayerControls), typeof(Rigidbody), typeof(PlayerJumpBehaviour))]
public class PlayerSinkBehaviour : MonoBehaviour {
    
    public float sinkAfterSeconds;

    [Tooltip("Default and sinking drag")]
    public Vector2 drags;

    private float startSinkingTime;
    private PlayerControls _controls;
    private Rigidbody _rigidbody;

    void Start() {
        _controls = GetComponent<PlayerControls>();
        _rigidbody = GetComponent<Rigidbody>();
        // allow the player some extra time at game start
        startSinkingTime = Time.time + 5 * sinkAfterSeconds;
    }

    void FixedUpdate() {
        // reset if there was movement or the player is on solid ground
        if(_controls.getLastMovement().sqrMagnitude != 0 || Physics.CheckSphere(transform.position, .1f, LayerMask.GetMask(CubesLayers.Floors))) {
            startSinkingTime = Time.time + sinkAfterSeconds;
            gameObject.layer = 0; // Default
            _rigidbody.drag = drags.x;
        }
        if(Time.time > startSinkingTime) {
            gameObject.layer = LayerMask.NameToLayer(CubesLayers.Sinking);
            _rigidbody.drag = drags.y;
        }
    }
}
