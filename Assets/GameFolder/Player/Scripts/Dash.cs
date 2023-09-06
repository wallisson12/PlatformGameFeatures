using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [Header("Dash")]
    public static bool _isDashing;
    [SerializeField] private float _dashingVelocity;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashTime,_dashValue;
    [SerializeField] private bool _canDash;

    [Header("Reference Player")]
    [SerializeField] private PlayerController p;
    
    void Start()
    {
        p = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        Dashh();
    }

    void Dashh()
    {
        if (p.gc.isGround && !_isDashing)
        {
            _canDash = true;

        }else if (WallJump.isSliding && !_isDashing)
        {
            _canDash = true;
        }

        _dashTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C) && _canDash && _dashTime > _dashValue)
        {
            _dashTime = 0f;
            p.rb.gravityScale = 0f;
            p.rb.velocity = Vector2.zero;
            p.anim.Play("Dash_Animation");
            _isDashing = true;
            _canDash = false;
            Invoke("SetDashingFalse", _dashDuration);
        }

        if (_isDashing)
        {
            p.rb.AddForce(new Vector2(_dashingVelocity * p.direcao, 0f));
        }

    }

    void SetDashingFalse()
    {
        p.rb.gravityScale = p.gravityScale;
        _isDashing = false;
    }
}
