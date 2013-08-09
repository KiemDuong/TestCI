using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour 
{

	float rotationspeed = 100;
	void Update()
	{
		float rotation = rotationspeed*Time.deltaTime;
		if(rotation > 360)
		{
			rotation = 0;
		}
		transform.Rotate(0,rotation,0);
	}
}
