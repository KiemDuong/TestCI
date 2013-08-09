using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Finite state machine.
/// </summary>
public class FiniteStateMachine
{
  
	List<FSMState> states;
	private GameflowController Owner;
	private FSMState CurrentState;
	private FSMState PreviousState;
	private FSMState GlobalState;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	public void Awake ()
	{
			CurrentState = null;
			PreviousState = null;
			GlobalState = null;
	}

	/// <summary>
	/// Configure the specified owner and InitialState.
	/// </summary>
	/// <param name="owner">Owner.</param>
	/// <param name="InitialState">Initial state.</param>		
	public void Configure (GameflowController owner, FSMState initialState)
	{
		Owner = owner;
		ChangeState (initialState);
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void  Update ()
	{
		if (GlobalState != null)
				GlobalState.Execute ();
		if (CurrentState != null)
				CurrentState.Execute ();
	}

	/// <summary>
	/// Changes the state.
	/// </summary>
	/// <param name="NewState">New state.</param>
	public void  ChangeState (FSMState NewState)
	{
		PreviousState = CurrentState;
		if (CurrentState != null)
				CurrentState.Exit ();
		CurrentState = NewState;
		if (CurrentState != null)
				CurrentState.Enter ();
	}

	/// <summary>
	/// Reverts the state of the to previous.
	/// </summary>
	public void  RevertToPreviousState ()
	{
		if (PreviousState != null)
				ChangeState (PreviousState);
	}
}