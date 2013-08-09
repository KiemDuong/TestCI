using UnityEngine;
using System.Collections;

/// <summary>
/// FSM state.
/// </summary>
abstract public class FSMState  
{
	abstract public void Enter ();

	abstract public void Execute ();

	abstract public void Exit ();
}
	