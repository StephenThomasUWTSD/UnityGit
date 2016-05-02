using UnityEngine;
using System.Collections;

public class PatrolState : TankFSMState {

    public PatrolState(Transform[] wp)
    {
        // Initialise speeds
        curRotSpeed = 1.0f;
        curSpeed = 100.0f;

        // Initialise this state's id and the waypoints
        ID = TankFSMStateID.Patrolling;
        waypoints = wp;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //Check the distance with player tank
        //When the distance is near, transition to chase state <= 300.0f
		float dist = Vector3.Distance(npc.position, destPos);
		AgentFSMTank tank = npc.GetComponent<AgentFSMTank>();
        if (dist <= 300f)
        {
            Debug.Log("Switch to Chase State");
			tank.PerformTransition(TankFSMTransition.SawPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        //Find another random patrol point if the current point is reached <= 100.0f
		float dist = Vector3.Distance(npc.position, destPos);
        if (dist <= 100f)
        {
            // Get next point
            Debug.Log("Reached to the destination point\ncalculating the next point");
            FindNextPoint();

        }

        //Rotate to the target point
		Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
		npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);


        //Go Forward
        npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }
}
