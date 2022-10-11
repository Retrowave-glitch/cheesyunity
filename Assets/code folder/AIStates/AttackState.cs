using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    public override AIState RunCurrentState()
    {
        Debug.Log("I have attacked!");
        return this;
    }
}
