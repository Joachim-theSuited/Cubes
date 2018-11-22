using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Finds a game object marked as Persistent and sets it as the player object.
/// </summary>
public class AcquirePersistentPlayer : MonoBehaviour {

    public static GameObject PERSISTENT_PLAYER;

    IEnumerator Start() {
        if(PERSISTENT_PLAYER != null) {

            Destroy(GameObject.FindWithTag(Tags.DebugDefault));

            // this is a semi-dirty hack, that resets the mesh
            // necessary to reset cloth components, which went crazy on scene change
            // FIXME could be solved by temporatily disabling the cloth - though that crashs in unity's current version
            GameObject newInstance = Instantiate(PERSISTENT_PLAYER);
            newInstance.transform.parent = transform;
            newInstance.transform.localPosition = Vector3.zero;
            newInstance.transform.localRotation = Quaternion.identity;

            newInstance.SetActive(true);

            // stuff gets not properly deleted (and instantiated?) until end of frame
            yield return new WaitForEndOfFrame();
            GetComponent<Animator>().Rebind();
        } else {
            print("no PERSISTENT_PLAYER found");
        }
    }
}
