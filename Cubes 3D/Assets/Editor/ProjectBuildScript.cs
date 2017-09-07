using UnityEditor;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor.OSXStandalone;
using System.Diagnostics;

public class ProjectBuildScript
{
	public static readonly string linux = "linux64";
	public static readonly string win = "win64";
	public static readonly string android = "android";

    public static readonly string SCENELOCATION = "Assets/Scenes/";
	public static readonly string PROJECTROOT = "../";

	public static readonly string[] scenes = { SCENELOCATION + "Menu/MainMenu.unity", SCENELOCATION + "Develop/Sandbox.unity", SCENELOCATION + "Develop/Neighborhood.unity" };
	public static readonly string[] staticIncludes = { "/README.md" };

	[MenuItem("Build Tools/Open File Explorer in Target Directory")]
	static void OpenInTarget() {
		Process.Start(Path.GetFullPath("./Target"));
	}

    [MenuItem("Build Tools/Windows Desktop Build")]
    static void BuildWindows()
    {
		BuildPipeline.BuildPlayer(scenes, GetBuildTarget(win, ".exe"), BuildTarget.StandaloneWindows64, BuildOptions.None);
		includeStatics(GetBuildDirectory(win));
    }

    [MenuItem("Build Tools/Linux Desktop Build")]
    static void BuildLinux()
    {
		BuildPipeline.BuildPlayer(scenes, GetBuildTarget(linux, ""), BuildTarget.StandaloneLinux64, BuildOptions.None);
		includeStatics(GetBuildDirectory(linux));
    }

	[MenuItem("Build Tools/Experimental Android Build")]
	static void BuildAndroid()
	{
		BuildPipeline.BuildPlayer(scenes, GetBuildTarget(android, ".apk"), BuildTarget.Android, BuildOptions.None);
	}

	public static string GetBuildDirectory(string platform) {
		return String.Format("./Target/Cubes3D_{0}_{1}", DateTime.Today.ToString("yyyy_MM_dd"), platform);
	}

	public static string GetBuildTarget(string platform, string fileExtension) {
		return GetBuildDirectory(platform) + String.Format("/Cubes3D{2}", DateTime.Today.ToString("yyyy_MM_dd"), platform, fileExtension);
	}

	private static void includeStatics(string targetDirectory) {
		foreach(string si in staticIncludes) {
			File.Copy(PROJECTROOT + si, targetDirectory + si);
		}
	}
}