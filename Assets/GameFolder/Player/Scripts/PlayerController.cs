using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Attributes")]
    public Rigidbody2D rb;
    public GroundCheck gc;
    [SerializeField] private GameObject skin;
    [SerializeField] private float speed, forceJ, jumpHeight;
    public float gravityScale;
    [SerializeField] private float fallGravityScale;


    [Header("Animations")]
    public Animator anim;

    [Header("Direcao")]
    public float direcao;

    [Header("Ladders")]
    [SerializeField] private LayerMask layerLadders;
    [SerializeField] private float distanceRayLa;
    [SerializeField] private bool isClimbing;
    [SerializeField] private float speedY;

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
        Ladders();
    }

    void Movement()
    {
        if (Dash._isDashing == false)
        {
            float mX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(mX * speed, rb.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void Jump()
    {
        //One jump or Double jump
        if (Input.GetKeyDown(KeyCode.Space) && gc.nJumping > 0 && WallJump.isSliding == false && Dash._isDashing == false)
        {
            gc.nJumping--;
            rb.gravityScale = gravityScale;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            //Varies the force of the jump taking into the account height,mass,gravity of the world and the player
            forceJ = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
            rb.AddForce(new Vector2(rb.velocity.x, forceJ), ForceMode2D.Impulse);
        }

        if (!gc.isGround)
        {
            if (!WallJump.isSliding && rb.velocity.y > 0)
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isFall", false);
                anim.SetBool("isSlider", false);
            }
            else if(!WallJump.isSliding && rb.velocity.y < 0)
            {
               
                anim.SetBool("isJumping", false);
                anim.SetBool("isFall", true);
                anim.SetBool("isSlider", false);

            }
            
            if (WallJump.isSliding && rb.velocity.y > 0)
            {
                anim.SetBool("isSlider", true);
                anim.SetBool("isJumping", false);
                anim.SetBool("isFall", false);

            }else if (WallJump.isSliding && rb.velocity.y < 0)
            {
                anim.SetBool("isSlider", true);
                anim.SetBool("isJumping", false);
                anim.SetBool("isFall", false);
            }

        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFall", false);
            anim.SetBool("isSlider", false);
        }

        if (WallJump.onWall)
        {
            gc.nJumping = 1;
        }

        //More gravity on the jump fall and WallJump
        if (rb.velocity.y > 0 && Dash._isDashing == false)
        {
            rb.gravityScale = gravityScale;
        }
        else if(rb.velocity.y < 0 && Dash._isDashing == false)
        {
            rb.gravityScale = fallGravityScale;
        }
    }

    void Flip()
    {
        if (Dash._isDashing == false && WallJump.isSliding == false)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                direcao = 1f;
                Vector3 scale = Vector3.one;
                scale.x = direcao;
                skin.transform.localScale = scale;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                direcao = -1f;
                Vector3 scale = Vector3.one;
                scale.x = direcao;
                skin.transform.localScale = scale;
            }
            else
            {
                direcao = skin.transform.localScale.x;
                skin.transform.localScale = new Vector3(direcao, skin.transform.localScale.y, skin.transform.localScale.z);
            }
        }
    }

    void Ladders()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,Vector2.up,distanceRayLa,layerLadders);

        if (hitInfo.collider  != null)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isClimbing = true;

            }else if (Input.GetAxisRaw("Horizontal") != 0)
            {
                isClimbing = false;
            }

        }
        else
        {
            isClimbing = false;
        }

        if (isClimbing && hitInfo.collider != null)
        {
            float moveY = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x,moveY *speedY);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.up * distanceRayLa);
    }
}