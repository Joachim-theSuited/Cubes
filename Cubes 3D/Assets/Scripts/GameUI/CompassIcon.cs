using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that sets the required properties for the CompassIcon material.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class CompassIcon : MonoBehaviour {

    // TODO is this feature actually helpful?
    [Tooltip("When the player is closer than this, always show the icon in center")]
    public float focusDistance;

    GameObject _player;
    Material _material;

    int playerDistanceID;
    int screenOffsetID;

    void Start() {
        _player = GameObject.FindWithTag(Tags.Player);
        _material = GetComponent<SpriteRenderer>().material;

        playerDistanceID = Shader.PropertyToID("playerDistance");
        screenOffsetID = Shader.PropertyToID("screenOffset");
    }

    void Update() {
        Vector3 worldDirection = _player.transform.InverseTransformPoint(transform.position);
        worldDirection.y = 0; // ignore height differences

        float distance = worldDirection.magnitude;
        _material.SetFloat(playerDistanceID, distance);

        if(distance < focusDistance) {
            _material.SetFloat(screenOffsetID, 0);
        } else {
            float angle = Mathf.Atan2(worldDirection.z, worldDirection.x);
            angle -= Mathf.PI / 2; // straight ahead should be zero
            angle /= 2 * Mathf.PI; // normalise
            // for the wrap-around we want the parameter to be in [-0.75, 0.25],
            if(angle > 0.25f)
                angle -= 1;
            if(angle < -0.75f)
                angle += 1;

            // gradually shift icon into focus, when the player closes in
            if(distance < 2 * focusDistance) {
                // the shift in the angle needs to be based on the [-.5, .5] range
                float shiftAngle = angle > -0.5f ? angle : (angle + 1);
                angle -= shiftAngle * (2 - distance / focusDistance);
            }
                        
            _material.SetFloat(screenOffsetID, angle);
        }
    }
}
