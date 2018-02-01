using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Chunk of a grid based, auto-expanding world.
/// Requires a trigger collider to determine when the player leaves the chunk and to check where a chunk is already placed.
/// For the placement check to work, WorldChunks should be in a separate layer.
/// </summary>
[RequireComponent(typeof(Collider))]
public class WorldChunk : MonoBehaviour {

    [Tooltip("Tiling size of the world grid (and thus chunks)")]
    public float tileSize;

    [Tooltip("Distance in tiles at which to spawn new chunks")]
    public uint spawnDistance;

    [Tooltip("Prefab for new world chunks")]
    public PrefabReference spawn;

    void Start() {
        foreach(WorldChunkDecorator wcd in GetComponents<WorldChunkDecorator>())
            wcd.Decorate(this);
    }

    // when the player leaves the chunk, we want to extend the world in that direction
    void OnTriggerExit(Collider other) {
        if(other.tag == Tags.Player) {
            Vector3 delta = other.transform.position - transform.position;

            if(delta.z > 0)
                SpawnInDirection(Vector3.forward);
            if(delta.z < 0)
                SpawnInDirection(Vector3.back);
            if(delta.x > 0)
                SpawnInDirection(Vector3.right);
            if(delta.x < 0)
                SpawnInDirection(Vector3.left);
        }
    }

    /// <param name="direction">Direction to spawn in. Must be normalised and in the xz plane</param>
    void SpawnInDirection(Vector3 direction) {
        Vector3 spawnLineCentre = transform.position + direction * spawnDistance * tileSize;
        Vector3 spawnLine = new Vector3(-direction.z, 0, direction.x); // orthogonal, when in xz plane

        // place one in the centre
        SpawnAt(spawnLineCentre);
        // and extend prependicular in both directions (we're building the side of a square)
        for(int i = 1; i <= spawnDistance; ++i) {
            SpawnAt(spawnLineCentre + spawnLine * i * tileSize);
            SpawnAt(spawnLineCentre - spawnLine * i * tileSize);
        }
    }

    /// <summary>
    /// Instantiate prefab at position, if not already filled.
    /// Return the new instance or null accordingly.
    /// </summary>
    public GameObject SpawnAt(Vector3 position) {
        // first check, if a chunk is already present
        if(Physics.CheckSphere(position, Mathf.Min(1, tileSize / 2), 1 << gameObject.layer, QueryTriggerInteraction.Collide))
            return null;

        return Instantiate(spawn.reference, position, Quaternion.identity);
    }

}
