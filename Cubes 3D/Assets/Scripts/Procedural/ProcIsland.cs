using System;
using UnityEngine;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class ProcIsland : MonoBehaviour {

    public int meshResolution;
    public AnimationCurve sizeX;
    float _sizeX;
    public AnimationCurve sizeY;
    float _sizeY;

    public AnimationCurve topHeight;
    float _topHeight;

    public bool flatShading;

    [SpaceAttribute]
    public bool gaussian = true;
    public AnimationCurve peakynessX;
    float _peakynessX;
    public AnimationCurve peakynessY;
    float _peakynessY;

    [SpaceAttribute]
    public bool radialLerp = true;
    public float lerpPeakyness;
    public float baseHeight;

    [SpaceAttribute]
    public int noiseResolution;
    
    public AnimationCurve noiseStrength;
    float _noiseStrength;

    void Start() {
        GenerateNewIsland();
    }

    void EvaluateRandomCurves() {
        _sizeX = sizeX.Evaluate(UnityEngine.Random.value);
        _sizeY = sizeY.Evaluate(UnityEngine.Random.value);
        _topHeight = topHeight.Evaluate(UnityEngine.Random.value);
        _peakynessX = peakynessX.Evaluate(UnityEngine.Random.value);
        _peakynessY = peakynessY.Evaluate(UnityEngine.Random.value);
        _noiseStrength = noiseStrength.Evaluate(UnityEngine.Random.value);
    }

    public void GenerateNewIsland() {
        #if (UNITY_EDITOR)
        while(transform.childCount > 0)
            DestroyImmediate(transform.GetChild(0).gameObject);
        #endif
        // for now we assume a clean start in-game
        
        EvaluateRandomCurves();
        Perlin2D noise = new Perlin2D(noiseResolution);
        var mesh = GenerateIslandMesh(noise, meshResolution);

        GetComponent<MeshFilter>().sharedMesh = mesh;

        MeshCollider coll = GetComponent<MeshCollider>();
        coll.sharedMesh = mesh;
        
        // the MeshCollider is not yet initialised, but might be needed by ProcIslandGreebler
        // therefore the decoration calls are deferred
        needsDecoration = true;
    }
    
    // deferred decoration call
    bool needsDecoration;
    void LateUpdate() {
        if(needsDecoration) {
            #if (UNITY_EDITOR)
                var mesh = GetComponent<MeshFilter>().sharedMesh;
            #else
                var mesh = GetComponent<MeshFilter>().mesh;
            #endif
            foreach(ProcIslandDecorator d in GetComponents<ProcIslandDecorator>()) {
                d.Decorate(this, mesh);
            }

            needsDecoration=false;
        }
    }

    Vector3 EvaluateToVertex(Perlin2D noise, float u, float v) {
        Vector2 unitPos = new Vector2(u, v);
        Vector2 centeredPos = unitPos - new Vector2(0.5f, 0.5f);

        Vector3 newVert = new Vector3(centeredPos.x * _sizeX, 0, centeredPos.y * _sizeY);
        newVert.y = _topHeight * (1 + _noiseStrength * noise.Eval(unitPos));
        if(gaussian)
            newVert.y *= Mathf.Exp(-centeredPos.x * centeredPos.x * _peakynessX - centeredPos.y * centeredPos.y * _peakynessY);
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
