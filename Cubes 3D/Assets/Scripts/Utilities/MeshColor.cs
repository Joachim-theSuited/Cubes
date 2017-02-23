using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script to set the base color of a mesh.
/// </summary>
[RequireComponent(typeof(MeshRenderer))]
public class MeshColor : MonoBehaviour {

    public Color color;

    void Start() {
        GetComponent<MeshRenderer>().material.color = color;
    }
}