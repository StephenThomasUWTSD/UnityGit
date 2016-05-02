using UnityEngine;
using System.Collections;

public class DeadState : TankFSMState {

    // Constructor method
    public DeadState()
    {
        // Initialise this state's id
		ID = TankFSMStateID.Dead;
    }

    public override void Reason(Transform player, Transform npc)
    {
        // Do Nothing, not transitioning anywhere
    }

    public override void Act(Transform player, Transform npc)
    {
        // Do Nothing for the dead state
    }
}
