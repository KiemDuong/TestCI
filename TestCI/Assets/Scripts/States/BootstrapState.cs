using UnityEngine;
using System.Collections;

public sealed class BootstrapState : FSMState
{
	
	static readonly BootstrapState instance = new BootstrapState();

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static BootstrapState Instance 
	{
		get
		{
			return instance;
		}
	}

	/// <summary>
	/// Enter the specified miner.
	/// </summary>
	/// <param name="miner">Miner.</param>
	public override void Enter()
	{

		// Load data

		// Load controller

		// Load view 
	}

	/// <summary>
	/// Execute the specified entity.
	/// </summary>
	/// <param name="entity">Entity.</param>
	public override void Execute ()
	{

	}

	/// <summary>
	/// Exit the specified entity.
	/// </summary>
	/// <param name="entity">Entity.</param>
	public override void Exit ()
	{

	}

}
