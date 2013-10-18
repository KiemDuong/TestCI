using UnityEngine;
using UnityEditor;
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
		
		EditorApplication.Exit (0);
    }
}