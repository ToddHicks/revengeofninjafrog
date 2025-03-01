using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadish : Enemy
{
    private bool groundBelowDetected;
    private bool groundAboveDetected;
    [Header("Radish info")]
    [SerializeField] private float groundAboveCheckDistance;
    [SerializeField] private float groundBelowCheckDistance;
    [SerializeField] private float aggressiveTime;
    private float aggresiveTimeCounter;
    [SerializeField] private float flyForce;

    protected override void Start(){
        base.Start();
    }
    private void Update(){
        aggresiveTimeCounter -= Time.deltaTime;
        if(aggresiveTimeCounter <= 0){
            isAggressive = false;
            rb.gravityScale = 1;
            invincible = false;
        }
        if(!isAggressive){
            if(groundBelowDetected && !groundAboveDetected){
                rb.velocity = new Vector2(0,flyForce);
            }
        }
        else{
            //if(rb.velocity.y >= 0){
            if(groundDetected){
                invincible = false;
                WalkAround();
            }
            /*else if(rb.velocity.y >=0){
                Flip();
            }*/
        }
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isAggressive", isAggressive);
        CollisionChecksAndVelocityUpdate();
    }
    public override void Damage(){
        if(isAggressive && !invincible){
            base.Damage();
        }
        else{
            invincible = true;
            isAggressive = true;
            aggresiveTimeCounter = aggressiveTime;
            rb.gravityScale = 12;
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
