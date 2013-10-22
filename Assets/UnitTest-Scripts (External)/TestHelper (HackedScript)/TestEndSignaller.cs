using UnityEngine;
#if USE_NUNIT_TEST
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Threading;
 
public interface TestEndListener {
	bool IsTestEnd ();	
	bool IsWaitForOneFrame ();
}

public class TestEndSignaller : MonoBehaviour
{
	public TestEndListener endTestListener = null;
	
    void Start() 
	{	
        Application.runInBackground = true;
		
		StartCoroutine (FinishedTest ());
    }
	
	IEnumerator FinishedTest () {
		
		while (endTestListener == null || !endTestListener.IsTestEnd ()) {
			yield return null;	
		}
		
		if (endTestListener == null || endTestListener.IsWaitForOneFrame ()) {
			yield return null;
		}
		
#if USE_NUNIT_TEST
		EditorApplication.Exit (0);
#endif
		
	}
}