using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorScript
{
	 static string APP_NAME = "TestCI";
	 
	 static void PerformBuild ()
     {
		 string target_dir = APP_NAME + ".apk";
         string[] scenes = { "Assets/Scenes/TestCI.unity" };
		
		 // Test build on Android
         BuildPipeline.BuildPlayer(scenes, target_dir, BuildTarget.Android, BuildOptions.None);
     }
}
