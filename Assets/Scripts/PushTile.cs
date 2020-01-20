using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTile : MonoBehaviour
{
    public Direction direction;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        switch (direction)
        {
            case Direction.Up:
                transform.rotation = Quaternion.Euler(0, 0, -180);
                break;
            case Direction.Down:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.Left:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.Right:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;

        }
    }
}
