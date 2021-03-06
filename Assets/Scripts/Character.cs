﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Right, Down, Left}


public class Character : MonoBehaviour
{
    public Direction startingDirection;
    protected Direction direction;
    public float speed;
    public LayerMask UnwalkableLayer;
    public ParticleSystem deathParticleFX;
    [HideInInspector]public int stepCount;
    [HideInInspector] public int attackCount;
    Vector2 target;
    bool canMove;
    protected bool isDead;
   Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        SoundManager.instance.Play("Transistion");
        // Set starting Rotation
        canMove = true;
        direction = startingDirection;
        switch (startingDirection)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0,0,0);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            default:
                break;
        }
        stepCount = LevelManager.instance.startingSteps;
        attackCount = LevelManager.instance.startingBullets;
    }


    protected virtual void Update()
    {
        if (canMove == false)
        {
            return;
        }
        // Move Forward
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(CheckIfCanMoveInDirection(Direction.Up))               
                StartCoroutine("MoveTo");
        }
        // Rotate Anti-Clockwise
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.instance.Play("Move");
            transform.Rotate(0,0, 90);
            int directionNum = (int)direction;
            directionNum--;
            if (directionNum == -1)
            {
                directionNum = 3;
            }
            direction = (Direction)directionNum;
        }
        //Rotate Clockwise
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SoundManager.instance.Play("Move");
            transform.Rotate(0,0,-90);
            int directionNum = (int)direction;
            directionNum++;
            if (directionNum == 4)
            {
                directionNum = 0;
            }
            direction = (Direction)directionNum;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();          
        }


    }

    // moves one tile in the characters forward direction
    IEnumerator MoveTo()
    {
        animator.SetBool("IsWalking", true);
        if (stepCount > 0)
        {
            canMove = false;
            target = transform.position + transform.up;
         
            stepCount--;
            GameMenu.instance.UpdateUI();
            target = new Vector2(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y));

            SoundManager.instance.Play("Move");
            while (Vector2.Distance(target, transform.position) > Mathf.Epsilon)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }

            transform.position = target;
            canMove = true;
            TileCheck();
        }
        else
        {
            if (gameObject.GetComponent<Player>() != null) stepCount--;
            yield return null;
        }
        animator.SetBool("IsWalking", false);
       
    }

    IEnumerator PushTo(Direction d)
    {
        
        canMove = false;
        target = new Vector2(transform.position.x,transform.position.y);
        switch (d)
        {
            case Direction.Up:
                target += Vector2.up;
                break;
            case Direction.Down:
                target += Vector2.down;
                break;
            case Direction.Left:
                target += Vector2.left;
                break;
            case Direction.Right:
                target += Vector2.right;
                break;
            default:                    
                break;
        }            
            
        target = new Vector2(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y));

        SoundManager.instance.Play("Move");
        while (Vector2.Distance(target, transform.position) > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        canMove = true;
        TileCheck();       

    }


    // Checks to see if there is a wall in the direction the character is facing if so return false and dont move
    bool CheckIfCanMoveInDirection (Direction d)
    {
        switch (d)
        {
            case Direction.Up:
                return (!Physics2D.Raycast(transform.position,transform.up,1,UnwalkableLayer)); 
                
            case Direction.Right:
                return (!Physics2D.Raycast(transform.position, transform.right, 1, UnwalkableLayer));

            case Direction.Down:
                return (!Physics2D.Raycast(transform.position, -transform.up, 1, UnwalkableLayer));

            case Direction.Left:
                return (!Physics2D.Raycast(transform.position, -transform.right, 1, UnwalkableLayer));

            default:
                return false;
                
        }
    }


    // Dont Delete, is needed to override in derived classes
    protected virtual void Attack ()
    {
        
    }

    // Dying sequence
    public virtual void Die()
    {
        StartCoroutine("DelayDeath");
        ParticleSystem p = Instantiate(deathParticleFX, transform.position,Quaternion.identity);
        p.Play();
        Destroy(p.gameObject,0.2f);
        SoundManager.instance.Play("Hit");
    }

    // Delay so dying animation can be played
    IEnumerator DelayDeath ()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    // Check if there are any other characters on this tile, if so destroy all characters on this tile
    //possible bug areaxd
    void TileCheck ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        if (colliders == null)
        {
            return;
        }

        for (int i = 0; i < colliders.Length; i++)
        {           
            if (colliders[i].GetComponent<PushTile>())
            {
                StartCoroutine(PushTo(colliders[i].GetComponent<PushTile>().direction));
                return;
            }           
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                if (colliders[i].GetComponent<Character>())
                {
                    colliders[i].GetComponent<Character>().Die();
                    Die();
                }
            }
        }

    }
}
