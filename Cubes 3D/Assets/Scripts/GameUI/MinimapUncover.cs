using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MinimapUncover : MonoBehaviour
{
	public float fadeDuration;

	int materialColorId;

	void Start()
	{
		materialColorId = Shader.PropertyToID("_Color");
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == Tags.Player)
			Uncover();
    }

	void Uncover()
	{
		StartCoroutine(FadeColor());
        Destroy(gameObject, fadeDuration);
	}

	IEnumerator FadeColor()
	{
		while(true)
		{
			var material = GetComponent<MeshRenderer>().material;
			var color = material.GetColor(materialColorId);
			color.a -= Time.deltaTime / fadeDuration;
			material.SetColor(materialColorId, color);
			yield return new WaitForEndOfFrame();
		}
	}
}
