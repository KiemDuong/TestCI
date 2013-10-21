using UnityEngine;
using System.Collections;
using UnityEditor;

using System.IO;

public class AutomaticBuildScript
{
 	static string APP_NAME = "TestCI";
	
	const string BUILD_DIR = "build";
	
	const string BIN_DIR = "bin";
	
	 ///////////////////////////////////////////////////////////////////////////////////////////////////
	 static void PerformBuild (BuildTarget target, BuildOptions option = BuildOptions.None)
	{	
		Directory.Delete ("Assets/Plugins", true);
		
		Directory.Delete ("Assets/UnitTest-Scripts (External)", true);
		
		AssetDatabase.Refresh (ImportAssetOptions.ForceSynchronousImport);
		
		EditorUserBuildSettings.activeBuildTargetChanged = () => {
			Debug.Log ("Active target: " + EditorUserBuildSettings.activeBuildTarget.ToString ());	
		};
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (target);
		
		if (!Directory.Exists (BUILD_DIR)) {
			Directory.CreateDirectory (BUILD_DIR);	
		}
		
		if (!Directory.Exists (BUILD_DIR + "/" + BIN_DIR)) {
			Directory.CreateDirectory (BUILD_DIR + "/" + BIN_DIR);	
		}
		
		string target_dir = BUILD_DIR + "/" + BIN_DIR + "/" + APP_NAME;
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
