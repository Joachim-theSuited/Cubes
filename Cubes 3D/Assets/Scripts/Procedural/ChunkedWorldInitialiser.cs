using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

/// <summary>
/// Initialise the scene with a square of WorldChunks.
/// Required size and tiling is determined from the chunk prefab.
/// </summary>
public class ChunkedWorldInitialiser : MonoBehaviour {

    public WorldChunk chunk;

    [TooltipAttribute("Add an extra border of this many chunks")]
    public uint extraTiles;

    // Use this for initialization
    void Start() {
        InitWorld();
    }

    public void InitWorld() {
        int width = (int)(chunk.spawnDistance + extraTiles);
        for(int x = -width; x <= width; ++x) {
            for(int y = -width; y <= width; ++y) {
                WorldChunk newChunk = chunk.SpawnAt(transform.position + x * Vector3.right * chunk.tileSize + y * Vector3.forward * chunk.tileSize);
                if(newChunk != null)
                    newChunk.transform.SetParent(transform);
            }
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(ChunkedWorldInitialiser))]
public class ChunkedWorldInitialiserEditor : Editor {

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        ChunkedWorldInitialiser script = (ChunkedWorldInitialiser)target;
        if(GUILayout.Button("Clear All Children")) {
            while(script.transform.childCount > 0)
                DestroyImmediate(script.transform.GetChild(0).gameObject);
        }
        if(GUILayout.Button("Test Init")) {
            script.InitWorld();
        }
    }
}
#endif
