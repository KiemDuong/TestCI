using UnityEngine;
#if USE_NUNIT_TEST
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Threading;
 
public class OneFrameSignaller : MonoBehaviour
{
    void Start() 
	{	
        Application.runInBackground = true;
		
		StartCoroutine (signalFrameEnd (3.0f));
    }
    
    IEnumerator signalFrameEnd (float delay)
    {	
		yield return new WaitForSeconds (delay);
		
#if USE_NUNIT_TEST
		EditorApplication.Exit (0);
#endif
    }
}