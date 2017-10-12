using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Changes the material color of the attached Renderer when damage is received.
/// </summary>
[RequireComponent(typeof(Renderer))]
public class DamageColor : MonoBehaviour, AbstractDamageReceiver {

    /// <summary>
    /// The color to change to, upon taking damage.
    /// </summary>
    public Color damageColor = Color.red;

    /// <summary>
    /// How long the effect should last in seconds.
    /// Non-positive values induce permanent change.
    /// </summary>
    public float duration;

    /// <summary>
    /// The intensity of the color change.
    /// 0 is no change, 1 means the original color is completely replaced.
    /// </summary>
    [Range(0, 1)]
    public float intensity;

    //the coroutine is stored, to avoid animation overlap
    Coroutine colorBlend;

    public void ReceiveDamage(float dam, GameObject src) {
        if(colorBlend == null) // we don't want overlapping animation for now
            colorBlend = StartCoroutine(ColorBlend());
    }

    IEnumerator ColorBlend() {
        Material mat = GetComponent<Renderer>().material;
        Color origColor = mat.color;

        //apply coloring
        mat.color *= 1 - intensity;
        mat.color += intensity * damageColor;

        if(duration > 0) { //non-positive duration means permanent change
            //restore old color after the given period
            yield return new WaitForSeconds(duration);
            mat.color = origColor;
        }
        colorBlend = null; //release animation 'lock'
    }
}
