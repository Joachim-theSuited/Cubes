using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(CurvedCamera))]
public class DungeonCamera : MonoBehaviour {
 
	public float targetHeigth;
	public float transitionTime;

	GameObject player;
	CurvedCamera curvedCamera;
	Vector3 oldRelativePosition;

	Vector3 ccNearPoint;
	Vector3 ccSideOffset;
	float ccZoom;
	float ccZoomSpeed;
	float tLerp;

	 // disable meshes of the head when zoomed in
	string[] componentsToDeactivate = {
		"PersistentPlayer/PlayerRig/Kopf",
		"PersistentPlayer/PlayerRig/Hair",
	};

	// Use this for initialization
	void Awake () {
		enabled = false;
		player = GameObject.FindWithTag(Tags.Player);
		curvedCamera = GetComponent<CurvedCamera>();
	}

	void OnEnable()
	{
		if (zoomCoroutine != null)
		{
			StopCoroutine(zoomCoroutine);
			zoomCoroutine = null;
		}
		else
		{
			ccNearPoint = curvedCamera.nearPoint;
			ccSideOffset = curvedCamera.sideOffset;
			ccZoom = curvedCamera.zoom;

			ccZoomSpeed = curvedCamera.zoomSpeed;
			curvedCamera.zoomSpeed = 0;
		}
		tLerp = 0;
	}

	Coroutine zoomCoroutine;
	void OnDisable()
	{
		tLerp = 0;
		zoomCoroutine = StartCoroutine(ZoomOut());
	}

	IEnumerator ZoomOut()
	{
		foreach (string componentName in componentsToDeactivate)
		{
			Transform subcomponent = player.transform.Find(componentName);
			if (subcomponent)
				subcomponent.gameObject.SetActive(true);
			else
				Debug.Log("Did not find " +  componentName + " on player");
		}

		do 
		{
			tLerp += Time.deltaTime / transitionTime;
			curvedCamera.nearPoint =  Vector3.Lerp(new Vector3(0, targetHeigth, 0), ccNearPoint, tLerp);
			curvedCamera.sideOffset = Vector3.Lerp(Vector3.zero, ccSideOffset, tLerp);
			curvedCamera.zoom = Mathf.Lerp(0, ccZoom, tLerp);
			Vector3 targetPosition = player.transform.position;
			yield return new WaitForEndOfFrame();
		}
		while (tLerp < 1);

		curvedCamera.zoomSpeed = ccZoomSpeed;
		curvedCamera.enabled = true;
		zoomCoroutine = null;
	}
	
	// zooms in after activation
	void Update () 
	{
		tLerp += Time.deltaTime / transitionTime;
		curvedCamera.nearPoint =  Vector3.Lerp(ccNearPoint, new Vector3(0, targetHeigth, 0), tLerp);
		curvedCamera.sideOffset = Vector3.Lerp(ccSideOffset, Vector3.zero, tLerp);
		curvedCamera.zoom = Mathf.Lerp(ccZoom, 0, tLerp);
		if (tLerp > 1)
		{
			foreach (string componentName in componentsToDeactivate)
			{
				Transform subcomponent = player.transform.Find(componentName);
				if (subcomponent)
					subcomponent.gameObject.SetActive(false);
				else
					Debug.Log("Did not find " +  componentName + " on player");
			}
		}
	}
}
