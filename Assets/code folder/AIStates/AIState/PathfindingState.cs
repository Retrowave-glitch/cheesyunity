using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PathfindingState : AIState
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 15;

    public IdleState idleState;
    private Tilemap ground;
    private Tilemap obstacle;
    private bool debug=false;
    public override AIState RunCurrentState()
    {
        //get Tilemap data from InGameManager
        ground = InGameManager.Instance.ground;
        obstacle = InGameManager.Instance.obstacle;

        if (AIStateManagerpointer.vectorPath.Count==0)
        {
            //Find startLocation as cell
            Vector3Int startCellPosition = ground.LocalToCell(AIStateManagerpointer.Enemy.position);
            PathNode startNode = new PathNode(startCellPosition.x, startCellPosition.y);

            if (debug) Debug.Log(startCellPosition.ToString());

            //Find endLocation as cell
            Vector3Int endCellPosition = ground.LocalToCell(AIStateManagerpointer.MoveToLocation);
            PathNode endNode = new PathNode(endCellPosition.x, endCellPosition.y);

            if (debug) Debug.Log(endCellPosition.ToString());

            List<PathNode> path = findingPath(startNode,endNode);
            if(path==null||path.Count==0)
            {
                if(debug)Debug.Log("pathcount 0");
                return idleState;
            }
            else
            {
                foreach(PathNode pathnode in path)
                {
                    if (debug) Debug.Log("count");
                    AIStateManagerpointer.vectorPath.Add(ground.GetCellCenterWorld(pathnode.GetCellLocation()));
                }
                path.Clear();
            }
        }
        else
        {
            if (debug) Debug.Log("found path");
            if (moving()) {
                return this;
            }
            else
            {
                if (debug) Debug.Log("return idleState");
                return idleState;
            }   
        }
        return this;
    }
    private List<PathNode> findingPath(PathNode startNode,PathNode endNode)
    {
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        AIStateManagerpointer.openList = new List<PathNode> { startNode };
        AIStateManagerpointer.closedList = new List<PathNode>();
        while (AIStateManagerpointer.openList.Count > 0)
        {

            PathNode currentNode = GetLowestFCostNode(AIStateManagerpointer.openList);

            if (debug) Debug.Log(currentNode.ToString());

            if(currentNode.isEqual(endNode))
            {
                //Reached final Node
                return CalculatePath(currentNode);
            }


            for(int i =0; i< AIStateManagerpointer.openList.Count; i++)
            {
                if (AIStateManagerpointer.openList[i].GetCellLocation() == currentNode.GetCellLocation())
                {
                    AIStateManagerpointer.openList.RemoveAt(i);
                    break;
                }
            }

            AIStateManagerpointer.closedList.Add(currentNode);

            foreach(PathNode neighbournode in GetNeighbourList(currentNode))
            {
                if (debug) Debug.Log("search neighbour");
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbournode);
                if(tentativeGCost < neighbournode.gCost)
                {
                    neighbournode.cameFromNode = currentNode;
                    neighbournode.gCost = tentativeGCost;
                    neighbournode.hCost = CalculateDistanceCost(neighbournode, endNode);
                    neighbournode.CalculateFCost();
                    if (!isPathNodeInList(neighbournode, AIStateManagerpointer.openList))
                    {
                        if(debug)Debug.Log("add");
                        AIStateManagerpointer.openList.Add(neighbournode);
                    }
                }
            }
        }
        //out of nodes on the openlist   unable to move target position
        return null;
    }
    private bool isPathNodeInList(PathNode node,List<PathNode> list)
    {
        for (int j = 0; j < list.Count; j++)
        {
            if (list[j].x == node.x && list[j].y == node.y)
            {
                return true;
            }
        }
        return false;
    }
    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        int[] NEIGH_X = new int[] { -1, -1, 0, 1, 1, 1, 0, -1 };
        int[] NEIGH_Y = new int[] { 0, 1, 1, 1, 0, -1, -1, -1 }; // 8 ways

        for(int i = 0; i < 8; i++)
        {
            Vector3Int neighLocation = new Vector3Int(currentNode.x + NEIGH_X[i], currentNode.y + NEIGH_Y[i], 0);
            if (ground.HasTile(neighLocation)&&!obstacle.HasTile(neighLocation)) // if the position has ground and no obstacles
            {
                PathNode neighNode = new PathNode(neighLocation.x, neighLocation.y);
                neighNode.gCost = int.MaxValue;
                if(!isPathNodeInList(neighNode, AIStateManagerpointer.closedList)) neighbourList.Add(neighNode);
            }
            else
            {
                if (debug) Debug.Log("Obstacle hit");
                AIStateManagerpointer.closedList.Add(new PathNode(neighLocation.x, neighLocation.y));
            }
        }
        return neighbourList;
    }
    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
            if (debug) Debug.Log("add cameFromNodes");
        }
        path.Reverse();
        if (debug) Debug.Log("calculate path");
        return path;
    }
    private int CalculateDistanceCost(PathNode a, PathNode b)//Finding H cost
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST*Mathf.Min(xDistance,yDistance) +MOVE_DIAGONAL_COST*remaining;
    }
    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];
        for(int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
    bool moving()
    {
        if (AIStateManagerpointer.vectorPath.Count!= 0)
        {
            if (debug) Debug.Log("vectorpath:Count: "+AIStateManagerpointer.vectorPath.Count);
            if (debug) Debug.Log("CurrentPath:Count: "+AIStateManagerpointer.currentPathIndex);
            Vector3 targetPosition = AIStateManagerpointer.vectorPath[AIStateManagerpointer.currentPathIndex];
            if (debug) Debug.Log(targetPosition);
            if(Vector3.Distance(AIStateManagerpointer.Enemy.position,targetPosition)>(ground.cellSize.x/10)) //far to next cell location
            {
                if (debug) Debug.Log("moving");
                float speed = AIStateManagerpointer.speed;
                AIStateManagerpointer.Enemy.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else //near to next cell location
            {
                AIStateManagerpointer.currentPathIndex++;
                if (AIStateManagerpointer.currentPathIndex >= AIStateManagerpointer.vectorPath.Count)
                {
                    //Arrived
                    AIStateManagerpointer.vectorPath.Clear();
                    AIStateManagerpointer.closedList.Clear();
                    AIStateManagerpointer.openList.Clear();
                    AIStateManagerpointer.currentPathIndex = 0;
                    return false;
                }
            }
        }
        return true;
    }
}
