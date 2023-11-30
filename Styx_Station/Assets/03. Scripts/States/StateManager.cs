public class StateManager
{
    private StateBase currentState; 
    public void ChangeState(StateBase newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixedUpate();
        }
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public StateBase GetCurrentState() { return currentState; }
}
