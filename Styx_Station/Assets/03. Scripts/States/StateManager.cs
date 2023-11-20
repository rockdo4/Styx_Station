using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    private StateBase currentState; //현재 상태
    public void ChangeState(StateBase newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        Debug.Log(currentState.ToString());
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
