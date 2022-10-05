using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    AIState currentState;
    private void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {
        AIState nextState = currentState?.RunCurrentState();
        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }
    private void SwitchToTheNextState(AIState nextState)
    {
        currentState = nextState;
    }
}
