using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Enemy
{
    private Player player;

    public int attackCount = 4;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public void Attack()
    {
        if (attackCount > 0 && gameObject.GetComponentInChildren<Zone>().touchingPlayer == true)
        {
            player.Die();
            LevelManager.instance.RestartLevel();
        }
    }
}
