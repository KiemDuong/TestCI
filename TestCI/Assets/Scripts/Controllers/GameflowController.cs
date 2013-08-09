using UnityEngine;
using System.Collections;

public class GameflowController : MonoBehaviour 
{

	private FiniteStateMachine fsm;

	// Use this for initialization
	void Start () 
	{
		D.Log("GameflowController Start()");
		fsm = new FiniteStateMachine();
		fsm.Configure(this, BootstrapState.Instance);
	}

	// Update is called once per frame
	void Update () 
	{
		fsm.Update ();	
	}
	

	/// <summary>
	/// Changes the state.
	/// </summary>
	/// <param name="e">E.</param>
	public void ChangeState (FSMState newState)
	{
		D.Log("GameflowController ChangeState()");
		fsm.ChangeState (newState);
	}
}
