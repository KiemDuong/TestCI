using UnityEngine;
using System.Collections;
using UnityEditor;

public class UnitTestRunScript : MonoBehaviour {

	static void PerformTest () {
		
		EditorApplication.OpenScene("Assets/Scenes/TestCI_NUnit.unity");	
		
		GameObject oneFrameObject = new GameObject ("OneFrameObject");

		oneFrameObject.AddComponent <OneFrameSignaller> ();
		
		EditorApplication.isPlaying = true;
	}
	
	[MenuItem("UnitTest/PerformTest (Windows)")]
	static void PerformTestWindows () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneWindows);
		
		UnitTestRunScript.PerformTest ();
	}
	
	[MenuItem("UnitTest/PerformTest (MAC)")]
	static void PerformTestMAC () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneOSXIntel);
		
		UnitTestRunScript.PerformTest ();
	}
	
	[MenuItem("UnitTest/PerformTest (Android)")]
	static void PerformTestAndroid () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.Android);
		
		UnitTestRunScript.PerformTest ();
	}
	
	[MenuItem("UnitTest/PerformTest (iOS)")]
	static void PerformTestIOS () {
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.iPhone);
		
		UnitTestRunScript.PerformTest ();
	}
}
