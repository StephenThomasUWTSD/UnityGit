using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class AgentFSMTank : AgentFSM {


    public GameObject Bullet;
    private int health;

    //Tank Turret
    public Transform turret { get; set; }
    public Transform bulletSpawnPoint { get; set; }

    //Bullet shooting rate
    float shootRate;


    // Implement CurrentStateID, so you can use enum values
    public new TankFSMStateID CurrentStateID
	{
		get{return (TankFSMStateID)base.CurrentStateID;}
		set{base.CurrentStateID = (int)value;}
	}
    
    
    // Use this for initialization
    protected override void Initialize()
    {
        health = 100;

        elapsedTime = 0.0f;
        shootRate = 2.0f;

        // Get the target enemy(Player) and initialise playerTransform
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
		playerTransform = objPlayer.transform;

        if (!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");


        // Get the turret of the tank
        turret = gameObject.transform.GetChild(0).transform;
		bulletSpawnPoint = turret.GetChild(0).transform;

        //Start Doing the Finite State Machine
        ConstructFSM();
    }

    // Update is called once per frame
    protected override void FSMUpdate()
    {
        //Check for elapsed time
        elapsedTime += Time.deltaTime;
    }

    protected override void FSMFixedUpdate()
    {
        // Make the FSM reason and act accordingly
		CurrentState.Reason(playerTransform, transform);
		CurrentState.Act(playerTransform, transform);
        
    }

    /// <summary>
    /// This method tries to change the state the FSM is in based on
    /// the current state and the transition passed. If current state
    ///  doesn't have a target state for the transition passed, 
    /// an ERROR message is printed.
    /// </summary>
    public void PerformTransition(TankFSMTransition transition)
    {
        // Perform the required transition
        PerformTransition(transition.ToString());
    }


    private void ConstructFSM()
    {
        // Find all the WanderPoint game objects
		waypointList = GameObject.FindGameObjectsWithTag("WanderPoint");

        // Loop through the objects and get their transform and save in new array 
        Transform[] waypoints = new Transform[waypointList.Length];
        int i = 0;
        foreach (GameObject go in waypointList)
		{
			waypoints[i]=go.transform;
            i++;
		}

        // Create the different states and add to the collections.
        PatrolState patrol = new PatrolState(waypoints);
		patrol.AddTransition(TankFSMTransition.SawPlayer, TankFSMStateID.Chasing);
		patrol.AddTransition(TankFSMTransition.NoHealth, TankFSMStateID.Dead);
		
        ChaseState chase = new ChaseState(waypoints);
		chase.AddTransition(TankFSMTransition.LostPlayer, TankFSMStateID.Patrolling);
		chase.AddTransition(TankFSMTransition.ReachPlayer, TankFSMStateID.Attacking);
		chase.AddTransition(TankFSMTransition.NoHealth, TankFSMStateID.Dead);
		

        AttackState attack = new AttackState(waypoints);
		attack.AddTransition(TankFSMTransition.LostPlayer, TankFSMStateID.Patrolling);
		attack.AddTransition(TankFSMTransition.ReachPlayer, TankFSMStateID.Attacking);
		attack.AddTransition(TankFSMTransition.NoHealth, TankFSMStateID.Dead);

        DeadState dead = new DeadState();
		dead.AddTransition(TankFSMTransition.NoHealth, TankFSMStateID.Dead);
        // add the states
        AddState(patrol);
		AddState(chase);
		AddState(attack);
		AddState(dead);

    }

    /// <summary>
    /// Check the collision with the bullet
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        // Reduce health if hit by bullet
        if (collision.gameObject.tag == "Bullet")
		{
			health -= 50;
			if (health <= 0)
			{
				Debug.Log("Switch to dead state.");
				PerformTransition(TankFSMTransition.NoHealth);
				Explode();
			}
		}
    }

    protected void Explode()
    {
        float rndX = Random.Range(10.0f, 30.0f);
        float rndZ = Random.Range(10.0f, 30.0f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }

        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// Shoot the bullet from the turret
    /// </summary>
    public void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
    }
}


