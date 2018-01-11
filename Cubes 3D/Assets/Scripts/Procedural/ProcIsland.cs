using System;
using UnityEngine;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
public class ProcIsland : MonoBehaviour {

    public int meshResolution;
    public float size;

    public float topHeight;

    public bool generateColliderMesh;
    public int colliderMeshResolution;

    public bool flatShading;

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

    void Start() {
        GenerateNewIsland();
    }

    public void GenerateNewIsland() {
        #if (UNITY_EDITOR)
        while(transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
        #endif
        // for now we assume a clean start in-game

        Perlin2D noise = new Perlin2D(noiseResolution);
        var mesh = GenerateIslandMesh(noise, meshResolution);

        GetComponent<MeshFilter>().mesh = mesh;

        foreach(ProcIslandDecorator d in GetComponents<ProcIslandDecorator>()) {
            d.Decorate(this, mesh);
        }

        if(generateColliderMesh) {
            MeshCollider coll = GetComponent<MeshCollider>();
            if(coll == null)
                Debug.LogError("No MeshCollider found!");
            else
                coll.sharedMesh = GenerateIslandMesh(noise, colliderMeshResolution);
        }
    }

    Vector3 EvaluateToVertex(Perlin2D noise, float u, float v) {
        Vector2 unitPos = new Vector2(u, v);
        Vector2 centeredPos = unitPos - new Vector2(0.5f, 0.5f);

        Vector3 newVert = new Vector3(centeredPos.x * size, 0, centeredPos.y * size);
        newVert.y = topHeight * (1 + noiseStrength * noise.Eval(unitPos));
        if(gaussian)
            newVert.y *= Mathf.Exp(-centeredPos.x * centeredPos.x * peakyness.x - centeredPos.y * centeredPos.y * peakyness.y);
        if(radialLerp)
            newVert.y = Mathf.Lerp(newVert.y, baseHeight, _smoothstep(centeredPos.magnitude));

        return newVert;
    }

    Mesh GenerateIslandMesh(Perlin2D noise, int resolution) {
        Func<int, int, int> coord2idx = (x, y) => (x + y * resolution);
        Func<int, float> coord2float = c => (c / (float)(resolution - 1));

        Vector3[] newVertices;
        if(flatShading)
            newVertices = new Vector3[3 * 2 * (resolution - 1) * (resolution - 1)];
        else
            newVertices = new Vector3[resolution * resolution];
        int[] newTriangles = new int[3 * 2 * (resolution - 1) * (resolution - 1)];

        // for smooth shading vertices can be shared between triangles and we generate them up front
        if(!flatShading) {
            for(int y = 0; y < resolution; ++y) {
                for(int x = 0; x < resolution; ++x) {
                    newVertices[coord2idx(x, y)] = EvaluateToVertex(noise, coord2float(x), coord2float(y));
                }
            }

            int triIdx = 0; // write index for tris
            for(int y = 0; y < resolution - 1; ++y) {
                for(int x = 0; x < resolution - 1; ++x) {
                    // 0___2
                    //  | /
                    // 1|/
                    newTriangles[triIdx++] = coord2idx(x, y);
                    newTriangles[triIdx++] = coord2idx(x, y + 1);
                    newTriangles[triIdx++] = coord2idx(x + 1, y);
                    //    /|1
                    //   / |
                    // 2––––0
                    newTriangles[triIdx++] = coord2idx(x + 1, y + 1);
                    newTriangles[triIdx++] = coord2idx(x + 1, y);
                    newTriangles[triIdx++] = coord2idx(x, y + 1);
                }
            }

        }
        // for flat shading every triangle gets its own vertices, to store the face normal with every vertex
        else {
            for(int i = 0; i < newTriangles.Length; i++)
                newTriangles[i] = i;

            int vIdx = 0; // write index for verts
            for(int y = 0; y < resolution - 1; ++y) {
                for(int x = 0; x < resolution - 1; ++x) {
                    // flat shading makes tris visible, so randomly flip, whether we split |/| or |\| to break regularity
                    int flip = UnityEngine.Random.value > .5 ? 0 : 1;
                    newVertices[vIdx++] = EvaluateToVertex(noise, coord2float(x), coord2float(y));
                    newVertices[vIdx++] = EvaluateToVertex(noise, coord2float(x), coord2float(y + 1));
                    newVertices[vIdx++] = EvaluateToVertex(noise, coord2float(x + 1), coord2float(y + flip));

                    newVertices[vIdx++] = EvaluateToVertex(noise, coord2float(x + 1), coord2float(y + 1));
                    newVertices[vIdx++] = EvaluateToVertex(noise, coord2float(x + 1), coord2float(y));
                    newVertices[vIdx++] = EvaluateToVertex(noise, coord2float(x), coord2float(y + 1 - flip));

                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = newVertices;
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
