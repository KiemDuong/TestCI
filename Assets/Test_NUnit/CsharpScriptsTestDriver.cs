#region Usings

using UnityEngine;

using NUnitLite.Runner;

using System.IO;

using System.Reflection;

#endregion

public class CsharpScriptsTestDriver : MonoBehaviour
{
    #region Editor Fields

    public bool RunTests;

    #endregion



    #region Unity Callbacks

    private void Start()
    {
        if (RunTests) {
//            NUnitLiteUnityRunner.RunTests();
			using (var sw = new StringWriter())
	        {
	            var runner = new TextUI(sw);
	            runner.Execute(new string[] {"/result:TestResult.xml"});
	        }
		}
    }

    #endregion

}
