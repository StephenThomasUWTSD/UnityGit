using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AgentFSM : MonoBehaviour {

    /// <summary>
    ///  The player transform
    /// </summary>
    protected Transform playerTransform;

    
    /// <summary>
    /// List of waypoints
    /// </summary>
    protected GameObject[] waypointList;

    protected float elapsedTime;
    
    /// <summary>
    /// The current behaviour state id of the object
    /// </summary>
    protected virtual int CurrentStateID { get; set; }

    /// <summary>
    /// The current state of the object
    /// </summary>
    public FSMState CurrentState { get; protected set; }

    private List<FSMState> states;

    // State machines must implement these three methods

    /// <summary>
    /// 
    /// </summary>
    protected abstract void Initialize();

    protected abstract void FSMUpdate();

    protected abstract void FSMFixedUpdate();

    public AgentFSM()
    {
        states = new List<FSMState>();
    }


    // Use this for initialization
    void Start () {
        Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        FSMUpdate();
	}

    void FixedUpdate()
    {
        FSMFixedUpdate();
    }

    /// <summary>
    /// Add New State into the list
    /// </summary>
    public void AddState(FSMState fsmState)
    {
        // Check for Null reference before deleting
        if (fsmState == null)
        {
            Debug.LogError("FSM ERROR: Null reference is not allowed");
            return;
        }

        // First State inserted is also the Initial state
        //   the state the machine is in when the simulation begins
        if (states.Count == 0)
        {
            states.Add(fsmState);
            CurrentState = fsmState;
            CurrentStateID = fsmState.ID;
            return;
        }

        // Add the state to the List if it´s not inside it
        foreach (FSMState state in states)
        {
            if (state.ID == fsmState.ID)
            {
                Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list");
                return;
            }
        }

        //If no state in the current then add the state to the list
        states.Add(fsmState);
    }

    /// <summary>
    /// This method delete a state from the FSM List if it exists, 
    ///   or prints an ERROR message if the state was not on the List.
    /// </summary>
    public void DeleteState(int fsmState)
    {
        // Check for NullState before deleting
        if (fsmState == 0)
        {
            Debug.LogError("FSM ERROR: null id is not allowed");
            return;
        }

        // Search the List and delete the state if it´s inside it
        foreach (FSMState state in states)
        {
            if (state.ID == fsmState)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
    }

    /// <summary>
    /// This method tries to change the state the FSM is in based on
    /// the current state and the transition passed. If current state
    ///  doesn´t have a target state for the transition passed, 
    /// an ERROR message is printed.
    /// </summary>
    protected virtual void PerformTransition(string fsmTransition)
    {
        // Check for NullTransition before changing the current state
        if (fsmTransition == FSMTransition.None.ToString())
        {
            Debug.LogError("FSM ERROR: Null transition is not allowed");
            return;
        }

        // Check if the currentState has the transition passed as argument
        int id = CurrentState.GetOutputState(fsmTransition);
        if (id == 0)
        {
            Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
            return;
        }

        // Update the currentStateID and currentState		
        CurrentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == CurrentStateID)
            {
                CurrentState = state;
                break;
            }
        }
    }

}
