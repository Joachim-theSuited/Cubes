using UnityEngine;
using System.Collections;

/// <summary>
/// A door that needs to be unlocked with a Key.
/// When unlocked it will slide aside.
/// </summary>
public class LockedSlidingDoor : MonoBehaviour {

    public Vector3 slideBy;

    public float slideTime = 1;

    float speed;
    Vector3 closed;
    Vector3 open;

    void Start() {
        speed = slideBy.magnitude / slideTime;
        closed = transform.position;
        open = transform.position + slideBy;
    }

    public void Unlock() {
        StopCoroutine("Slide");
        StartCoroutine(Slide(open));
    }

    public void Lock() {
        StopCoroutine("Slide");
        StartCoroutine(Slide(closed));
    }

    IEnumerator Slide(Vector3 towards) {
        float startTime = Time.time;
        Vector3 startPosition = transform.position;
        float time = (startPosition - towards).magnitude / speed;

        while(transform.position != towards) {
            transform.position = Vector3.Lerp(startPosition, towards, (Time.time - startTime) / time);
            yield return new WaitForEndOfFrame();
        }
    }
}
