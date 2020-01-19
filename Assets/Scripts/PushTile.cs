using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushTile : MonoBehaviour
{
    public Direction direction;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        switch (direction)
        {
            case Direction.Up:
                sr.sprite = spriteUp;
                break;
            case Direction.Down:
                sr.sprite = spriteDown;
                break;
            case Direction.Left:
                sr.sprite = spriteLeft;
                break;
            case Direction.Right:
                sr.sprite = spriteRight;
                break;

        }
    }
}
