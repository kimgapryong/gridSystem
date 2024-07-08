using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PathFinding
{
    private const int STRAICHT_COST = 10;
    private const int DIAGONAL_COST = 14;

  
    private TileCreate<Node> grid;
    private List<Node> openList;
    private List<Node> closeList;


    public PathFinding(TileCreate<Node> grid)
    {
        if (grid == null)
        {
            grid = this.grid;
        }
        else
        {
            Debug.Log("Á¿±î");
        }
        
        
    }
    
    public List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        grid.GetXY(start, out int startX, out int startY);
        grid.GetXY(end, out int endX , out int endY);

        List<Node> nodes = FindPath(startX, startY, endX, endY);

        if (nodes == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorList = new List<Vector3>();
            foreach (Node node in nodes)
            {
                vectorList.Add(new Vector3(node.x, node.y) + Vector3.one * .5f);
            }
            return vectorList;
        }
    }
    private List<Node> FindPath(int startX, int startY , int endX, int endY)
    {
        
        Node startNode = grid.GetTile(startX, startY);
        Node endNode = grid.GetTile(endX, endY);

        openList = new List<Node>() { startNode };
        closeList = new List<Node>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int y = 0; y < grid.GetHeight(); y++)
            {
                Node node = grid.GetTile(x, y);
                node.gCost = int.MaxValue;
                node.fCost = node.GetFCost();
                node.ParentNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = GetHcost(startNode, endNode);
        startNode.fCost = startNode.GetFCost();

        while(openList.Count > 0)
        {
            Node currentNode = GetLowNode(openList);
            if(currentNode == endNode)
            {
                return CalucNode(currentNode);
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);

            foreach (Node node in NaverNode(currentNode))
            {
                if(closeList.Contains(node)) continue;

                int realGcost = node.gCost + GetHcost(currentNode, node);
                if(realGcost < node.gCost)
                {
                    node.gCost = realGcost;
                    node.hCost = GetHcost(node, endNode);
                    node.fCost = node.GetFCost();
                    node.ParentNode = currentNode;

                    if (!openList.Contains(node))
                    {
                        openList.Add(node);
                    }
                }
            }
        }
        return null;
    }

    private List<Node> CalucNode(Node currentNode)
    {
        List<Node> nodes = new List<Node>() {currentNode};
        Node endNode = currentNode;
        while(currentNode.ParentNode != null)
        {
            currentNode = currentNode.ParentNode;
            nodes.Add(currentNode);
        }
        nodes.Reverse();
        return nodes;
    }

    private List<Node> NaverNode(Node currentNode)
    {
        List<Node> nodes = new List<Node>();
        if(currentNode.x - 1 >= 0)
        {
            nodes.Add(GetNode(currentNode.x - 1, currentNode.y));
            if(currentNode.y + 1 < grid.GetWidth()) { nodes.Add(GetNode(currentNode.x - 1, currentNode.y + 1)); }
            if(currentNode.y - 1 >= 0) { nodes.Add(GetNode(currentNode.x - 1, currentNode.y - 1)); }
        }
        if (currentNode.x + 1 < grid.GetWidth())
        {

            nodes.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y - 1 >= 0) nodes.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.GetHeight()) nodes.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
    
        if (currentNode.y - 1 >= 0) nodes.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < grid.GetHeight()) nodes.Add(GetNode(currentNode.x, currentNode.y + 1));

        return nodes;
    }

    public Node GetNode(int x, int y)
    {
        return grid.GetTile(x,y);
    }
    private int GetHcost(Node a, Node b)
    {
        if(a == null || b == null)
        {
            return 0;
        }
        else
        {
            int dirX = Mathf.Abs(a.x - b.x);
            int dirY = Mathf.Abs(a.y - b.y);
            int remaning = Mathf.Abs(dirX - dirY);
            return Mathf.Min(dirX, dirY) * DIAGONAL_COST + remaning * STRAICHT_COST;
        }
        
    }

    private Node GetLowNode(List<Node> openList)
    {
        Node current = openList[0];
        for(int i = 1; i < openList.Count; i++)
        {
            if (openList[i].fCost < current.fCost)
            {
                current = openList[i];
            }
        }
        return current;
    }
}
