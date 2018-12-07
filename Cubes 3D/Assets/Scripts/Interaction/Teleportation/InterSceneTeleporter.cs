using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This Teleporter allows teleportation to InterSceneTeleporters in different scenes.
/// Only supports teleportation of the player.
/// InterSceneTeleporters in other Scenes are identified by their name.
/// </summary>
[RequireComponent(typeof(Collider))]
public class InterSceneTeleporter : Teleporter {

    /// <summary>
    /// Name of the Scene this Teleporter connects to.
    /// </summary>
    public string targetScene;

    /// <summary>
    /// The name of the teleporter that should be used as destination in the targetScene.
    /// </summary>
    public string targetTeleporterName;

    void OnTriggerEnter(Collider other) {
        Teleportable otherTeleportable = other.GetComponent<Teleportable>();
        if(otherTeleportable != null && otherTeleportable.lastTeleportedTo != this && targetScene != null) {
            if(AcquirePersistentPlayer.PERSISTENT_PLAYER != null) {
                DontDestroyOnLoad(AcquirePersistentPlayer.PERSISTENT_PLAYER);
            }

            if(GetComponent<AudioSource>() != null) {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.Play();
                StartCoroutine(WaitForAudioPlayed(audioSource.clip.length));
            } else {
                SceneManager.LoadScene(targetScene);
                SceneManager.activeSceneChanged += _activeSceneChanged;
            }

        }
    }

    private void _activeSceneChanged(Scene from, Scene to) {
        Teleportable player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<Teleportable>();
        
        bool complete = false;
        foreach(GameObject go in to.GetRootGameObjects()) {
            foreach(InterSceneTeleporter teleporter in go.GetComponentsInChildren<InterSceneTeleporter>()) {
                if (teleporter.name == targetTeleporterName) {
                    player.transform.position = teleporter.transform.position;
                    player.lastTeleportedTo = teleporter;
                    complete = true;
                    break;
                }
            }
            if(complete) {
                break;
            }
        }

        // unsubscribe self
        SceneManager.activeSceneChanged -= _activeSceneChanged;
    }

    IEnumerator WaitForAudioPlayed(float toWait) {
        yield return new WaitForSeconds(toWait);

        SceneManager.LoadScene(targetScene);
        SceneManager.activeSceneChanged += _activeSceneChanged;
    }
}
