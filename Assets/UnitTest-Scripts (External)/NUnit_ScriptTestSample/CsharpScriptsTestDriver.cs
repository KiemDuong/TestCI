#region Usings

using UnityEngine;

using NUnitLite.Runner;

using System.IO;

using System.Reflection;

#endregion

public class CsharpScriptsTestDriver : MonoBehaviour, TestEndListener
{
	bool isTestFinished = false;

    #region Unity Callbacks

    private void Start()
    {
		TestEndSignaller testEnd = gameObject.AddComponent <TestEndSignaller> ();
		testEnd.endTestListener = this;
		
        using (var sw = new StringWriter())
        {
			if (!Directory.Exists ("UnitTest")) {
				Directory.CreateDirectory ("UnitTest");	
			}
			
            var runner = new TextUI(sw);
            runner.Execute(new string[] {"/format:nunit2", "/result:UnitTest/TestResult.xml"});
			isTestFinished = true;
        }
    }
	
	public bool IsTestEnd () {
		return isTestFinished;
	}
	
	public bool IsWaitForOneFrame () {
		return true;	
	}
	
    #endregion

}
