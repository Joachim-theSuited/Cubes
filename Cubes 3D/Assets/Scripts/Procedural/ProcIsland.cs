using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
public class ProcIsland : MonoBehaviour {

    public int meshResolution;
    public float size;

    public float topHeight;

    [SpaceAttribute]
    public bool gaussian = true;
    public Vector2 peakyness;

    [SpaceAttribute]
    public bool radialLerp = true;
    public float lerpPeakyness;
    public float baseHeight;

    [SpaceAttribute]
    public int noiseResolution;
    public float noiseStrength;

    //public GameObject[] trees;

    void Start() {
        GenerateNewIsland();
    }

    public void GenerateNewIsland() {
        #if (UNITY_EDITOR)
        while(transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
        #endif
        // for now we assume a clean start in-game

        var mesh = GenerateIslandMesh();

        GetComponent<MeshFilter>().mesh = mesh;

        foreach(ProcIslandDecorator d in GetComponents<ProcIslandDecorator>()) {
            d.Decorate(this, mesh);
        }
    }

    Mesh GenerateIslandMesh() {
        Perlin2D noise = new Perlin2D(noiseResolution);

        Vector3[] newVertices = new Vector3[meshResolution * meshResolution];
        Vector2[] newUV = new Vector2[meshResolution * meshResolution];
        int[] newTriangles = new int[3 * 2 * (meshResolution - 1) * (meshResolution - 1)];

        // first we'll create points (and UVs) in a simple grid
        for(int y = 0; y < meshResolution; ++y) {
            for(int x = 0; x < meshResolution; ++x) {
                Vector2 unitPos = new Vector2(coord2float(x), coord2float(y));
                Vector2 centeredPos = unitPos - new Vector2(0.5f, 0.5f);

                Vector3 newVert = new Vector3(centeredPos.x * size, 0, centeredPos.y * size);
                newVert.y = topHeight * (1 + noiseStrength * noise.Eval(unitPos));
                if(gaussian)
                    newVert.y *= Mathf.Exp(-centeredPos.x * centeredPos.x * peakyness.x - centeredPos.y * centeredPos.y * peakyness.y);
                if(radialLerp)
                    newVert.y = Mathf.Lerp(newVert.y, baseHeight, _smoothstep(centeredPos.magnitude));

                newVertices[coord2idx(x, y)] = newVert;
                newUV[coord2idx(x, y)] = new Vector2(coord2float(x), coord2float(y));
            }
        }

        // then the triangles
        int triIdx = 0;
        // 0___2
        //  | /
        // 1|/
        for(int y = 0; y < meshResolution - 1; ++y) {
            for(int x = 0; x < meshResolution - 1; ++x) {
                newTriangles[triIdx] = coord2idx(x, y);
                newTriangles[triIdx + 1] = coord2idx(x, y + 1);
                newTriangles[triIdx + 2] = coord2idx(x + 1, y);
                triIdx += 3;
            }
        }
        //    /|1
        //   / |
        // 2––––0
        for(int y = 1; y < meshResolution; ++y) {
            for(int x = 1; x < meshResolution; ++x) {
                newTriangles[triIdx] = coord2idx(x, y);
                newTriangles[triIdx + 1] = coord2idx(x, y - 1);
                newTriangles[triIdx + 2] = coord2idx(x - 1, y);
                triIdx += 3;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        return mesh;
    }

    float _smoothstep(float x) {
        x = Mathf.Clamp01(2 * x);
        return x * x * ((lerpPeakyness + 1) - lerpPeakyness * x);
    }

    int coord2idx(int x, int y) {
        return x + y * meshResolution;
    }

    float coord2float(int c) {
        return c / (float)meshResolution;
    }
}

#if (UNITY_EDITOR)

[CustomEditor(typeof(ProcIsland))]
public class ProcIslandEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if(GUILayout.Button("Generate")) {
            ((ProcIsland)target).GenerateNewIsland();
        }
    }

}

#endif
