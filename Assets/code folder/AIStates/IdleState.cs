using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public ChaseState chaseState;
    public FleeState fleeState;
    public PursueState pursueState;
    public override AIState RunCurrentState()
    {
        AIState nextState;
        if (canSeeThePlayer())
        {
            switch (AIStateManagerpointer.AItype)
            {
                case AIStateManager.AIType.Seek:
                    nextState = chaseState;
                    break;
                case AIStateManager.AIType.Pursue:
                    nextState = pursueState;
                    break;
                case AIStateManager.AIType.Flee:
                    nextState = fleeState;
                    break;
                default:
                    nextState = chaseState;
                    break;
            }
            return nextState;
        }
        else
        {
            return this;
        }
    }
    public bool canSeeThePlayer()
    {
        List<GameObject> colliders;
        bool bfindPlayer = false;
        float targetDetectionRange = AIStateManagerpointer.detectingRange;
        LayerMask playerLayerMask = AIStateManagerpointer.playerLayerMask;
        LayerMask obstaclesLayerMask = AIStateManagerpointer.obstaclesLayerMask;

        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);
        if (playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask);
            //check if there are obstacles
            if (hit.collider == null) 
            {
                colliders = new List<GameObject>() { playerCollider.gameObject};
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
        AIStateManagerpointer.targets = colliders;
        return bfindPlayer;
    }
}
