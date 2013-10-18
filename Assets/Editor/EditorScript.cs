using UnityEngine;
using System.Collections;
using UnityEditor;
using SharpUnit;

public class EditorScript
{
	 static string APP_NAME = "TestCI";
	
	 ///////////////////////////////////////////////////////////////////////////////////////////////////
	 static void PerformBuild (BuildTarget target, BuildOptions option = BuildOptions.None)
     {
		EditorUserBuildSettings.SwitchActiveBuildTarget (target);
		
		string target_dir = APP_NAME;
		string[] scenes = { "Assets/Scenes/TestBuild.unity" };
		
		// Test build 
		BuildPipeline.BuildPlayer(scenes, target_dir, target, option);
     }
	
	static void PerformTest () {
		
		EditorApplication.OpenScene("Assets/Scenes/TestCI_NUnit.unity");	
		
		GameObject oneFrameObject = new GameObject ("OneFrameObject");

		oneFrameObject.AddComponent <OneFrameSignaller> ();
		
		EditorApplication.isPlaying = true;
	}
	///////////////////////////////////////////////////////////////////////////////////////////////////
	
	static void PerformBuildAndroid ()
	{		
		EditorScript.PerformBuild (BuildTarget.Android);
	}
	
	static void PerformBuildWindows ()
	{
		EditorScript.PerformBuild (BuildTarget.StandaloneWindows);
	}
	
	static void PerformBuildMAC ()
	{
		EditorScript.PerformBuild (BuildTarget.StandaloneOSXIntel);
	}
	
	static void PerformBuildIOS ()
	{
		EditorScript.PerformBuild (BuildTarget.iPhone);
	}
	
	[MenuItem("Window/PerformTest (Windows)")]
	static void PerformTestWindows () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneWindows);
		
		EditorScript.PerformTest ();
	}
	
	[MenuItem("Window/PerformTest (MAC)")]
	static void PerformTestMAC () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneOSXIntel);
		
		EditorScript.PerformTest ();
	}
	
	[MenuItem("Window/PerformTest (Android)")]
	static void PerformTestAndroid () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.Android);
		
		EditorScript.PerformTest ();
	}
	
	[MenuItem("Window/PerformTest (iOS)")]
	static void PerformTestIOS () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.iPhone);
		
		EditorScript.PerformTest ();
	}
}
