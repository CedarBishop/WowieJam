﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject shootSprite;
    public Vector2 dTemp;

    protected override void Start()
    {
        base.Start();
        isDead = false;
        LevelManager.instance.playerIsDead = false;
        GameMenu.instance.UpdateUI();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.R))
        {
            LevelManager.instance.RestartLevel();
        }
    }

    public override void Die()
    {
        if (isDead == false)
        {
            isDead = true;
            base.Die();
            LevelManager.instance.playerIsDead = true;
            LevelManager.instance.RestartLevel();
        }        
    }

    protected override void Attack()
    {
        if (attackCount > 0)
        {
            if(attackCount > 0) attackCount--;
            SoundManager.instance.Play("Shoot");
            GameMenu.instance.UpdateUI();
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
            GameObject go = Instantiate(shootSprite, gameObject.transform.position, Quaternion.identity);
            go.GetComponent<Bullet>().parent = gameObject;
            dTemp = d;
            go.GetComponent<Bullet>().direction = dTemp;
            /*
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, d, 10);


            if (hit == null)
            {
                return;
            }
            
            for (int i = 0; i < hit.Length; i++)
            {
                //cycles though the array of what was raycasted, if there were any characters other than this instance, destroy them
                if (hit[i].collider.gameObject != gameObject)
                {
                    if (hit[i].collider.GetComponent<Character>())
                    {
                        hit[i].collider.GetComponent<Character>().Die();
                    }
                }

            }*/
        }
    }
}