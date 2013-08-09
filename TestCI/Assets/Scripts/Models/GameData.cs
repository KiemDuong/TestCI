using System;

/// <summary>
/// Model.
/// </summary>
public class GameData
{
	static readonly GameData instance = new GameData();

	public static GameData Instance
	{
		get
		{
			return instance;
		}
	}

	public int Point;

	public int NumOfLife = 3;

	public Mode ControlMode = Mode.Swipe;

	public bool isCharacterMoving = true;
}

