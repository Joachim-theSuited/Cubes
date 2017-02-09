using UnityEngine;
using System.Collections;

/// <summary>
/// The AreaChangeTrigger is designed to be attached to an Object enclosing GameObjects of a room.
/// One of the enclosed GameObjects must be a sprite that can cover the room.
/// Such an Object is referred to as a curtain, which can be turned opaque to hide a room the player is not in.
/// </summary>
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshRenderer))]
public class AreaChangeTrigger : MonoBehaviour
{
    private static IEnumerator RunningCoroutine;

    public float fadeTime = 1f;

    void Start() {
        //so it needn't be enabled in the editor
        GetComponent<MeshRenderer>().material.color = Color.black;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // stop running fading process if any
            if(RunningCoroutine != null)
            {
                StopCoroutine(RunningCoroutine);
            }
            RunningCoroutine = ColorUtilities.FadeColorOfMaterial(GetComponent<MeshRenderer>().material, Color.black, Color.clear, fadeTime);
            StartCoroutine(RunningCoroutine);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // stop running fading process if any
            if (RunningCoroutine != null)
            {
                StopCoroutine(RunningCoroutine);
            }
            RunningCoroutine = ColorUtilities.FadeColorOfMaterial(GetComponent<MeshRenderer>().material, Color.clear, Color.black, fadeTime);
            StartCoroutine(RunningCoroutine);
        }
    }
}