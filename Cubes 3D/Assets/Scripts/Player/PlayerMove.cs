using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour {

    // Use this for initialization
    void Start() {
		
    }
	
    // Update is called once per frame
    void Update() {
        GetComponent<CharacterController>().Move(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
    }
}
