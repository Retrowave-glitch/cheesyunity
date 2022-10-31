using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PathfindingState : AIState
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public IdleState idleState;
    private Tilemap ground;
    private Tilemap obstacle;
    private Vector2 nextLocation;

    private List<PathNode> openList;
    private List<PathNode> closedList;

    private bool foundPath=false;
    public override AIState RunCurrentState()
    {
        if (!foundPath)
        {
            //Find startLocation as cell
            Vector3Int startCellPosition = ground.LocalToCell(transform.position);
            PathNode startNode = new PathNode(startCellPosition.x, startCellPosition.y);

            //Find endLocation as cell
            Vector3Int endCellPosition = ground.LocalToCell(AIStateManagerpointer.MoveToLocation);
            PathNode endNode = new PathNode(endCellPosition.x, endCellPosition.y);

            List<PathNode> path = findingPath(startNode,endNode);
            foundPath = true;
        }
        else
        {
            return idleState;
        }
        return this;
    }
    private List<PathNode> findingPath(PathNode startNode,PathNode endNode)
    {
        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode.isEqual(endNode))
            {
                //Reached final Node
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighbournode in GetNeighbourList(currentNode))
            {
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbournode);
                if(tentativeGCost < neighbournode.gCost)
                {
                    neighbournode.cameFromNode = currentNode;
                    neighbournode.gCost = tentativeGCost;
                    neighbournode.hCost = CalculateDistanceCost(neighbournode, endNode);
                    neighbournode.CalculateFCost();

                    if (!isPathNodeInList(neighbournode, openList))
                    {
                        openList.Add(neighbournode);
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

                if(!isPathNodeInList(neighNode,closedList)) neighbourList.Add(new PathNode(neighLocation.x, neighLocation.y));
            }
            else
            {
                closedList.Add(new PathNode(neighLocation.x, neighLocation.y));
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
        }
        return null;
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
    void moving()
    {
        float speed = AIStateManagerpointer.speed;
        AIStateManagerpointer.Enemy.position = Vector2.MoveTowards(transform.position, nextLocation, speed * Time.deltaTime);
    }
}
