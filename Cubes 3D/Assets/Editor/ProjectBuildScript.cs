using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Diagnostics;

public class ProjectBuildScript {
	public static readonly string linux = "linux64";
	public static readonly string win = "win64";
	public static readonly string android = "android";

    public static readonly string SCENELOCATION = "Assets/Scenes/";
	public static readonly string PROJECTROOT = "../";

	// change starting scene if we ever decide to have a different first scene
	public static readonly string startingScene = SCENELOCATION + "Menu/InteractiveMenu.unity";
	public static readonly string[] staticIncludes = { "/README.md" };

	[MenuItem("Build Tools/Open File Explorer in Target Directory")]
	static void OpenInTarget() {
		Directory.CreateDirectory(Path.GetFullPath("./Target"));
		Process.Start(Path.GetFullPath("./Target"));
	}

    [MenuItem("Build Tools/Windows Desktop Build")]
    static void BuildWindows() {
		BuildPipeline.BuildPlayer(constructSceneArray(), GetBuildTarget(win, ".exe"), BuildTarget.StandaloneWindows64, BuildOptions.None);
		includeStatics(GetBuildDirectory(win));
    }

    [MenuItem("Build Tools/Linux Desktop Build")]
    static void BuildLinux() {
		BuildPipeline.BuildPlayer(constructSceneArray(), GetBuildTarget(linux, ""), BuildTarget.StandaloneLinux64, BuildOptions.None);
		includeStatics(GetBuildDirectory(linux));
    }

	[MenuItem("Build Tools/Experimental Android Build")]
	static void BuildAndroid() {
		BuildPipeline.BuildPlayer(constructSceneArray(), GetBuildTarget(android, ".apk"), BuildTarget.Android, BuildOptions.None);
	}

	public static string GetBuildDirectory(string platform) {
		return String.Format("./Target/Cubes3D_{0}_{1}", DateTime.Today.ToString("yyyy_MM_dd"), platform);
	}

	public static string GetBuildTarget(string platform, string fileExtension) {
		return GetBuildDirectory(platform) + String.Format("/Cubes3D{2}", DateTime.Today.ToString("yyyy_MM_dd"), platform, fileExtension);
	}

	private static void includeStatics(string targetDirectory) {
		foreach(string si in staticIncludes) {
			File.Delete(targetDirectory + si);
			File.Copy(PROJECTROOT + si, targetDirectory + si);
		}
	}

	private static List<string> findAllScenes(string location) {
		List<string> scenes = new List<string>();
		foreach(string subDirectory in Directory.GetDirectories(location)) {
			scenes.AddRange(findAllScenes(subDirectory));
		}

		foreach(string scene in Directory.GetFiles(location, "*.unity")) {
			// strip out stupid windows backslashes
			// may not be a problem on UNIX-likes
			scenes.Add(Regex.Replace(scene, "\\\\", "/"));
		}
		return scenes;
	}

	public static string[] constructSceneArray() {
		// using set to automatically remove duplicates
		HashSet<string> scenes = new HashSet<string>();
		scenes.Add(startingScene);
		foreach(string scene in findAllScenes(SCENELOCATION)) {
			scenes.Add(scene);
		}

		return new List<string>(scenes).ToArray();
	}
}