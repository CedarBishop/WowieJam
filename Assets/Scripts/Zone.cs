using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [HideInInspector] public bool touchingPlayer = false;

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touchingPlayer = true;
            Debug.Log("In");
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            touchingPlayer = false;
            Debug.Log("Out");
        }
    }
}
