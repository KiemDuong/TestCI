using UnityEngine;
using System.Collections;

public class Minion : MonoBehaviour 
{

	void OnTriggerEnter (Collider collider )
	{

		string name = collider.gameObject.name;
		string minionName = gameObject.name;

		// Collide bomb
		if(name.Contains("Bomb") && minionName.Contains("Minion1"))
		{
			collider.gameObject.SetActive(false);
			Vector3 localPos = collider.gameObject.transform.localPosition;
			collider.gameObject.transform.localPosition = new Vector3(localPos.x, 2800, localPos.z);
			gameObject.SetActive(false);
			GameData.Instance.isCharacterMoving = true;
		}

		if(name.Contains("Obstacle1") && minionName.Contains("Minion2"))
		{
			collider.gameObject.SetActive(false);
			Vector3 localPos = collider.gameObject.transform.localPosition;
			collider.gameObject.transform.localPosition = new Vector3(localPos.x, 2800, localPos.z);
			gameObject.SetActive(false);
			GameData.Instance.isCharacterMoving = true;
		}

		if(name.Contains("Obstacle2") && minionName.Contains("Minion3"))
		{
			collider.gameObject.SetActive(false);
			Vector3 localPos = collider.gameObject.transform.localPosition;
			collider.gameObject.transform.localPosition = new Vector3(localPos.x, 2800, localPos.z);
			gameObject.SetActive(false);
			GameData.Instance.isCharacterMoving = true;
		}

	}
}
