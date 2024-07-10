using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateObj : MonoBehaviour
{
    
    [SerializeField] private GameObject obj;
    [SerializeField] private testing testing;
    private TileCreate<Node> tile;
    private List<Vector2Int> Vector2int;
   
    
    private void Start()
    {
        CreatTree();
    }
    public void GetTileCreate(TileCreate<Node> tile)
    {
        this.tile = tile;
      
    }
    
    public void CreatTree()
    {
        int width = tile.GetWidth();
        int height = tile.GetHeight();
        int tree = Mathf.FloorToInt(width * height / 3);

        
        Vector2int = new List<Vector2Int>();
        for (int x = 0; x < tile.GetWidth(); x++)
        {
            for (int y = 0; y < tile.GetHeight(); y++)
            {
                Vector2int.Add(tile.GetTilePos(x, y));
            }
        }

        for (int i = 0; i < tree && Vector2int.Count > 0;)
        {
            int index = Random.Range(0, Vector2int.Count);
            Vector2Int newPos = Vector2int[index];

            Vector2int.RemoveAt(index);
            i++;
            PathFinding.instance.GetNode(newPos.x, newPos.y).isWalkable = false;
            GameObject treeObj = Instantiate(obj);
            treeObj.transform.position = new Vector3(newPos.x, newPos.y, 0);
            treeObj.AddComponent<BoxCollider2D>();
        }

    }

    
}
