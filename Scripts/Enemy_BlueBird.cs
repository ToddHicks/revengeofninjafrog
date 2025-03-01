using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BlueBird : Enemy
{
    private bool groundBelowDetected;
    private bool groundAboveDetected;
    [Header("BlueBird info")]
    [SerializeField] private float groundAboveCheckDistance;
    [SerializeField] private float groundBelowCheckDistance;
    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;
    private float flyForce;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        flyForce = flyUpForce;
    }

    // Update is called once per frame
    void Update()
    {
        CollisionChecksAndVelocityUpdate();
        if(groundAboveDetected){
            flyForce = flyDownForce;
        }
        else if(groundBelowDetected){
            flyForce = flyUpForce;
        }
        if(wallDetected){
            Flip();
        }
    }

    public override void Damage(){
        base.Damage();
    }

    public void FlyUpEvent(){
        if(!isKnocked){
            rb.velocity = new Vector2(speed * facingDirection, flyForce);
        }
        else{
            rb.velocity = new Vector2(0,0);
        }
    }

    protected override void CollisionChecks(){
        base.CollisionChecks();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, groundAboveCheckDistance, whatIsGround);
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, groundBelowCheckDistance, whatIsGround);
    }
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + groundAboveCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundBelowCheckDistance));
    }
}
