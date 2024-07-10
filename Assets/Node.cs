using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x;
    public int y;

    public bool isWalkable = true;

    public int gCost;
    public int hCost;
    public int fCost;

    public Node ParentNode;

    private TileCreate<Node> Tiles;
    public Node(TileCreate<Node> Tiles, int x, int y)
    {
        this.x = x;
        this.y = y;
        this.Tiles = Tiles;
    }
    public int GetFCost()
    {
        return fCost = gCost + hCost;
    }
}
