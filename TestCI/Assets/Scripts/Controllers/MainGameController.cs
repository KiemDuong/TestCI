using UnityEngine;
using System.Collections;

public enum Mode { Swipe, Tilt, Jump };

public class MainGameController : MonoBehaviour 
{
	
	public GameObject uiRoot;
	public GameObject ropeObject;
	public GameObject character;
	public GameObject left;
	public GameObject left2;
	public GameObject right;
	public GameObject right2;
	public GameObject coin;	
	public GameObject bomb;	
	GameObject[] coins;

	GameObject theRope;
	bool isGameOver;

	const float YMovingSpeed = 800f;
	const float XMovingSpeed = 2000f;
	const float Sensitivity = 0.01f;
	const float SwipeSpeed = 50;

	const int LIMIT_LEFT = -300;
	const int LIMIT_RIGHT = 300;

	// Use this for initialization
	void Start () 
	{
		coins = new GameObject[5];
		for (int i = 0; i < coins.Length; i++)
		{
			coins[i] = Instantiate(coin) as GameObject;
			coins[i].transform.parent = coin.transform.parent;
			coins[i].transform.localPosition = new Vector3(Random.Range(LIMIT_LEFT, LIMIT_RIGHT),
			                                    coin.transform.localPosition.y + Random.Range(-1000, 1000),
			                                    coin.transform.localPosition.z);
			coins[i].transform.localRotation = coin.transform.localRotation;
			coins[i].transform.localScale = coin.transform.localScale;
		}

		// Initialize data
		if(GameData.Instance.ControlMode == Mode.Swipe)
		{

		}
		else if(GameData.Instance.ControlMode == Mode.Tilt)
		{

		}
		else if(GameData.Instance.ControlMode == Mode.Jump)
		{
			SetUpPhysics();
		}
	}

	int w = 100;
	int h = 40;
	void OnGUI ()
	{
		// Back 
		if(GUI.Button( new Rect(0, h, w, h), "Back"))
		{
			Application.LoadLevel("Start");
		}	

		// Point
		GUI.Label( new Rect(Screen.width - w, h, w, h), "   Point: " + GameData.Instance.Point);		
	}

	bool canDraw;
	Vector3 mousePositonDown;
	Vector3 mousePositonUp;
	Vector3 previousTouch;
	Vector3 currentTouch;
	Vector3 AccelerometerDirection;
	Vector3 delta;

	void Update () 
	{
		// Accelerometer
		if(GameData.Instance.ControlMode == Mode.Tilt)
		{	
			AccelerometerDirection = Input.acceleration;

			if (Mathf.Abs(AccelerometerDirection.x) > Sensitivity)
			{
				character.transform.localPosition += new Vector3(AccelerometerDirection.x * Time.deltaTime,
				                                                 0,
				                                                 0) * XMovingSpeed;

				if (character.transform.localPosition.x < LIMIT_LEFT)
					character.transform.localPosition = 
						new Vector3(LIMIT_LEFT, character.transform.localPosition.y, character.transform.localPosition.z);

				if (character.transform.localPosition.x > LIMIT_RIGHT)
					character.transform.localPosition = 
						new Vector3(LIMIT_RIGHT, character.transform.localPosition.y, character.transform.localPosition.z);
			}

			LoopPositonComponents();
		}

		// Swipe mode
		else if (GameData.Instance.ControlMode == Mode.Swipe)
		{
			if (Input.touchCount > 0 && 
			    Input.GetTouch(0).phase == TouchPhase.Moved) 
			{
				// Get movement of the finger since last frame
				Vector2 delta = Input.GetTouch(0).deltaPosition;
				if(Mathf.Abs(delta.x) > Sensitivity)
				{
					character.transform.localPosition += new Vector3(delta.x * Time.deltaTime,
				                                         0,
				                                         0) * SwipeSpeed ;

					if (character.transform.localPosition.x < LIMIT_LEFT)
						character.transform.localPosition = 
							new Vector3(LIMIT_LEFT, character.transform.localPosition.y, character.transform.localPosition.z);

					if (character.transform.localPosition.x > LIMIT_RIGHT)
						character.transform.localPosition = 
							new Vector3(LIMIT_RIGHT, character.transform.localPosition.y, character.transform.localPosition.z);
				}
			}

			LoopPositonComponents();
		}
		// Rope mode
		else
		{
			if (Input.GetMouseButtonDown (0))
			{
				mousePositonDown = Input.mousePosition;
				canDraw = true;
			}

			if (Input.GetMouseButtonUp (0))
			{
				//mousePositonUp = Input.mousePosition;
				//canDraw = false;
			}

			if(canDraw)
			{
				ShowRope();
			}

			LoopPositonComponents();
		}
	}
		
	const int LIMIT_BOTTOM = -1720;
	const int LIMIT_TOP = 2800;
	void LoopPositonComponents ()
	{
		// Bomb
		bomb.transform.localPosition -=  new Vector3(0, Time.deltaTime, 0) * YMovingSpeed;
		bomb.SetActive(true);
		if (bomb.transform.localPosition.y <= LIMIT_BOTTOM)
			bomb.transform.localPosition = new Vector3(bomb.transform.localPosition.x + Random.Range(-250, 250),
			                                               LIMIT_TOP,
			                                           		bomb.transform.localPosition.z);

		// Move coins
		for(int i = 0; i < coins.Length; i++)
		{
			coins[i].transform.localPosition -=  new Vector3(0, Time.deltaTime, 0) * YMovingSpeed;
			coins[i].SetActive(true);
			if (coins[i].transform.localPosition.y <= LIMIT_BOTTOM)
				coins[i].transform.localPosition = new Vector3(coins[i].transform.localPosition.x,
				                                               LIMIT_TOP,
				                                               coins[i].transform.localPosition.z);
		}

		// Move down
		left.transform.localPosition -=  new Vector3(0, Time.deltaTime, 0) * YMovingSpeed;
		right.transform.localPosition -=  new Vector3(0, Time.deltaTime, 0) * YMovingSpeed;
		left2.transform.localPosition -=  new Vector3(0, Time.deltaTime, 0) * YMovingSpeed;
		right2.transform.localPosition -=  new Vector3(0, Time.deltaTime, 0) * YMovingSpeed;

		// Check position & adjust
		if (left.transform.localPosition.y <= LIMIT_BOTTOM)
			left.transform.localPosition = new Vector3(left.transform.localPosition.x, LIMIT_TOP,left.transform.localPosition.z);
		if (right.transform.localPosition.y <= LIMIT_BOTTOM)
			right.transform.localPosition = new Vector3(right.transform.localPosition.x, LIMIT_TOP,right.transform.localPosition.z);
		if (left2.transform.localPosition.y <= LIMIT_BOTTOM)
			left2.transform.localPosition = new Vector3(left2.transform.localPosition.x, LIMIT_TOP,left2.transform.localPosition.z);
		if (right2.transform.localPosition.y <= LIMIT_BOTTOM)
			right2.transform.localPosition = new Vector3(right2.transform.localPosition.x, LIMIT_TOP,right2.transform.localPosition.z);
	}

	/// <summary>
	/// Sets up physics for Jump mode
	/// </summary>
	void SetUpPhysics()
	{
		Rigidbody rigid = character.GetComponent<Rigidbody> ();
		character.transform.localPosition += new Vector3(0, 400, 0);
		rigid.isKinematic = false;
		rigid.useGravity = true;
		rigid.constraints = RigidbodyConstraints.None;
		rigid.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
	}

	float tmp;
	void ShowRope ()
	{
//		float xPos = mousePositonDown.x * uiRoot.GetComponent<UIRoot>().pixelSizeAdjustment;
//		float yPos = mousePositonDown.y * uiRoot.GetComponent<UIRoot>().pixelSizeAdjustment;
	
		tmp =ropeObject.transform.position.y;
		ropeObject.transform.position = 
			camera.ScreenToWorldPoint(new Vector3(mousePositonDown.x, mousePositonDown.y, ropeObject.transform.localPosition.z));
		ropeObject.SetActive(true);

        // Adjust postion
		float deltaY = ropeObject.transform.position.y - tmp;
		left.transform.position -=  new Vector3(0, deltaY, 0);
		right.transform.position -=  new Vector3(0, deltaY, 0);
		left2.transform.position -=  new Vector3(0, deltaY, 0);
		right2.transform.position -=  new Vector3(0, deltaY, 0);

		canDraw = false;
	}

	void BuildRope2()
	{
		if(theRope) DestroyImmediate(theRope);

		theRope = Instantiate(ropeObject) as GameObject;
		theRope.SetActive(true);
		theRope.transform.position = mousePositonDown;
	}

}
