using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PursueState : AIState
{
    public AttackState attackState;
    public bool isInAttackRange;
    public override AIState RunCurrentState()
    {
        if (isInAttackRange)
        {
            return attackState;
        }
        else
        {
            PursuePlayer();
        }
        return this;
    }
    void PursuePlayer()
    {
        float speed = AIStateManagerpointer.speed;

        //update current target
        AIStateManagerpointer.currentTarget = AIStateManagerpointer.targets.OrderBy
            (target => Vector2.Distance(target.transform.position, AIStateManagerpointer.transform.position)).FirstOrDefault();
        GameObject currentTarget = AIStateManagerpointer.currentTarget;
        Vector2 TargetPosition;
        TargetPosition = (Vector2)currentTarget.transform.position + currentTarget.GetComponent<PlayerMovement>().getMovement();

        AIStateManagerpointer.Enemy.position = Vector2.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);
    }
}
