using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AIStateManager : MonoBehaviour
{
    public float detectingRange = 100000.0f;
    public float lostRange = 5.0f;
    public float lostTime = 2.0f;
    public float speed = 2.0f;
    public Transform Enemy;
    public IdleState idleState;
    public AIType AItype = AIType.Seek;
    public Vector3 MoveToLocation;

    public List<GameObject> targets = null;
    public Collider2D[] obstacles = null;
    public GameObject currentTarget;

    public List<PathNode> openList;
    public List<PathNode> closedList;
    public List<Vector3> vectorPath;

    public int currentPathIndex = 0;
    public int GetTargetsCount() => targets == null ? 0 : targets.Count;

    [SerializeField]
    public LayerMask obstaclesLayerMask, playerLayerMask;
    AIState currentState;

    private void Awake()
    {

        SwitchToTheNextState(idleState); //default to idle
        currentState.AIStateManagerpointer = this;
        
    }
    private void Update()
    {
        RunStateMachine();
    }
    private void RunStateMachine()
    {
        AIState nextState = currentState?.RunCurrentState(); //if the currentState is not null
        if (nextState != null)
        {
            SwitchToTheNextState(nextState);
        }
    }
    private void SwitchToTheNextState(AIState nextState)
    {
        currentState = nextState;
        currentState.AIStateManagerpointer = this;
    }
    public enum AIType
    {
        Seek,
        Pursue,
        Flee,
        Pathfinding
    }
}
