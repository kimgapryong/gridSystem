using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileCreate<T>
{
    private int width;
    private int height;
    private T[,] tiles;
     
    public TileCreate(int width, int height, Func<TileCreate<T>, int, int, T>TTile)
    {
        this.width = width;
        this.height = height;
        tiles = new T[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                tiles[x,y] = TTile(this, x, y);
            }
        }
    }
    
    public T GetTile(int x, int y)
    {
        return tiles[x, y];
    }
    public Vector2Int GetTilePos(int x, int y)
    {
        return new Vector2Int(x, y);
    }
    public int GetWidth() {  return width; }
    public int GetHeight() { return height; }
    
    public void GetXY(Vector3 Position, out int x, out int y)
    {
        x = Mathf.FloorToInt(Position.x);
        y = Mathf.FloorToInt(Position.y);
    }
}
