using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script that places an object in the center of a WorldChunk with a given probability.
/// The result depends on the position of the chunk and is thus reproducible.
/// </summary>
public class IslandPlacer : MonoBehaviour, WorldChunkDecorator {

    public int randomSeedOffset;

    public GameObject[] islandPrefabs;

    [Range(0, 1)]
    public float probability;

    [Range(0, 1)]
    [Tooltip("How far from the chunk's center may the island be placed")]
    public float spread;

    [Tooltip("Square areas of size tiling will have the same island type")]
    public int tiling;
    [Tooltip("Below 1 for more perlin-like distribution. Above 1 for decorrelation of neighbouring tiles.")]
    public float tileRandomness;

    void WorldChunkDecorator.Decorate(WorldChunk chunk) {
        Vector3 normedPosition = chunk.transform.position / chunk.tileSize;

        int x = (int)normedPosition.x;
        int z = (int)normedPosition.z;
        float angle = Mathf.Atan2(z, x);
        if(angle < 0) // atan2 can return negative angles
            angle += Mathf.PI * 2;
        int quadrant = (int)(angle / (Mathf.PI / 2));

        // remove quadrant information from coords by rotating into quadrant 0
        switch(quadrant) {
            case 0: // already set correctly
                break;
            case 1:
                x = (int)normedPosition.z;
                z = -(int)normedPosition.x;
                break;
            case 2:
                x = -(int)normedPosition.x;
                z = -(int)normedPosition.z;
                break;
            case 3:
                x = -(int)normedPosition.z;
                z = (int)normedPosition.x;
                break;
            default:
                Debug.Log("Invalid quadrant: " + quadrant.ToString() + " !");
                break;
        }
        Debug.Assert(x >= 0 && z >= 0, "Coordinates (" + x.ToString() + ", " + z.ToString() + ") do not lie in quadrant 0!");

        // map N^2 to N by diagonalisation
        int diagonalRow = x + z - 1; // (0, 0) is a special case counts as 'row -1'
        int flatIdx = (diagonalRow * (diagonalRow + 1) / 2 + z);
        // map different quadrants together into N
        flatIdx = diagonalRow == -1 ? 0 : (flatIdx * 4 + quadrant + 1);

        // the randomisation is based on the position, to always get the same results on regeneration
        Random.InitState(flatIdx + randomSeedOffset);

        if(Random.value < probability && islandPrefabs.Length != 0) {
            // randomise position in chunk
            Vector2 direction = Random.insideUnitCircle;
            Vector3 offset = new Vector3(direction.x, 0, direction.y);
            offset *= spread * chunk.tileSize / 2;

            // choose island type
            // we group squares of tiling*tiling WorldChunks
            //Vector2 tileCoords = new Vector2((int)normedPosition.x / tiling, (int)normedPosition.z / tiling);
            Vector2 tileCoords = new Vector2(Mathf.Round(normedPosition.x / tiling), Mathf.Round(normedPosition.z / tiling));
            // Unity's perlin mirrors at 0, which we try to avoid with a huge offset
            // also it seems to be uniformly .5 for integer coords, so we add some fractional fudge
            Vector2 perlinCoords = tileCoords * tileRandomness + new Vector2(16000.375f, 16000.625f);
            var noise = Mathf.PerlinNoise(perlinCoords.x, perlinCoords.y);
            // rescale, as the interval ends seemed to be underrepresented
            if(islandPrefabs.Length > 2) {
                float rsFactor = 1f / islandPrefabs.Length;
                noise = ( Mathf.Clamp(noise, rsFactor, 1-rsFactor) - rsFactor ) / ( 1 - 2*rsFactor );
            }
            int islandIdx = (int) Mathf.Clamp( noise * islandPrefabs.Length, 0, islandPrefabs.Length - 1);

            Instantiate(islandPrefabs[islandIdx], chunk.transform.position + offset, Quaternion.identity, chunk.transform);
        }
    }

}
