using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    public AIStateManager AIStateManagerpointer;
    public abstract AIState RunCurrentState();
}
