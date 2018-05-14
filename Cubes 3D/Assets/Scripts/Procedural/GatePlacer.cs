using System.Collections.Generic;
using UnityEngine;

public class GatePlacer : MonoBehaviour, WorldChunkDecorator {

    public GameObject gatePrefab;

    void WorldChunkDecorator.Decorate(WorldChunk chunk) {
        Vector3 normedPosition = chunk.transform.position / chunk.tileSize;

        if((int) normedPosition.x == ChunkedWorldInitialiser.gateChunk.x && (int) normedPosition.z == ChunkedWorldInitialiser.gateChunk.z) {
            Instantiate(gatePrefab, chunk.transform);
        }
    }
}