using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    public Rigidbody2D rb;
    public GroundCheck gc;
    [SerializeField] private float speed, forceJ, jumpHeight;
    [SerializeField] private float gravityScale;
    [SerializeField] private float fallGravityScale;

    [Header("Direcao")]
    public int direcao;

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
        Flip();
    }

    void Movement()
    {
        float mX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(mX * speed,rb.velocity.y);
    }

    void Jump()
    { 
        //One jump or Double jump
        if (Input.GetKeyDown(KeyCode.Space) && gc.nJumping > 0 && WallJump.isSliding == false)
        {
            gc.nJumping--;
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x,0f);
            //Varies the force of the jump taking into the account height,mass,gravity of the world and the player
            forceJ = Mathf.Sqrt(jumpHeight *(Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(new Vector2(rb.velocity.x,forceJ),ForceMode2D.Impulse);
        }
        

        if (WallJump.onWall)
        {
            gc.nJumping = 2;
        }

        //More gravity on the jump fall and WallJump
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = gravityScale;
        }
        else 
        {
            rb.gravityScale = fallGravityScale;
        }
    }

    void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            direcao = 1;
            Vector3 scale = transform.localScale;
            scale.x = direcao;
            transform.localScale = scale;
        }
        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            direcao = -1;
            Vector3 scale = transform.localScale;
            scale.x = direcao;
            transform.localScale = scale;
        }
    }
}
