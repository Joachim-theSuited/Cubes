using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that places an object in the center of a WorldChunk with a given probability.
/// The result depends on the position of the chunk and is thus reproducible.
/// </summary>
public class IslandPlacer : MonoBehaviour, WorldChunkDecorator {

    public GameObject islandPrefab;

    [Range(0, 1)]
    public float probability;

    public int randomSeedOffset;

    void WorldChunkDecorator.Decorate(WorldChunk chunk) {
        Vector3 normedPosition = chunk.transform.position / chunk.tileSize;

        int quadrant = 0; // a factor in [0,3]; not conventinal numbering
        if(normedPosition.x < 0)
            quadrant += 1;
        if(normedPosition.z < 0)
            quadrant += 2;

        // remove quadrant information from coords
        int x = (int)Mathf.Abs(normedPosition.x);
        int z = (int)Mathf.Abs(normedPosition.z);
        // map N^2 to N by diagonalisation
        int flatIdx = (x * (x + 1) / 2 + z);
        // map different quadrants together into N
        flatIdx = flatIdx * 4 + quadrant;

        // the randomisation is based on the position, to always get the same results on regeneration
        Random.InitState(flatIdx + randomSeedOffset);

        if(Random.value < probability)
            Instantiate(islandPrefab, chunk.transform.position, Quaternion.identity, chunk.transform);
    }

}
