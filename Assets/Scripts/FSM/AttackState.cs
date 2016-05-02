using UnityEngine;
using System.Collections;

public class AttackState : TankFSMState {

    public AttackState(Transform[] wp)
    {
        // Initialise this state's id and the waypoints
		ID = TankFSMStateID.Attacking;
		waypoints = wp;
        
    }
	
    // Determine if need to transition state
    public override void Reason(Transform player, Transform npc)
    {
        // Check the distance with the player tank
        // Transition to Chase state if distance between 200 and 300
        float dist = Vector3.Distance(npc.position, destPos);
		AgentFSMTank tank = npc.GetComponent<AgentFSMTank>();
        if (dist >= 200f && dist <= 300f)
        {
            //Rotate to the target point
            Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
			npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * curRotSpeed);

            //Go Forward
            npc.Translate(Vector3.forward * Time.deltaTime * curSpeed);

            // Set transition to Chase State
            Debug.Log("Switch to Chase State");
			tank.PerformTransition(TankFSMTransition.SawPlayer);
            

        } 
        // Else transition to Patrol is the tank become too far >= 300
        else if (dist>= 300f)
        {
            Debug.Log("Switch to Patrol State");
            tank.PerformTransition(TankFSMTransition.LostPlayer);

        }
    }

    public override void Act(Transform player, Transform npc)
    {
        //Set the target position as the player position
        destPos = player.position;
        
		AgentFSMTank tank = npc.GetComponent<AgentFSMTank>();
		
        //Always Turn the turret towards the player
        Transform turret = npc.GetComponent<AgentFSMTank>().turret;
		
		Quaternion turretRotation = Quaternion.LookRotation(destPos - turret.position);
		turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, Time.deltaTime * curRotSpeed);
        //Shoot bullet towards the player
        tank.ShootBullet();
    }
}
