using UnityEngine;
using System.Collections;


/// <summary>
/// Boot strap controller: starting point to access the game
/// This will be attached to a bootstrap prefab
/// </summary>
public class BootStrapController : MonoBehaviour 
{
	public GameObject ninja;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(JumpIn ());
		UIEventListener.Get(ninja).onClick += (go) =>
		{
//			Application.LoadLevel("Main");
		};
	}

	/// <summary>
	/// Jumps in.
	/// </summary>
	/// <returns></returns>
	IEnumerator JumpIn() 
	{	
		yield return new WaitForSeconds(0.5f);
		TweenPosition.Begin (ninja, 0.2f, new Vector3(0.5f, 0, -10));
	}

	int w = 100;
	int h = 50;
	void OnGUI ()
	{
		// Back 
		if(GUI.Button( new Rect(0, 10, 100, 50), "Back"))
		{
			Application.LoadLevel("Menu");
		}

		if(GUI.Button( new Rect(Screen.width -  w, h, w, h), "Mission-Swipe"))
		{
			GameData.Instance.ControlMode = Mode.Swipe;
			Application.LoadLevel("Main");
		}
		if(GUI.Button( new Rect(Screen.width -  w, h*3, w, h), "Mission-Tilt"))
		{
			GameData.Instance.ControlMode = Mode.Tilt;
			Application.LoadLevel("Main");
		}
		if(GUI.Button( new Rect(Screen.width -  w, h*5, w, h), "Steal-Swipe"))
		{
			GameData.Instance.ControlMode = Mode.Jump;
			Application.LoadLevel("JumpPlatform");
		}
		if(GUI.Button( new Rect(Screen.width -  w, h*7, w, h), "Steal-Auto"))
		{
			Application.LoadLevel("JumpPlatform2");
		}


//		if(GUI.Button( new Rect(Screen.width/2 -  w/2, Screen.height - h, w, h),"Test Rope"))
//		{
//			Application.LoadLevel("TestRope");
//		}
	}
}
