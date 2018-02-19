using UnityEditor;
using UnityEditor.SceneManagement;
using System;
using System.IO;

public class BakeLightForAllScenes {

    public static readonly string[] scenes = ProjectBuildScript.scenes;

    [MenuItem("Automation/Bake Lightmap For All Scenes")]
    static void BakeLightmapForAllScenes() {
        string activeScene = EditorSceneManager.GetActiveScene().path;
        foreach(string scene in scenes) {
            EditorSceneManager.OpenScene(scene);
            Lightmapping.giWorkflowMode = Lightmapping.GIWorkflowMode.OnDemand;
            Lightmapping.Bake();
            EditorSceneManager.SaveOpenScenes();
        }
        EditorSceneManager.OpenScene(activeScene);
    }

}