using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component modifies a given mesh so it appears to have rolling waves.
/// It also dents the water where a given GameObject touches it.
/// </summary>
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
public class WaterDent : MonoBehaviour {

	private MeshFilter meshFilter;
	private MeshCollider meshCollider;
	public GameObject dentingObject;
	public float waveAmplitude;
	public float waveFrequency;

	private float[] waveRandoms;


	void Start() {
		meshFilter = GetComponent<MeshFilter>();
		meshCollider = GetComponent<MeshCollider>();

		waveRandoms = new float[meshFilter.mesh.vertices.Length];
		for(int i = 0; i < waveRandoms.Length; i++) {
			waveRandoms[i] = Random.Range(-waveAmplitude*0.5f, waveAmplitude*0.5f);
		}
	}

	void Update() {
		Vector3[] newVertices = meshFilter.mesh.vertices;
		for(int i = 0; i < newVertices.Length; i++) {
			float collisionDistance = (transform.TransformPoint(newVertices[i]) - dentingObject.transform.position).magnitude;
			if(collisionDistance < 2f) {
				newVertices[i].z = Mathf.Lerp(-1, 0, Mathf.SmoothStep(0, 1, Mathf.Pow(collisionDistance, 2) / 2f));
			} else {
				newVertices[i].z = ((Mathf.Sin(newVertices[i].x * waveFrequency + Time.time % 100) + 1) * waveAmplitude) + waveRandoms[i];
				waveRandoms[i] += Mathf.Clamp(Random.Range(-waveAmplitude*0.01f, waveAmplitude*0.01f), -waveAmplitude*0.75f, waveAmplitude*0.75f);
			}
		}
		meshFilter.mesh.vertices = newVertices;
	}


}
