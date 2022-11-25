using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class AIStateManager : MonoBehaviour
{
    public float detectingRange = 20.0f;
    public float lostRange = 5.0f;
    
    public float lostTime = 2.0f;
    public float lostsecond = 0.0f;

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
    public bool canSeeThePlayer()
    {
        List<GameObject> colliders;
        bool bfindPlayer = false;

        float targetDetectionRange = detectingRange;

        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);
        if (playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask);
            //check if there are obstacles
            if (hit.collider == null)
            {
                colliders = new List<GameObject>() { playerCollider.gameObject };
                bfindPlayer = true;
            }
            else
            {
                colliders = null;
            }
        }
        else
        {
            colliders = null;
        }
        //Give every targets
        targets = colliders;
        //update current target
        if (colliders != null)
        {
            currentTarget = targets.OrderBy
                (target => Vector2.Distance(target.transform.position, transform.position)).FirstOrDefault();
            //give new update location on current target
            MoveToLocation = currentTarget.transform.position;
        }

        return bfindPlayer;
    }
    public enum AIType
    {
        Seek,
        Pursue,
        Flee,
        Pathfinding
    }
}
