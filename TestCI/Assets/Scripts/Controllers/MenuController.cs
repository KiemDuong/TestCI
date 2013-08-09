using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
		
	void Start () 
	{
		Application.targetFrameRate = 60;
	}
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			Application.LoadLevel("Start");
		}

	}
}
