using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPlacer : MonoBehaviour, WorldChunkDecorator {

	public GameObject dungeonPrefab;

	void WorldChunkDecorator.Decorate(WorldChunk chunk)
	{
		Instantiate(dungeonPrefab, chunk.transform);
	}
}
