using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Right, Down, Left}


public class Character : MonoBehaviour
{
    public Direction startingDirection;
    [HideInInspector]public Direction direction;
    public float speed;
    public LayerMask UnwalkableLayer;
    public ParticleSystem deathParticleFX;
    public int stepCount;
    Vector2 target;
    bool canMove;
    private LevelManager lm;

    void Start()
    {
        lm = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
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
    }


    private void Update()
    {
        if (canMove == false)
        {
            return;
        }
        // Move Forward
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(CheckIfCanMoveInDirection(Direction.Up))
                 StartCoroutine(MoveTo(Direction.Up));
        }
        // Strafe Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CheckIfCanMoveInDirection(Direction.Left))
                StartCoroutine(MoveTo(Direction.Left));
        }
        // Move Backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckIfCanMoveInDirection(Direction.Down))
                StartCoroutine(MoveTo(Direction.Down));
        }
        // Strafe Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckIfCanMoveInDirection(Direction.Right))
                StartCoroutine(MoveTo(Direction.Right));
        }
        // Rotate Anti-Clockwise
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(0,0,-90);
            int directionNum = (int)direction;
            directionNum++;
            if (directionNum == 4)
            {
                directionNum = 0;
            }
            direction = (Direction)directionNum;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
                Attack();          
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            lm.RestartLevel();
        }

    }

    // moves one tile in the direction of travel
    IEnumerator MoveTo(Direction d)
    {
        if (stepCount > 0)
        {
            canMove = false;
            switch (d)
            {
                case Direction.Up:
                    target = transform.position + transform.up;
                    break;
                case Direction.Down:
                    target = transform.position + -transform.up;
                    break;
                case Direction.Left:
                    target = transform.position + -transform.right;
                    break;
                case Direction.Right:
                    target = transform.position + transform.right;
                    break;
                default:
                    target = transform.position;
                    break;
            }

            stepCount--;
            GameMenu.instance.UpdateUI();
            target = new Vector2(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y));

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
            yield return null;
        }
       
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


    // Raycasts in the direction the character is facing, cycles though the array of what was raycasted, if there were any characters other than this instance, destroy them
    void Attack ()
    {
        GameMenu.instance.UpdateUI();

        if (gameObject.GetComponent<Shooter>() != null) gameObject.GetComponent<Shooter>().Attack();

        if (gameObject.GetComponent<Player>() != null) gameObject.GetComponent<Player>().Attack();

        //if (gameObject.GetComponent<Melee>() != null) gameObject.GetComponent<Melee>().Attack();
    }

    // Dying sequence
    public virtual void Die()
    {
        StartCoroutine("DelayDeath");
        ParticleSystem p = Instantiate(deathParticleFX, transform.position,Quaternion.identity);
        p.Play();
        Destroy(p.gameObject,0.2f);
    }

    // Delay so dying animation can be played
    IEnumerator DelayDeath ()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    // Check if there are any other characters on this tile, if so destroy all characters on this tile
    void TileCheck ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.25f);
        if (colliders == null)
        {
            return;
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
