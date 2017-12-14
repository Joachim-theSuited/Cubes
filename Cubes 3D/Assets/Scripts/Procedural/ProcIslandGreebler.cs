using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcIslandGreebler : MonoBehaviour, ProcIslandDecorator {

    public GameObject[] greebles;

    [TooltipAttribute("Minimum and maximum number of greebles to place")]
    public Vector2 countMinMax;

    [TooltipAttribute("Minimum and maximum mesh height for placing greebles")]
    public Vector2 heightMinMax;

    public bool alignToMeshNormal;
    public bool randomYRotation;

    [TooltipAttribute("Stop after this many fails")]
    public int maxFails;

    void ProcIslandDecorator.Decorate(ProcIsland island, Mesh mesh) {
        if(greebles.Length == 0)
            return;

        int count = (int)Random.Range(countMinMax.x, countMinMax.y + 0.1f);
        int fails = 0;
        while(count > 0) {
            int idx = Random.Range(0, mesh.vertices.Length);
            if(mesh.vertices[idx].y < heightMinMax.x || mesh.vertices[idx].y > heightMinMax.y) {
                ++fails;
                if(fails >= maxFails)
                    break;
                continue;
            }
            var obj = Instantiate(greebles[(int)Random.Range(0, greebles.Length - 0.5f)], island.transform); 
            obj.transform.localPosition = (mesh.vertices[idx]);
            if(randomYRotation)
                obj.transform.Rotate(0, Random.Range(0, 360), 0, Space.World);
            if(alignToMeshNormal)
                obj.transform.rotation *= Quaternion.FromToRotation(Vector3.up, mesh.normals[idx]);
            

            --count;
        }

        if(count > 0)
            Debug.Log("Stopped after " + fails + " fails, had " + count + " more objects to place.");
    }

}
