using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustColliderToCloth : MonoBehaviour {

	private MeshCollider meshCollider;
	private Cloth cloth;

	private Mesh mesh;

	void Start () {
		meshCollider = GetComponent<MeshCollider>();
		cloth = GetComponent<Cloth>();
		mesh = GetComponent<MeshFilter>().mesh;
	}
	
	void Update () {
		if((int) Time.time % 10 == 0) {
			// print(cloth.vertices);
			if(mesh.vertices.Length > 0) {
				// print(cloth.vertices[0]);
				print(mesh.vertices[0]);
			}
		}


	}
}
