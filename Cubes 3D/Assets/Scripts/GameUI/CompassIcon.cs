using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that sets the required properties for the CompassIcon material.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class CompassIcon : MonoBehaviour {

    GameObject _player;
    Material _material;

    void Start() {
        _player = GameObject.FindWithTag(Tags.Player);
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update() {
        Vector3 worldDirection = _player.transform.InverseTransformPoint(transform.position);
        worldDirection.y = 0; // ignore height differences

        _material.SetFloat("playerDistance", worldDirection.magnitude);

        float angle = Mathf.Atan2(worldDirection.z, worldDirection.x);
        angle -= Mathf.PI / 2; // straight ahead should be zero
        angle /= (2 * Mathf.PI); // normalise
        // for the wrap-around we want the parameter to be in [-0.75, 0.25],
        if(angle > 0.25)
            angle -= 1;
        if(angle < -0.75)
            angle += 1;
        
        _material.SetFloat("screenOffset", angle);
    }
}
