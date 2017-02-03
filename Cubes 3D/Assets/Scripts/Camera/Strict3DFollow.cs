using UnityEngine;

/// <summary>
/// Follow a given Transform while maintaining an offset. Does not affect rotation.
/// </summary>
public class Strict3DFollow : MonoBehaviour {

    public Transform objectToFollow;

    public Vector3 offset;
	
	// Update is called once per frame
	void Update () {
        transform.position = objectToFollow.position + offset;
	}
}
