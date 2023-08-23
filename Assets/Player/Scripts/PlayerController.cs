using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed, forceJ;
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Movement();
    }
    
    void Update()
    {
        Jump();
    }

    void Movement()
    {
        float mX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(mX * speed,rb.velocity.y);
    }

    void Jump()
    {
        //New Input system
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x,0f);
            rb.AddForce(new Vector2(rb.velocity.x,forceJ),ForceMode2D.Impulse);
        }
    }
}
