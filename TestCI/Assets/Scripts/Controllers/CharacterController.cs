using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
	bool isGameOver;

	// Sound
	public AudioSource audioSource;
	public AudioClip takeCoinClip;
	public AudioClip explosion;      

	const float MIN_HIGHT = 2800f;

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="collider">Collider.</param>
	void OnTriggerEnter (Collider collider )
	{
		string name = collider.gameObject.name;

		// Collide coin
		if(name.Contains("Coin"))
		{
			audioSource.PlayOneShot(takeCoinClip);
			collider.gameObject.SetActive(false);
			Vector3 localPos = collider.gameObject.transform.localPosition;
			collider.gameObject.transform.localPosition = new Vector3(localPos.x, MIN_HIGHT, localPos.z);
			GameData.Instance.Point += 1;
		}

		// Collide bomb
		if(name.Contains("Bomb"))
		{
			audioSource.PlayOneShot(explosion);
			collider.gameObject.SetActive(false);
			Vector3 localPos = collider.gameObject.transform.localPosition;
			collider.gameObject.transform.localPosition = new Vector3(localPos.x, MIN_HIGHT, localPos.z);
			GameData.Instance.NumOfLife--;
		}

		// Collide Obstacle
		if(name.Contains("Obstacle"))
		{
			collider.gameObject.SetActive(false);
			Vector3 localPos = collider.gameObject.transform.localPosition;
			collider.gameObject.transform.localPosition = new Vector3(localPos.x, MIN_HIGHT, localPos.z);
			GameData.Instance.NumOfLife--;
		}
	}
}
