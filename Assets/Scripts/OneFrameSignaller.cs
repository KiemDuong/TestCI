using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Threading;
 
public class OneFrameSignaller : MonoBehaviour
{
    public static bool atLeastOneFrameRan = false;
    public static DateTime startTime;
    public static TimeSpan testDuration;    
    
    void Update()
    {
        atLeastOneFrameRan = true;
//        StartCoroutine(signalFrameEnd()); // use this to quit after this frame, you could use a counter instead
        if (DateTime.Now - OneFrameSignaller.startTime > OneFrameSignaller.testDuration) // use this if you want a timed end of the test
        {
			StartCoroutine(signalFrameEnd());
        }
		// Thread.Sleep(20); //this is somewhat evil, us this to extend the execution time 
		// of the frames if you feel they run to fast for useful assertions
    }
 
    void Start() {
        OneFrameSignaller.startTime = DateTime.Now;
        OneFrameSignaller.testDuration = TimeSpan.FromSeconds(3);
        Application.runInBackground = true;
    }
    
    IEnumerator signalFrameEnd()
    {
        yield return new WaitForEndOfFrame();
		
        EditorApplication.isPlaying = false;
    }
}