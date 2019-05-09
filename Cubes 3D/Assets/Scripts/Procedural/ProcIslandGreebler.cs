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
    const int maxFails = 100;

    void ProcIslandDecorator.Decorate(ProcIsland island, Mesh mesh)
    {
        if (greebles.Length == 0)
        {
            Debug.Log("No greebles assigned on " + gameObject.name);
            return;
        }

        MeshCollider coll = island.GetComponent<MeshCollider>();
        int count = (int)Random.Range(countMinMax.x, countMinMax.y + 0.1f);
        int fails = 0;
        while (count > 0)
        {
            Vector3 randomPosition = new Vector3(
                    coll.bounds.min.x + Random.Range(0, coll.bounds.size.x),
                    coll.bounds.max.y + 1,
                    coll.bounds.min.z + Random.Range(0, coll.bounds.size.z));

            RaycastHit hit;
            if (coll.Raycast(new Ray(randomPosition, Vector3.down), out hit, 100))
            {
                randomPosition.y = hit.point.y;
            }
            else
            {
                Debug.Log("Raycast didn't hit mesh.");
                ++fails;
                if (fails >= maxFails)
                    break;
                continue;
            }

            if (randomPosition.y < heightMinMax.x || randomPosition.y > heightMinMax.y)
            {
                ++fails;
                if (fails >= maxFails)
                    break;
                continue;
            }

            var greeble = Instantiate(greebles[(int)Random.Range(0, greebles.Length - 0.5f)], island.transform); 
            greeble.transform.position = randomPosition;
            if (alignToMeshNormal)
            {
                greeble.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            }
            if (randomYRotation)
                greeble.transform.Rotate(0, Random.Range(0, 360), 0, Space.World);

            --count;
        }

        if (count > 0)
            Debug.Log("Stopped after " + fails + " fails, had " + count + " more objects to place.");
    }

}
