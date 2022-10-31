using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FleeState : AIState
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
            fleefromPlayer();
        }
        return this;
    }
    void fleefromPlayer()
    {
        float speed = AIStateManagerpointer.speed;

        //update current target
        AIStateManagerpointer.currentTarget = AIStateManagerpointer.targets.OrderBy
            (target => Vector2.Distance(target.transform.position, AIStateManagerpointer.transform.position)).FirstOrDefault();
        GameObject currentTarget = AIStateManagerpointer.currentTarget;
        Vector2 direction = currentTarget.transform.position - transform.position;
        direction = -direction;

        AIStateManagerpointer.Enemy.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position+direction, speed * Time.deltaTime);
    }
}
