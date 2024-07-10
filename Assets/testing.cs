using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class testing : MonoBehaviour
{
    [SerializeField] private Tilemap Tilemap;
    [SerializeField] private TileBase Tilebase, TileWall;
    [SerializeField] private CreateObj CreateObj;
    private TileCreate<Node> TileCreate;

    public int width = 20;
    public int height = 10;

    List<Vector2Int> Vector2Int;
    private void Awake()
    {
     
        PathFinding path = new PathFinding(width, height);
        TileCreate = path.GetGrid();
        paintingTile();
        PaintWall(Vector2Int);
        CreateObj.GetTileCreate(TileCreate);
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
            PaintTile(vector2Ints, Tilebase);
        }
    }

    public void PaintTile(Vector2Int Tiles, TileBase tilebase)
    {
        Vector3Int tileVec = Tilemap.WorldToCell((Vector3Int)Tiles);
        Tilemap.SetTile(tileVec, tilebase);
    }

    public void PaintWall(List<Vector2Int> Tiles)
    {
        List<Vector2Int> RandomVec = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };
        HashSet<Vector2Int> hashList = new HashSet<Vector2Int>();
        foreach(Vector2Int v in Tiles)
        {
            foreach(Vector2Int v2 in RandomVec)
            {
                Vector2Int tile = v + v2;
                if(Tiles.Contains(tile) == false)
                {
                    hashList.Add(tile);
                }
            }
        }
        
        
        foreach(Vector2Int v in hashList)
        {
             
            PaintTile(v, TileWall);
            
        }
        foreach(Vector3Int v in hashList)
        {
            TileBase tile = Tilemap.GetTile(v);
            if (tile != null)
            {
                Vector3 tileWorldPos = Tilemap.GetCellCenterWorld(v);
                GameObject colliderObject = new GameObject("ColliderObject");
                colliderObject.transform.position = tileWorldPos;
                colliderObject.AddComponent<BoxCollider2D>();
                
            }
        }
        
    }
}
