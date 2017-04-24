using UnityEditor;

public class ProjectBuildScript
{
    public static readonly string SCENELOCATION = "Assets/Scenes/";

    public static readonly string[] scenes = { SCENELOCATION + "Develop/Sandbox.unity", SCENELOCATION + "Develop/Neighborhood.unity" };

    [MenuItem("Build Tools/Experimental Android Build")]
    static void BuildAndroid()
    {
        BuildPipeline.BuildPlayer(scenes, "./Target/Android/Cubes3D.apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Build Tools/Windows Desktop Build")]
    static void BuildWindows()
    {
        BuildPipeline.BuildPlayer(scenes, "./Target/Win64/Cubes3D.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    [MenuItem("Build Tools/Linux Desktop Build")]
    static void BuildLinux()
    {
        BuildPipeline.BuildPlayer(scenes, "./Target/Linux64/Cubes3D", BuildTarget.StandaloneLinux64, BuildOptions.None);
    }
}