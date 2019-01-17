using UnityEngine;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

/// <summary>
/// Initialise the scene with a square of WorldChunks.
/// Required size and tiling is determined from the chunk prefab.
/// </summary>
public class ChunkedWorldInitialiser : MonoBehaviour {

    [System.Serializable]
    public class GoalConfig 
    {
        public GameObject compassIcon;
        public GameObject prefab;
    }

    public GoalConfig gate;
    public GoalConfig dungeon;

    public WorldChunk chunk;

    [TooltipAttribute("Add an extra border of this many chunks")]
    public uint extraTiles;

    [TooltipAttribute("Maximum distance in chunks of the gate and dungeon to the players start position")]
    public int maxGoalSpawnDistance;

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
        // spawn the goals first, the other chunk check to avoid them
        RandomizeGoalChunk(gate);
        RandomizeGoalChunk(dungeon);

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
    }

    private Vector3 RandomizeGoalChunk(GoalConfig goal) {
        Vector2 gateChunk = Random.insideUnitCircle;
        
        float chunkValue = Random.Range(0.0f, 1.0f);
        while(chunkValue > gateDistribution.Evaluate(gateChunk.magnitude)) {
            gateChunk = Random.insideUnitCircle;
            chunkValue = Random.Range(0.0f, 1.0f);
        }
        Vector3 chunkPosition = new Vector3((int) (gateChunk.x * maxGoalSpawnDistance), 0, (int) (gateChunk.y * maxGoalSpawnDistance));

        Instantiate(goal.compassIcon, chunkPosition * this.chunk.tileSize, Quaternion.identity);
        Instantiate(goal.prefab, chunkPosition * this.chunk.tileSize, Quaternion.identity);

        return chunkPosition;
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
