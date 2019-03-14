using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraToggle : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.Player)
		{
			Camera.main.GetComponent<DungeonCamera>().enabled = true;
			//Camera.main.GetComponent<CurvedCamera>().enabled = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == Tags.Player)
		{
			Camera.main.GetComponent<DungeonCamera>().enabled = false;
			//Camera.main.GetComponent<CurvedCamera>().enabled = true;
		}
	}

}
