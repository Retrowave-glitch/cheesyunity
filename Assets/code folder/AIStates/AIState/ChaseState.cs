using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ChaseState : AIState
{
    public AttackState attackState;
    public IdleState idlestate;
    public bool isInAttackRange;
    public override AIState RunCurrentState()
    {
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            //Enemy lost player function
            if (!AIStateManagerpointer.canSeeThePlayer()) {
                AIStateManagerpointer.lostsecond += Time.deltaTime;
                if (AIStateManagerpointer.lostsecond > AIStateManagerpointer.lostTime)
                {
                    return idlestate;
                }
                else
                {
                    MovePlayerLastLocation();
                }
            }
            else
            {
                ChasePlayer();
                AIStateManagerpointer.lostsecond = 0.0f;
            }
        }

        return this;
    }
    void MovePlayerLastLocation()
    {
        float speed = AIStateManagerpointer.speed;
        AIStateManagerpointer.Enemy.position = Vector2.MoveTowards(transform.position, AIStateManagerpointer.MoveToLocation, speed * Time.deltaTime);
    }
    void ChasePlayer()
    {
        float speed = AIStateManagerpointer.speed;

        //update current target
        AIStateManagerpointer.currentTarget = AIStateManagerpointer.targets.OrderBy
            (target => Vector2.Distance(target.transform.position, AIStateManagerpointer.transform.position)).FirstOrDefault();

        GameObject currentTarget = AIStateManagerpointer.currentTarget;

        //Chase Behavior
        AIStateManagerpointer.Enemy.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
        //
    }
}
