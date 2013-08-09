using UnityEngine;
using System.Collections;

public class RopeController : MonoBehaviour {

	public GameObject left;
	public GameObject right;

	public GameObject sphere;

	bool canDraw;
	// Use this for initialization
	void Start () {
	
	}
	
	int w = 100;
	int h = 30;
	void OnGUI ()
	{
		if(GUI.Button( new Rect(Screen.width/2 -  w/2, Screen.height - h, w, h), "Back"))
		{
			Application.LoadLevel("Start");
		}
	}

	Vector3 mousePositonDown;
	Vector3 mousePositonUp;
	void Update () 
	{
		if (Input.GetMouseButtonDown (0))
		{
			mousePositonDown = Input.mousePosition;
			canDraw = false;
		}

		if (Input.GetMouseButtonUp (0))
		{
			mousePositonUp = Input.mousePosition;
			canDraw = true;
		}

		if(canDraw)
			ShowRope();
		else 
			HideRope();
	}

	void ShowRope ()
	{
//		left.transform.position = camera.ScreenToWorldPoint(mousePositonDown);
//		left.SetActive(true);
//		right.transform.position = camera.ScreenToWorldPoint(mousePositonUp);
//		right.SetActive(true);

		GameObject left = Instantiate(sphere) as GameObject;
		left.transform.position = camera.ScreenToWorldPoint(mousePositonDown);
		left.SetActive(true);

		GameObject right = Instantiate(sphere) as GameObject;
		right.transform.position = camera.ScreenToWorldPoint(mousePositonUp);
		right.SetActive(true);

//		left.AddComponent("Rope_Line");
//		Rope_Line rope = left.GetComponent("Rope_Line");
//		rope.target = right.transform;
		canDraw = false;
	}

	void HideRope ()
	{
//		left.SetActive(false);
//		right.SetActive(false);
	}
}
