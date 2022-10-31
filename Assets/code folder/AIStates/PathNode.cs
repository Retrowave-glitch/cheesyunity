using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    public int x;
    public int y;

    public int gCost;//Walking Cost from the Start Node
    public int hCost;//Heuristic Cost to reach End Node
    public int fCost;// G + H

    public PathNode cameFromNode;
    public PathNode(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
    public override string ToString()
    {
        return x + "," + y;
    }
    public bool isEqual(PathNode a)
    {
        if(x == a.x&&y == a.y)
        {
            return true;
        }
        return false;
    }
}
