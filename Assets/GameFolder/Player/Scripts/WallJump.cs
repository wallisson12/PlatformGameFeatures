using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public  static bool onWall;
    [SerializeField] private bool rightWall,leftWall;
    [SerializeField] private Vector3 wallOffset; 
    [SerializeField] private float wallradius;
    [SerializeField] private LayerMask walllayer;
    [SerializeField] private PlayerController player;

    [Header("WallJump")]
    public float speedJumpWall;
    public bool wallJumping;
    public float wallJumpTime;
    public float xWallForce,yWallForce;
    public static bool isSliding;


    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        rightWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffset.x,0f),wallradius,walllayer);
        leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffset.x, 0f), wallradius, walllayer);

        if (rightWall || leftWall)
        {
            onWall = true;
        }
        else
        {
            onWall = false;
        }

        WallJumpp();
    }

    void WallJumpp()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 && onWall == true && player.gc.isGround == false)
        {
            isSliding = true;
            player.rb.velocity = new Vector2(player.rb.velocity.x, Mathf.Clamp(player.rb.velocity.y, -speedJumpWall, float.MaxValue));
        }
        else
        {
            isSliding = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingFalse", wallJumpTime);
        }

        if (wallJumping == true)
        {
            player.rb.velocity = new Vector2(-player.direcao * xWallForce, yWallForce);
        }
    }

    void SetWallJumpingFalse()
    {
        wallJumping = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(wallOffset.x, 0f),wallradius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOffset.x, 0f), wallradius);
    }
}
