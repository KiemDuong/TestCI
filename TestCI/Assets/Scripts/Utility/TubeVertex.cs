using UnityEngine;
using System.Collections;

public class TubeVertex 
{
	public Vector3 point = Vector3.zero;
	public float radius = 1.0f;
	public Color color = Color.yellow;

	public TubeVertex (Vector3 pt, float r, Color c)
	{
		point=pt;
		radius=r;
		color=c;
	}
}
