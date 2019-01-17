using System.Collections.Generic;
using UnityEngine;

public class GatePlacer : MonoBehaviour, ProcIslandDecorator {

    public GameObject gatePrefab;
    
    void ProcIslandDecorator.Decorate(ProcIsland island, Mesh mesh) {
        Instantiate(gatePrefab, island.transform);
    }
}