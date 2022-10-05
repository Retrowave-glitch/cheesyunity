using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIState
{
    public override AIState RunCurrentState()
    {
        return this;
    }
}
