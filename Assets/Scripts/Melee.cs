using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    private Player player;
    public Vector2 size = new Vector2(3, 3);
    public GameObject attackAnim;


    protected override void Attack()
    {
        if (attackCount > 0)
        {
            attackCount--;
            SoundManager.instance.Play("Shoot");
            GameObject go = Instantiate(attackAnim, transform.position, Quaternion.identity);            
            Destroy(go,0.5f);
            RaycastHit2D[] hit = Physics2D.BoxCastAll(transform.position, size, 0, Vector2.up);
            if (hit == null)
            {
                return;
            }
            SoundManager.instance.Play("Hit");
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
