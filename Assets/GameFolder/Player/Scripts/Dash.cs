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

    [Header("Dash Particle")]
    [SerializeField] private ParticleSystem dashParticle;
    [SerializeField] private ParticleSystem.MainModule mainModule;
    [SerializeField] private ParticleSystemRenderer renderParticle;
    [SerializeField] private float timeParticle;

    void Start()
    {
        p = FindObjectOfType<PlayerController>();
        mainModule = dashParticle.main;
        renderParticle = dashParticle.GetComponent<ParticleSystemRenderer>();
        dashParticle.Stop();
    }

    void Update()
    {
        timeParticle += Time.deltaTime;

        if (timeParticle > 1f)
        {
            timeParticle = 1f;
        }

        Dashh();

        //Particle Direction
        if (p.direcao > 0 && _isDashing == false && TimeParticle())
        {
            mainModule.startSpeed = 10f;
            renderParticle.flip = Vector3.zero;
            
        }
        else if(p.direcao < 0 && _isDashing == false && TimeParticle())
        {
            mainModule.startSpeed = -10f;
            renderParticle.flip = Vector3.right;
        }
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
            dashParticle.Play();
            timeParticle = 0f;
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
        _isDashing = false;
        dashParticle.Stop();
    }

    bool TimeParticle()
    {
        if (timeParticle == 1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
