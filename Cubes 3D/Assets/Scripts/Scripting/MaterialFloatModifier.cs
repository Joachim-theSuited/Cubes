using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows modifying a float property of a material.
/// Does a simple Lerp between two values over a given time.
/// </summary>
public class MaterialFloatModifier : MonoBehaviour {

    /// <summary>
    /// The name of the parameter (in the code, may differ from the one displayed).
    /// </summary>
    public string paramName;

    public float duration;
    public float fromVal;
    public float toVal;

    void OnEnable() {
        StartCoroutine(anim());
    }

    IEnumerator anim() {
        Material mat = GetComponent<Renderer>().material;
        TimeLerper<float> lerper = new TimeLerper<float>(Mathf.Lerp, fromVal, toVal, duration);

        while(lerper.isNotDone) {
            mat.SetFloat(paramName, lerper.Current());
            yield return new WaitForFixedUpdate();
        }
    }

}
