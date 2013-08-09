using UnityEngine;
using System.Collections;

public class PlatformJumpController2 : MonoBehaviour 
{
	// Set in editor
	public GameObject character;
	public GameObject row;
	public GameObject background;
	public Material[] materials;
	public GameObject coin;
	public GameObject obstacle;
	public GameObject[] minions;

	public Texture2D bombTexture;
	public Texture2D obstacle1Texture;
	public Texture2D obstacle2Texture;
	public GUIStyle style;

	public AudioSource audioSource;
	public AudioClip jumpClip;
	public AudioClip takeCoinClip;

	bool isMoveRight;
	bool jumping = true;
	Vector3 VoRight = new Vector3(1, 17, 0);
	Vector3 VoLeft = new Vector3(-1, 17, 0);
	Vector3 velocity;
	const float gravity = 30.81f;
	Vector3 tmpPosition;
	int currentFloor;		

	const int MAX_FLOOR = 7;
	float MARGIN_LEFT = -10f;
	float MARGIN_RIGHT = 10f;
	const float Sensitivity = 20f;
	float XMovingSpeed = 2f;
	const float MinionSpeed = 7f;
	Vector3 AccelerometerDirection;

	const int DISTANCE = 4;
	const float BG_MOVE_RATIO = 1.1f;
	private Vector3 startpos;
	private Vector3 endpos;
	float delta;
	bool isDead;
	int minionType = 1;
	GameObject minion;

	void Start ()
	{
		minion = minions[0];
		minion.SetActive(false);

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

		// Adjust speed
		if(GUI.Button( new Rect(Screen.width - 50, 10, 50, 50), "+"))
		{
			XMovingSpeed++;
		}
		if(GUI.Button( new Rect(Screen.width - 50, 70, 50, 50), "-"))
		{
			XMovingSpeed--;
		}


		if(GUI.Button( new Rect(0, 80, 100, 50), bombTexture, style))
		{
			UpdateMinion(1);
		}
		if(GUI.Button( new Rect(0, 150, 100, 50), obstacle1Texture, style))
		{
			UpdateMinion(2);
		}
		if(GUI.Button( new Rect(0, 220, 100, 50), obstacle2Texture, style))
		{
			UpdateMinion(3);
		}

		// Life
		GUI.Label( new Rect(Screen.width - w, h, w, h), "   Life " + GameData.Instance.NumOfLife);	

		// DEBUG
		GUI.Label(new Rect(0, 70, 100, 50), delta.ToString());
	}

	void UpdateMinion (int type)
	{
		foreach (GameObject m in minions)
			m.SetActive(false);

		minionType = type;
		minion = minions[type - 1];
		minion.transform.position = character.transform.position;
		minion.SetActive(true);
		GameData.Instance.isCharacterMoving = false;
	}

	void Update () 
	{					
		CheckLife();

		//Save position
		if (!isDead) UpdatePosition();
	}

	float deltaPos;
	bool isStop;
	float jumpTime;
	void UpdatePosition()
	{
		if (currentFloor >= MAX_FLOOR - 1)
			StartCoroutine(RestartScene());

		if(!jumping)
		{
			if (isMoveRight)
			{
				if(GameData.Instance.isCharacterMoving)
					character.transform.position += new Vector3(Time.deltaTime * XMovingSpeed,
				                                                 0,
				                                                 0) ;
				else
					minion.transform.position += new Vector3(Time.deltaTime * MinionSpeed,
					                                            0,
					                                            0) ;



			}
			else
			{
				if(GameData.Instance.isCharacterMoving)
					character.transform.position -= new Vector3(Time.deltaTime * XMovingSpeed,
				                                            0,
				                                            0);
				else 
					minion.transform.position -= new Vector3(Time.deltaTime * MinionSpeed,
					                                            0,
					                                            0);
			}

			// Check for edge and jump
			// Jump right
			if (character.transform.position.x < MARGIN_LEFT)
			{
				jumping = true;
				velocity = VoRight;
				currentFloor++;

				// Jump sound
				audioSource.PlayOneShot(jumpClip);

				UpdatePlatform();
				isMoveRight = !isMoveRight;
			}
			
			// Jump left
			if (character.transform.position.x > MARGIN_RIGHT)	
			{
				jumping = true;
				velocity = VoLeft;
				currentFloor++;

				// Jump sound
				audioSource.PlayOneShot(jumpClip);

				UpdatePlatform();
				isMoveRight = !isMoveRight;
			}

			if (minion.transform.position.x < MARGIN_LEFT || minion.transform.position.x > MARGIN_RIGHT)
			{
				GameData.Instance.isCharacterMoving = true;
				minion.SetActive(false);
			}

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
		Application.LoadLevel("JumpPlatform2");
	}
}
