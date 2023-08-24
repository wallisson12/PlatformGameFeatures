using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed, forceJ,jumpHeight;
    [SerializeField] private GroundCheck gc;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityScale;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //New Input system
        Movement();
    }
    
    void Update()
    {
        //New Input system
        Jump();
    }

    void Movement()
    {
        float mX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(mX * speed,rb.velocity.y);
    }

    void Jump()
    {
        //One jump or Double jump
        if (Input.GetKeyDown(KeyCode.Space) && gc.nJumping > 0)
        {
            gc.nJumping--;
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x,0f);
            //Varies the force of the jump taking into the account height,mass,gravity of the world and the player
            forceJ = Mathf.Sqrt(jumpHeight *(Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(new Vector2(rb.velocity.x,forceJ),ForceMode2D.Impulse);
            
        }

        //More gravity on the jump fall
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;
        }
        else
        {
            rb.gravityScale = fallGravityScale;
        }
    }
}
