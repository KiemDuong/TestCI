using UnityEngine;
using System.Collections;
using UnityEditor;

using System.IO;

public class AutomaticBuildScript
{
	 static string APP_NAME = "TestCI";
	
	 ///////////////////////////////////////////////////////////////////////////////////////////////////
	 static void PerformBuild (BuildTarget target, BuildOptions option = BuildOptions.None)
	{	
		// Clean up
		Directory.Delete ("Assets/Automatic/2_UnitTest", true);
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (target);
		
		string target_dir = APP_NAME;
		string[] scenes = { "Assets/Scenes/TestBuild.unity" };
		
		// Test build 
		BuildPipeline.BuildPlayer(scenes, target_dir, target, option);
     }
	///////////////////////////////////////////////////////////////////////////////////////////////////
	
	static void PerformBuildAndroid ()
	{		
		AutomaticBuildScript.PerformBuild (BuildTarget.Android);
	}
	
	static void PerformBuildWindows ()
	{
		AutomaticBuildScript.PerformBuild (BuildTarget.StandaloneWindows);
	}
	
	static void PerformBuildMAC ()
	{
		AutomaticBuildScript.PerformBuild (BuildTarget.StandaloneOSXIntel);
	}
	
	static void PerformBuildIOS ()
	{
		AutomaticBuildScript.PerformBuild (BuildTarget.iPhone);
	}
}
