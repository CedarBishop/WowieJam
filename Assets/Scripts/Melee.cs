using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    private Player player;
    public Vector2 size = new Vector2(3, 3);

    public int attackCount = 4;

    public void Attack()
    {
        if (attackCount > 0)
        {
            attackCount--;
            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, size, 0, Vector2.up);
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
                        hit[i].collider.GetComponent<Character>().Die();
                    }
                }
            }
        }
    }
}
