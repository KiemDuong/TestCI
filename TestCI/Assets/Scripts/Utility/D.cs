using UnityEngine;
using System.Collections;

public static class D  
{

	/// <summary>
	/// Log: log message in development phase
	/// </summary>
	/// <param name="msg">Message.</param>
	public static void Log(string msg)
	{
		if (Config.IsDevelopment)
			Debug.Log(msg);
	}
}
