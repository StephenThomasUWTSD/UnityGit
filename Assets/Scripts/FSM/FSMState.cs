using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// This class is adapted and modified from the FSM implementation class available on UnifyCommunity website
/// The license for the code is Creative Commons Attribution Share Alike.
/// It's originally the port of C++ FSM implementation mentioned in Chapter01 of Game Programming Gems 1
/// You're free to use, modify and distribute the code in any projects including commercial ones.
/// Please read the link to know more about CCA license @http://creativecommons.org/licenses/by-sa/3.0/

/// This class represents the States in the Finite State System.
/// Each state has a Dictionary with pairs (transition-state) showing
/// which state the FSM should be if a transition is fired while this state
/// is the current state.
/// Reason method is used to determine which transition should be fired .
/// Act method has the code to perform the actions the NPC is supposed to do if it´s on this state.
/// </summary>
/// 
public abstract class FSMState  {

    protected Dictionary<string, int> transitions { get; set; }

    public virtual int ID { get; protected set; }

    protected Vector3 destPos;
    protected Transform[] waypoints;
    protected float curRotSpeed;
    protected float curSpeed;

    private int numWaypoints;

    protected string[] stateNames;

    public FSMState()
    {
        transitions = new Dictionary<string, int>();
    }

    void Start()
    {
    }

    protected virtual void AddTransition(string transition, int stateID)
    {
        if (IsTransitionEmpty(transition) || stateID <= 0)
        {
            Debug.LogWarning("FSMState : Null transition not allowed");
            return;
        }

        if (transitions.ContainsKey(transition))
        {
            Debug.LogWarning("FSMState ERROR: transition is already inside the map");
            return;
        }

        transitions.Add(transition, stateID);
        if (stateNames != null && stateID <= stateNames.Length)
        {
            Debug.Log("Added : " + transition + " transitions to state : " + stateNames[stateID]);
        }
        else
        {
            {

                Debug.Log("Added : " + transition + " transitions to state ID : " + stateID);
            }
        }

        
    }

    protected void DeleteTransition(string transition)
    {
        // Check for none
        if (IsTransitionEmpty(transition))
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return;
        }

        if (transitions.ContainsKey(transition))
        {
            transitions.Remove(transition);
        } else
        {
            Debug.LogError("FSMState ERROR: Transition passed was not on this State's List");
        }
    }

    /// <summary>
    /// This method returns the new state the FSM should transition to if this state receives a transition condition
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stateID"></param>
    /// <returns></returns>
    public virtual int GetOutputState(string transition)
    {
        // Check for NullTransition
        if (IsTransitionEmpty(transition))
        {
            Debug.LogError("FSMState ERROR: NullTransition is not allowed");
            return 0;
        }

        // Check if the map has this transition
        if (transitions.ContainsKey(transition))
        {
            return transitions[transition];
        } else
        {
            Debug.LogError("FSMState ERROR: " + transition + " Transition passed to the State was not on the list");
            return 0;
        }
    }

    /// <summary>
    /// Find next semi-random patrol point
    /// </summary>
    public void FindNextPoint()
    {
        int rndIndex = Random.Range(0, waypoints.Length);
        Vector3 rndPosition = Vector3.zero;
        destPos = waypoints[rndIndex].position + rndPosition;
    }


    /// <summary>
    /// Check whether the next random position is the same as current tank position
    /// </summary>
    /// <param name="pos">position to check</param>
    protected bool IsInCurrentRange(Transform trans, Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - trans.position.x);
        float zPos = Mathf.Abs(pos.z - trans.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }


    private bool IsTransitionEmpty(string value)
    {
        switch(value.Trim())
        {
            case "":
            case null:
            case "0":
            case "None":
                return true;
            default:
                return false;
        }
    }


    /// <summary>
    /// Decides if the state should transition to another on its list
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npcAgent"></param>
    public abstract void Reason(Transform player, Transform npcAgent);

    /// <summary>
    /// This method controls the behavior of the NPC AI in the game World.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="npcAgent"></param>
    public abstract void Act(Transform player, Transform npcAgent);

}


public enum FSMTransition
{
    None = 0
}

public enum FSMStateID
{
    None = 0
}