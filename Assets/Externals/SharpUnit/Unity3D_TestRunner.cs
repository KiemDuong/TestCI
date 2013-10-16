/**
 * @file TestRunner.cs
 * 
 * Unity3D unit test runner.
 * Sets up the unit testing suite and executes all unit tests.
 * Drag this onto an empty GameObject to run tests.
 */

using UnityEngine;
using System.Collections;
using SharpUnit;

public class Unity3D_TestRunner : MonoBehaviour 
{
    /**
     * Initialize class resources.
     */
	void Start() 
    {
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
		
		StartCoroutine (TestOneFrame ());
	}
	
	IEnumerator TestOneFrame () {
		
		yield return new WaitForEndOfFrame ();
		
		Debug.LogError ("DDDDDD!!!");
	}
}
