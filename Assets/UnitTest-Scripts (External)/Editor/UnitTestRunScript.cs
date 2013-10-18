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
		
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Standalone, "USE_NUNIT_TEST");
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneWindows);
		
		UnitTestRunScript.PerformTest ();
	}
	
	[MenuItem("UnitTest/PerformTest (MAC)")]
	static void PerformTestMAC () {
		
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Standalone, "USE_NUNIT_TEST");
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.StandaloneOSXIntel);
		
		UnitTestRunScript.PerformTest ();
	}
	
	[MenuItem("UnitTest/PerformTest (Android)")]
	static void PerformTestAndroid () {
		
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Android, "USE_NUNIT_TEST");
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.Android);
		
		UnitTestRunScript.PerformTest ();
	}
	
	[MenuItem("UnitTest/PerformTest (iOS)")]
	static void PerformTestIOS () {
		
		PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.iPhone, "USE_NUNIT_TEST");
		
		EditorUserBuildSettings.SwitchActiveBuildTarget (BuildTarget.iPhone);
		
		UnitTestRunScript.PerformTest ();
	}
}
