using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testing : MonoBehaviour
{
    [SerializeField] private Tilemap Tilemap;
    [SerializeField] private TileBase TileBase;
    private TileCreate<Node> TileCreate;

    public int width = 20;
    public int height = 10;

    List<Vector2Int> Vector2Int;
    private void Awake()
    {
        TileCreate = new TileCreate<Node>(width, height, (TileCreate<Node> g, int x, int y) => new Node(g,x,y));
        paintingTile();
    }

    public TileCreate<Node> getGrid()
    {
        return TileCreate;
    }
    public void paintingTile()
    {
        Vector2Int = new List<Vector2Int>();
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Vector2Int.Add(TileCreate.GetTilePos(x, y));
            }
        }

        foreach(Vector2Int vector2Ints in Vector2Int)
        {
            PaintTile(vector2Ints);
        }
    }

    public void PaintTile(Vector2Int Tiles)
    {
        Vector3Int tileVec = Tilemap.WorldToCell((Vector3Int)Tiles);
        Tilemap.SetTile(tileVec, TileBase);
    }
}
