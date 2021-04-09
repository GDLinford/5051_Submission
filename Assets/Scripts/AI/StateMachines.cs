using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachines : MonoBehaviour
{
    public Stack<State> States { get; set; }

    private void Awake()
    {
        //Empty stack
        States = new Stack<State>();
    }

    private void Update()
    {
        //check if theres an active state if so invoke it
        if (GetCurrentState() != null)
        {
            GetCurrentState().ActiveAction.Invoke();
        }
    }
    public void pushState(Action active, Action onEnter)
    {
        //Used to get the AI into its next state
        State state = new State(active, onEnter);
        States.Push(state);

        GetCurrentState().OnEnterExecute();
    }
    private State GetCurrentState()
    {
        //gets whatever state should currently be active if states.count is more than 0
        return States.Count > 0 ? States.Peek() : null;
    }
}
