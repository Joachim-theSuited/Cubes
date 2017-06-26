using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroy the attached GameObject some time after this script is enabled.
/// </summary>
public class DestroyDelayed : MonoBehaviour {

    /// <summary>
    /// The delay in seconds.
    /// </summary>
    public float delay;

    void OnEnable() {
        Destroy(gameObject, delay);
    }
	
}
