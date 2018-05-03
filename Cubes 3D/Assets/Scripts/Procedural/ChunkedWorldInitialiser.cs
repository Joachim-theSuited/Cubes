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

    public static Vector3 gateChunk;
    public GameObject gateCompassIcon;
    public float sceneChunkTileSize;

    public WorldChunk chunk;

    [TooltipAttribute("Add an extra border of this many chunks")]
    public uint extraTiles;

    public AnimationCurve gateDistribution;

    // Use this for initialization
    void Start() {
        InitWorld();
    }

    public void InitWorld() {
        int width = (int)(chunk.spawnDistance + extraTiles);
        for(int x = -width; x <= width; ++x) {
            for(int y = -width; y <= width; ++y) {
                var newChunk = chunk.SpawnAt(transform.position + x * Vector3.right * chunk.tileSize + y * Vector3.forward * chunk.tileSize);
                if(newChunk != null)
                    newChunk.transform.SetParent(transform);
            }
        }

        gateChunk = randomizeGateChunk();
    }

    private Vector3 randomizeGateChunk() {
        Vector2 gateChunk = Random.insideUnitCircle;
        int maxSpawnDistance = 50;
        float chunkValue = Random.Range(0.0f, 1.0f);
        while(chunkValue > gateDistribution.Evaluate(gateChunk.magnitude)) {
            gateChunk = Random.insideUnitCircle;
            chunkValue = Random.Range(0.0f, 1.0f);
        }
        Vector3 chunk = new Vector3((int) (gateChunk.x * maxSpawnDistance), 0, (int) (gateChunk.y * maxSpawnDistance));

        // place compass marker at gate chunk
        Instantiate(gateCompassIcon, chunk * sceneChunkTileSize, Quaternion.identity);

        return chunk;
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
