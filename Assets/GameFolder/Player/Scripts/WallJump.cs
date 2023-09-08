using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJump : MonoBehaviour
{
    public static bool onWall;
    [SerializeField] private bool rightWall, leftWall;
    [SerializeField] private Vector3 wallOffset1, wallOffset2;
    [SerializeField] private float wallradius;
    [SerializeField] private LayerMask walllayer;
    [SerializeField] private PlayerController player;

    [Header("WallJump")]
    public float speedJumpWall;
    public bool wallJumping;
    public float wallJumpTime;
    public float xWallForce, yWallForce;
    public static bool isSliding;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        rightWall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOffset1.x, 0f), wallradius, walllayer);
        leftWall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOffset2.x, 0f), wallradius, walllayer);

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
        if (onWall == true && player.gc.isGround == false)
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
            player.rb.velocity = Vector2.zero;
            wallJumping = true;
            Invoke("SetWallJumpingFalse", wallJumpTime);
        }

        if (wallJumping)
        {
            player.rb.AddForce(new Vector2(xWallForce * -player.direcao, yWallForce));
        }
    }

    void SetWallJumpingFalse()
    {
        wallJumping = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(wallOffset1.x, 0f), wallradius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOffset2.x, 0f), wallradius);
    }
}