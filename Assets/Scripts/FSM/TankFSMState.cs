using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TankFSMState: FSMState {
    
    public TankFSMState()
    {
        stateNames = System.Enum.GetNames(typeof(TankFSMStateID));
    }

    // Implement ID, so can use easier TankFSMStateID enum values
	public new TankFSMStateID ID
	{
		get{return (TankFSMStateID) base.ID;}
		set{base.ID = (int)value;}
	}

    // Overload the AddTransition method, so can use easier TankFSMStateID and TankFSMTransition enum values
    public void AddTransition(TankFSMTransition transition, TankFSMStateID stateID)
    {
        base.AddTransition(transition.ToString(), (int)stateID);
    }
}

// Define the TankFSMTransition states
public enum TankFSMTransition
{
    None = FSMTransition.None,
	SawPlayer,
	ReachPlayer,
	LostPlayer,
	NoHealth
    
}

// Define the TankFSMStateID values
public enum TankFSMStateID
{
    None = FSMStateID.None,
    Patrolling,
	Chasing,
	Attacking,
	Dead
}
