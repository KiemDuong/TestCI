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
	
	[MenuItem("Window/PerformTest")]
	static void PerformTest () {
		
		EditorApplication.OpenScene("Assets/Scenes/TestCI.unity");	
		
		EditorApplication.playmodeStateChanged = EditorScript.Finished;
		
		EditorApplication.isPlaying = true;
		
		GameObject oneFrameObject = new GameObject ("OneFrameObject");

		oneFrameObject.AddComponent <OneFrameSignaller> ();
	}
	
	public static void Finished () {
		if (EditorApplication.isPlaying) {
			EditorApplication.Exit (0);
		}
	}
}
