using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State CurrentState { get; private set; }

    public void Initialise(State startingState) 
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState) 
    {
        CurrentState.Exit();

        CurrentState = newState;
        newState.Enter();
    }
}
