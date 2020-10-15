using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMap : MonoBehaviour {

	public GameObject minimapCover;

	void Interact()
	{
		minimapCover.BroadcastMessage("Uncover", SendMessageOptions.DontRequireReceiver);
	}

}
