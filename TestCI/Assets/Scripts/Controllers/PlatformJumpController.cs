using UnityEngine;
using System.Collections;

public class PlatformJumpController : MonoBehaviour 
{
	// Set in editor
	public GameObject character;
	public GameObject row;
	public GameObject background;
	public Material[] materials;
	public GameObject coin;
	public GameObject obstacle;
	public GameObject bomb;

	public AudioSource audioSource;
	public AudioClip jumpClip;
	public AudioClip takeCoinClip;

	bool isMoveRight;
	bool jumping = true;
	Vector3 Vo = new Vector3(0, 17, 0);
	Vector3 velocity;
	const float gravity = 30.81f;
	Vector3 tmpPosition;
	int currentFloor;		

	const int MAX_FLOOR = 25;
	float MARGIN_LEFT = -10f;
	float MARGIN_RIGHT = 10f;
	const float Sensitivity = 20f;
	const float XMovingSpeed = 5f;
	Vector3 AccelerometerDirection;

	const int DISTANCE = 4;
	const float BG_MOVE_RATIO = 1.1f;
	private Vector3 startpos;
	private Vector3 endpos;
	float delta;
	bool isDead;

	void Start ()
	{
//		Camera.main.orthographicSize = 12;
//		MARGIN_LEFT = -Camera.main.orthographicSize / 2;
//		MARGIN_RIGHT = Camera.main.orthographicSize/2;
	}

	int w = 100;
	int h = 40;
	void OnGUI ()
	{
		// Back 
		if(GUI.Button( new Rect(0, 10, 100, 50), "Back"))
		{
			Application.LoadLevel("Start");
		}
				
		// Life
		GUI.Label( new Rect(Screen.width - w, h, w, h), "   Life " + GameData.Instance.NumOfLife);	

		// DEBUG
		GUI.Label(new Rect(0, 70, 100, 50), delta.ToString());
	}


	void Update () 
	{

		if (Input.touchCount > 0 && 
		    Input.GetTouch(0).phase == TouchPhase.Began)
		{
			startpos = Input.GetTouch (0).position;
		}
		else if (Input.touchCount > 0 && 
		    Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			 endpos = Input.GetTouch (0).position;
			 delta = (endpos - startpos).x;

			if ( Mathf.Abs(delta) < Sensitivity && !jumping && currentFloor < MAX_FLOOR - 1)
			{
				jumping = true;
				velocity = Vo;
				currentFloor++;

				// Jump sound
				audioSource.PlayOneShot(jumpClip);

				UpdatePlatform();
			}

			// Swipe to change moving direction
			if(delta > Sensitivity)
				isMoveRight = true;	
			if(delta < -Sensitivity)
				isMoveRight = false;
		}


#if UNITY_EDITOR

		// For testing on Editor
		if ( Input.GetMouseButton(0) && !jumping && currentFloor < MAX_FLOOR - 1)
		{
			jumping = true;
			velocity = Vo;
			currentFloor++;

			// Jump sound
			audioSource.PlayOneShot(jumpClip);

			UpdatePlatform();
		}
#endif
		CheckLife();

		//Save position
		if (!isDead) UpdatePosition();
	}

	float deltaPos;
	bool isStop;
	float jumpTime;
	void UpdatePosition()
	{
		if(!jumping)
		{
			if (isMoveRight)
			{
				character.transform.position += new Vector3(Time.deltaTime * XMovingSpeed,
				                                                 0,
				                                                 0) ;


			}
			else
			{
				character.transform.position -= new Vector3(Time.deltaTime * XMovingSpeed,
				                                            0,
				                                            0);
			}

			// Check for edge
			if (character.transform.position.x < MARGIN_LEFT)
				character.transform.position = 
					new Vector3(MARGIN_RIGHT, character.transform.position.y, character.transform.position.z);

			if (character.transform.position.x > MARGIN_RIGHT)	
				character.transform.position = 
					new Vector3(MARGIN_LEFT, character.transform.position.y, character.transform.position.z);

			return;
		}

		// Character jumps!!!
		tmpPosition = character.transform.position;
		velocity.y -= (gravity * Time.deltaTime);
		character.transform.position += velocity * Time.deltaTime;

		// Move camera & background when character is higher than camera
		if(camera.transform.position.y < character.transform.position.y)
		{
			camera.transform.position += velocity * Time.deltaTime;
			background.transform.position += velocity * Time.deltaTime / BG_MOVE_RATIO;
		}

		float targetPos = currentFloor * DISTANCE - 7 + 1.8f;
		deltaPos = character.transform.position.y - tmpPosition.y;

		// Set position to next floor if character fall down
		if(deltaPos < 0 &&  Mathf.Abs(targetPos - character.transform.position.y) < 0.2f)
		{
			Vector3 tmpPos = character.transform.position;
			tmpPos.y = targetPos;
			character.transform.position = tmpPos;

			jumping = false;
		}
	}

	/// <summary>
	/// Remove old rows, add new rows
	/// </summary>
	void UpdatePlatform ()
	{

		if(currentFloor + 7 > MAX_FLOOR) return;

		// Add new one
		GameObject newRow = Instantiate(row) as GameObject;
		newRow.name = "Cube" + (currentFloor + 7);
		Vector3 tmp = row.transform.position;
		tmp.y += DISTANCE;
		row.transform.position = tmp;
		row.renderer.material = materials[Random.Range(0, 2)];

		// TODO: remove row at bottom 


		// New items: coins, bombs,...
		GameObject newObstacle = Instantiate(obstacle) as GameObject;
		newObstacle.name = "Obstacle" + (currentFloor + 7);
		newObstacle.transform.position = tmp + new Vector3(Random.Range(-5,5),0,0);


		// Coins
		tmp.y += DISTANCE/2;
		for (int i = 0; i < Random.Range(1,3); i++)
		{
			tmp.x = Random.Range(MARGIN_LEFT, MARGIN_RIGHT);
			GameObject newCoin = Instantiate(coin) as GameObject;
			newCoin.name = "Coin" + (currentFloor + 7);
			newCoin.transform.position = tmp;
		}

		// Bombs
		for (int i = 0; i < Random.Range(1,3); i++)
		{
			tmp.x = Random.Range(MARGIN_LEFT, MARGIN_RIGHT);
			GameObject aBomb = Instantiate(bomb) as GameObject;
			aBomb.name = "Bomb" + (currentFloor + 7);
			aBomb.transform.position = tmp;
		}

	}

	void CheckLife()
	{
		if(GameData.Instance.NumOfLife <= 0)
		{
			isDead = true;
			StartCoroutine(RestartScene());
		}			
	}

	IEnumerator RestartScene ()
	{
		yield return new  WaitForSeconds(1);
		GameData.Instance.NumOfLife = 3;
		isDead = false; 
		Application.LoadLevel("JumpPlatform");
	}
}
