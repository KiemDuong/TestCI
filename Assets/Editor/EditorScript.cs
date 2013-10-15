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
		
		// Create test suite
        TestSuite suite = new TestSuite();

        // Example: Add tests to suite
        suite.AddAll(new Dummy_Test());

        // Run the tests
        TestResult res = suite.Run(null);

        // Report results
        Unity3D_TestReporter reporter = new Unity3D_TestReporter();
        reporter.LogResults(res);

		XML_TestReporter xmlReporter = new XML_TestReporter();
		xmlReporter.Init("CI_test.xml");
		xmlReporter.LogResults(res);
     }
}
