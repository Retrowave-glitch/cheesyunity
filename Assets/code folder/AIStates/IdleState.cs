using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public ChaseState chaseState;

    public override AIState RunCurrentState()
    {
        if (canSeeThePlayer())
        {
            return chaseState;
        }
        else
        {
            return this;
        }
    }
    public bool canSeeThePlayer()
    {
        List<Transform> colliders;
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
                Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                colliders = new List<Transform>() { playerCollider.transform };
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
