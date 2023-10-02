using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isJumping;
    public int nJumping;
    public bool isGround;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            nJumping = 1;
            isGround = true;
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isGround = false;
        }
    }
}
