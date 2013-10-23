using UnityEngine;
using System.Collections;
using UnityEditor;

using System.IO;

public class AutomaticBuildScript
{
 	static string APP_NAME = "TestCI";
	
	const string BUILD_DIR = "build";
	
	const string OUTPUT_DIR = "output";
	
	 ///////////////////////////////////////////////////////////////////////////////////////////////////
	static void PerformBuild (BuildTarget target, BuildOptions option = BuildOptions.None)
	{	
		EditorUserBuildSettings.SwitchActiveBuildTarget (target);
		
		if (!Directory.Exists (BUILD_DIR)) {
			Directory.CreateDirectory (BUILD_DIR);	
		}
		
		if (!Directory.Exists (BUILD_DIR + "/" + OUTPUT_DIR)) {
			Directory.CreateDirectory (BUILD_DIR + "/" + OUTPUT_DIR);	
		}
		
		string target_dir = BUILD_DIR + "/" + OUTPUT_DIR + "/" + APP_NAME;
		string[] scenes = { "Assets/Scenes/TestBuild.unity" };
		
		// Test build 
		BuildPipeline.BuildPlayer(scenes, target_dir, target, option);
 	}
	///////////////////////////////////////////////////////////////////////////////////////////////////
	
	[MenuItem ("AutomaticBuild/BuildAndroid")]
	static void PerformBuildAndroid ()
	{	
		AutomaticBuildScript.PerformBuild (BuildTarget.Android);
	}
	
	[MenuItem ("AutomaticBuild/BuildWindows")]
	static void PerformBuildWindows ()
	{
		AutomaticBuildScript.PerformBuild (BuildTarget.StandaloneWindows);
	}
	
	[MenuItem ("AutomaticBuild/BuildMAC")]
	static void PerformBuildMAC ()
	{
		AutomaticBuildScript.PerformBuild (BuildTarget.StandaloneOSXIntel);
	}
	
	[MenuItem ("AutomaticBuild/BuildIOS")]
	static void PerformBuildIOS ()
	{
		AutomaticBuildScript.PerformBuild (BuildTarget.iPhone);
	}
}
