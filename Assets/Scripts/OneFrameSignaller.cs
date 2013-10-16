using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Threading;
 
public class OneFrameSignaller : MonoBehaviour
{
    void Start() {
		
        Application.runInBackground = true;
		
		StartCoroutine (signalFrameEnd (3.0f));
    }
    
    IEnumerator signalFrameEnd (float delay)
    {	
		Debug.LogError ("AAAAA!");
		
		yield return new WaitForSeconds (delay);
		
		Debug.LogError ("BBBBB!");
		
        yield return new WaitForEndOfFrame();
		
		Debug.LogError ("CCCC!");
		
        EditorApplication.isPlaying = false;
    }
}