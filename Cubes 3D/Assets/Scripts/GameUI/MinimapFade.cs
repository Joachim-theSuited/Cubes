using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class MinimapFade : MonoBehaviour {

	[Tooltip("Duration of the fade effect in seconds")]
	public float fadeDuration;

	RawImage uiMinimap;

	void Start()
	{
		uiMinimap = GameObject.Find("Minimap").GetComponent<RawImage>();
		// hide at start
		uiMinimap.CrossFadeAlpha(0, 0, false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.Player)
		{
			uiMinimap.CrossFadeAlpha(1, fadeDuration, false);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == Tags.Player)
		{
			uiMinimap.CrossFadeAlpha(0, fadeDuration, false);
		}
	}
}
