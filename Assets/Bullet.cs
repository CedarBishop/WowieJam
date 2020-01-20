using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public GameObject parent;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * 10);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != parent)
        {

       
        if (col.gameObject.GetComponent<Player>() != null)
        {
            col.gameObject.GetComponent<Player>().Die();
        }
        else if (col.gameObject.GetComponent<Shooter>() != null)
        {
            col.gameObject.GetComponent<Shooter>().Die();
        }
        else if (col.gameObject.GetComponent<Melee>() != null)
        {
            col.gameObject.GetComponent<Melee>().Die();
        }
        else if (col.gameObject.tag == "Unwalkable")
        {
                Destroy(gameObject);
        }
        }
    }
}
