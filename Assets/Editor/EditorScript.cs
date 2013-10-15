using UnityEngine;
using System.Collections;
using UnityEditor;

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
}
