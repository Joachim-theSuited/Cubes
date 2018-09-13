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
    public static Vector3 dungeonGateChunk;
    public GameObject gateCompassIcon;
    public GameObject dungeonEntryCompassIcon;
    public float sceneChunkTileSize;

    public WorldChunk chunk;

    [TooltipAttribute("Add an extra border of this many chunks")]
    public uint extraTiles;

    public AnimationCurve gateDistribution;

    // Use this for initialization
    void Start() {
        GameObject player = GameObject.FindWithTag(Tags.Player);
        if(player) {
            transform.position = player.transform.position;
        }
        InitWorld();
    }

    float AlignToTileSize(float pos) {
        return Mathf.Round(pos / chunk.tileSize) * chunk.tileSize;
    }

    public void InitWorld() {
        // position might be init'ed from player position, but also influences generation
        // for consistent results we want to ensure a certain alignment
        Vector3 position = transform.position;
        position.x = AlignToTileSize(position.x);
        position.y = 0;
        position.z = AlignToTileSize(position.z);
        transform.position = position;

        int width = (int)(chunk.spawnDistance + extraTiles);
        for(int x = -width; x <= width; ++x) {
            for(int y = -width; y <= width; ++y) {
                var newChunk = chunk.SpawnAt(transform.position + x * Vector3.right * chunk.tileSize + y * Vector3.forward * chunk.tileSize);
                if(newChunk != null)
                    newChunk.transform.SetParent(transform);
            }
        }

        gateChunk = randomizeGateChunk(gateCompassIcon);
        dungeonGateChunk = randomizeGateChunk(dungeonEntryCompassIcon);
    }

    private Vector3 randomizeGateChunk(GameObject compassIcon) {
        Vector2 gateChunk = Random.insideUnitCircle;
        int maxSpawnDistance = 50;
        float chunkValue = Random.Range(0.0f, 1.0f);
        while(chunkValue > gateDistribution.Evaluate(gateChunk.magnitude)) {
            gateChunk = Random.insideUnitCircle;
            chunkValue = Random.Range(0.0f, 1.0f);
        }
        Vector3 chunk = new Vector3((int) (gateChunk.x * maxSpawnDistance), 0, (int) (gateChunk.y * maxSpawnDistance));

        // place compass marker at gate chunk
        Instantiate(compassIcon, chunk * sceneChunkTileSize, Quaternion.identity);

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
