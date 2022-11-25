using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
public class IdleState : AIState
{
    public ChaseState chaseState;
    public FleeState fleeState;
    public PursueState pursueState;
    public PathfindingState pathfindingState;
    public override AIState RunCurrentState()
    {
        if (AIStateManagerpointer.canSeeThePlayer())
        {
            return chaseState;
        }
        else
        {
            //roaming
            int rnd = Random.Range(0, 100);
            if(rnd == 1) {
                //set roaming position to PathfindingState
                //get next roaming move location
                Vector3Int currentcell;
                Vector3Int newlocationcell;
                int rndx, rndy;
                for(int attempt=0;attempt<5;attempt++)
                {
                    rndx = Random.Range(-3, 4);
                    rndy = Random.Range(-3, 4);
                    if (!(rndx == 0 && rndy == 0)) // not if the random value give 0,0 
                    {
                        currentcell = InGameManager.Instance.ground.LocalToCell(AIStateManagerpointer.Enemy.position);
                        newlocationcell = new Vector3Int(currentcell.x+rndx, currentcell.y+rndy, 0);
                        if (InGameManager.Instance.ground.HasTile(newlocationcell) && !InGameManager.Instance.obstacle.HasTile(newlocationcell))
                        {
                            AIStateManagerpointer.MoveToLocation = InGameManager.Instance.ground.CellToLocal(newlocationcell);
                            return pathfindingState;
                        }
                    }
                }
            }
            return this;
        }
    }
}
