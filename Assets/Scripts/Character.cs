using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Right, Down, Left}
public enum LocalDirection { Forward, Right, Back, Left}


public class Character : MonoBehaviour
{
    public Direction startingDirection;
    [SerializeField] private Direction direction;
    public float speed;
    public LayerMask UnwalkableLayer;
    Vector2 target;
    bool canMove;

    void Start()
    {

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
            if(CheckIfCanMoveInDirection(LocalDirection.Forward))
                 StartCoroutine(MoveTo(LocalDirection.Forward));
        }
        // Strafe Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CheckIfCanMoveInDirection(LocalDirection.Left))
                StartCoroutine(MoveTo(LocalDirection.Left));
        }
        // Move Backward
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (CheckIfCanMoveInDirection(LocalDirection.Back))
                StartCoroutine(MoveTo(LocalDirection.Back));
        }
        // Strafe Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (CheckIfCanMoveInDirection(LocalDirection.Right))
                StartCoroutine(MoveTo(LocalDirection.Right));
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
            Shoot();
        }
    }


    IEnumerator MoveTo(LocalDirection localDirection)
    {
        canMove = false;
        switch (localDirection)
        {
            case LocalDirection.Forward:
                target = transform.position + transform.up;
                break;
            case LocalDirection.Back:
                target = transform.position + -transform.up;
                break;
            case LocalDirection.Left:
                target = transform.position + -transform.right;
                break;
            case LocalDirection.Right:
                target = transform.position + transform.right;
                break;
            default:
                target = transform.position;
                break;
        }

        target = new Vector2(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y));

        while (Vector2.Distance(target, transform.position) > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        canMove = true;
    }

    bool CheckIfCanMoveInDirection (LocalDirection localDirection)
    {
        switch (localDirection)
        {
            case LocalDirection.Forward:
                return (!Physics2D.Raycast(transform.position,transform.up,1,UnwalkableLayer));
                
            case LocalDirection.Right:
                return (!Physics2D.Raycast(transform.position, transform.right, 1, UnwalkableLayer));

            case LocalDirection.Back:
                return (!Physics2D.Raycast(transform.position, -transform.up, 1, UnwalkableLayer));

            case LocalDirection.Left:
                return (!Physics2D.Raycast(transform.position, -transform.right, 1, UnwalkableLayer));

            default:
                return false;
                
        }
        return false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Vector2 d = Vector2.up;
    //    switch (direction)
    //    {
    //        case Direction.Up:
    //            d = Vector2.up;
    //            break;
    //        case Direction.Right:
    //            d = Vector2.right;
    //            break;
    //        case Direction.Down:
    //            d = Vector2.down;
    //            break;
    //        case Direction.Left:
    //            d = Vector2.left;
    //            break;
    //        default:
    //            break;
    //    }
    //    Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + (d * 10));
    //}

    void Shoot ()
    {        
        Vector2 d = Vector2.up;
        switch (direction)
        {
            case Direction.Up:
                d = Vector2.up;
                break;
            case Direction.Right:
                d = Vector2.right;
                break;
            case Direction.Down:
                d = Vector2.down;
                break;
            case Direction.Left:
                d = Vector2.left;
                break;
            default:
                break;
        }
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position,d,10);
        //Debug.DrawRay(transform.position,d,Color.green,10);
        if (hit == null)
        {
            return;
        }
        for (int i = 0; i < hit.Length; i++)
        {
            
            if (hit[i].collider.gameObject != gameObject)
            {
                if (hit[i].collider.GetComponent<Character>())
                {
                    print("sgewrggs");
                    hit[i].collider.GetComponent<Character>().Die();
                }
            }
           
        }
    }

    public void Die()
    {
        StartCoroutine("DelayDeath");
        print("Triggered Die Function");
    }

    IEnumerator DelayDeath ()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
