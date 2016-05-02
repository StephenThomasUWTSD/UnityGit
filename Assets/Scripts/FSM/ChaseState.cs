using UnityEngine;
using System.Collections;

public class ChaseState : TankFSMState {

    // Constructor method
    public ChaseState(Transform[] wp)
    {
        // Initialise speeds
        curRotSpeed = 1.0f;
        curSpeed = 100.0f;


        // Initialise this state's id and the waypoints
		ID = TankFSMStateID.Chasing;
		waypoints = wp;
        
        //find next Waypoint position
		FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {
        //Set the target position as the player position
        destPos = player.position;

        //Check the distance with player tank
        //When the distance is near (<=200), transition to attack state
        float dist = Vector3.Distance(npc.position, destPos);
		AgentFSMTank tank = npc.GetComponent<AgentFSMTank>();
        if (dist <= 200f)
        {
            Debug.Log("Switch to Attack state");
			tank.PerformTransition(TankFSMTransition.ReachPlayer);
            
        }
        //Go back to patrol is it become too far >= 300.0f
        else if (dist >= 300f)
        {
            Debug.Log("Switch to Patrol state");
			tank.PerformTransition(TankFSMTransition.LostPlayer);
            
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        //Rotate to the target point
        destPos = player.position;
		Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
		npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        

        //Go Forward
        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }
}
