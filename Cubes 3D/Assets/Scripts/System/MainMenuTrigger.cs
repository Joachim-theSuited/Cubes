using UnityEngine;

public class MainMenuTrigger : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Cancel") > 0) {
			Application.Quit();
		}
	}
}
