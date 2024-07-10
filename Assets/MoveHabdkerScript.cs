using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class MoveHabdkerScript : MonoBehaviour
{
    private float Speed = 7f;
  
    private GameObject Player;
    private List<Vector3> vectorList;
    private int current;


    private void Awake()
    {
        Player = GameObject.Find("Pla");
        Debug.Log(Player.ToString());
    }

    public Vector3 GetenermyPos() {  return transform.position; }
    public Vector3 GetplaPos() { return Player.transform.position; }

  

    private void Update()
    {
        MoveHandler();
        PathNode();
        /*
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
            Debug.Log("1");
            PathNode(mousePos);
        }
        */
    }
    private void MoveHandler()
    {
        if (vectorList != null)
        {
            Vector3 targetPosition = vectorList[current];
            if (Vector3.Distance(transform.position, targetPosition) > 0.2f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * Speed * Time.deltaTime;
            }
            else
            {
                current++;
                if (current >= vectorList.Count)
                {
                    vectorList = null;
                }
            }
        }
    }
    private void PathNode()
    {
        current = 0;
        vectorList = PathFinding.instance.FindPath(GetenermyPos(), GetplaPos());
        if (vectorList != null && vectorList.Count > 1)
        {
            vectorList.RemoveAt(0);
        }

    }
}
