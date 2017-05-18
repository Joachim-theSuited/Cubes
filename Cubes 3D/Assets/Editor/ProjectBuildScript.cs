using UnityEditor;
using System;
using System.IO;

public class ProjectBuildScript
{
    public static readonly string SCENELOCATION = "Assets/Scenes/";

    public static readonly string[] scenes = { SCENELOCATION + "Develop/Sandbox.unity", SCENELOCATION + "Develop/Neighborhood.unity" };

    [MenuItem("Build Tools/Experimental Android Build")]
    static void BuildAndroid()
    {
		BuildPipeline.BuildPlayer(scenes, GetBuildTarget("android", ".apk"), BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("Build Tools/Windows Desktop Build")]
    static void BuildWindows()
    {
		BuildPipeline.BuildPlayer(scenes, GetBuildTarget("win64", ".exe"), BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    [MenuItem("Build Tools/Linux Desktop Build")]
    static void BuildLinux()
    {
		BuildPipeline.BuildPlayer(scenes, GetBuildTarget("linux64", ""), BuildTarget.StandaloneLinux64, BuildOptions.None);
    }

	public static string GetBuildTarget(string platform, string fileExtension) {
		return String.Format("./Target/Cubes3D_{0}_{1}/Cubes3D{2}", DateTime.Today.ToString("yyyy_MM_dd"), platform, fileExtension);
	}
}