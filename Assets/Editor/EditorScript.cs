using UnityEngine;
using System.Collections;
using UnityEditor;
using SharpUnit;

public class EditorScript
{
	 static string APP_NAME = "TestCI";
	 
	 static void PerformBuild ()
     {
		string target_dir = APP_NAME;
		string[] scenes = { "Assets/Scenes/TestCI.unity" };
		
		// Test build 
		BuildPipeline.BuildPlayer(scenes, target_dir, BuildTarget.StandaloneWindows, BuildOptions.None);
     }
	
	static OneFrameSignaller comp = null;
	[MenuItem("Window/PerformTest")]
	static void PerformTest () {
		
		EditorApplication.playmodeStateChanged = Finished;
		
		EditorApplication.OpenScene("Assets/Scenes/TestCI.unity");	
		
		EditorApplication.isPlaying = true;
		
		GameObject testRunner = GameObject.Find ("TestRunner");

		if (testRunner != null) {
			comp = testRunner.AddComponent <OneFrameSignaller> ();
		}
	}
	
	static void Finished () {
		
		if (!EditorApplication.isPlaying) {
			if (comp != null) {
				Object.Destroy (comp);	
			}
			
			EditorApplication.Exit(0);
		}
	}
}
