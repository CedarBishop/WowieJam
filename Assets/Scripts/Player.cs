using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int GRID_SIZE = 1;
    private const int MOVE_TIME = 1;
    public float Speed = 2;
    public LayerMask wall;
    private Vector2 target;
    private bool canMove = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) )
        {
            if (!Physics.Raycast(transform.position, Vector2.up, GRID_SIZE))
            {
                target = new Vector2(transform.position.x, transform.position.y + 1);
                canMove = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Physics.Raycast(transform.position, Vector2.down, GRID_SIZE))
            {
                target = new Vector2(transform.position.x, transform.position.y - 1);
                canMove = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!Physics.Raycast(transform.position, Vector2.left, GRID_SIZE))
            {
                target = new Vector2(transform.position.x - 1, transform.position.y);
                canMove = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!Physics.Raycast(transform.position, Vector2.right, GRID_SIZE))
            {
                target = new Vector2(transform.position.x + 1, transform.position.y);
                canMove = true;
            }

        }
        if (canMove) 
        {
            //Vector2.MoveTowards(transform.position, target, Speed);
            transform.position = target;
        }
    }
}