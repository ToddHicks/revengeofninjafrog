using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBunny : Enemy
{
    [Header("Bunny Info")]
    [SerializeField] protected float platformCheckDistance;
    [SerializeField] protected float ceilingCheckDistance;
    [SerializeField] protected Transform ceilingCheck1;
    [SerializeField] protected Transform ceilingCheck2;
    [SerializeField] protected Transform platformCheck;
    [SerializeField] protected float jumpForce;
    private bool ceilingDetected;
    private bool platformDetected;
    private bool jumping;
    // Update is called once per frame
    void Update()
    {
        if(jumping && groundDetected){
            jumping = false;
        }
        else if(!ceilingDetected && platformDetected && !jumping && idleTimeCounter <= 0){
            Jump();
            jumping = true;
        }
        
        CollisionChecks();
        WalkAround();
    }
    protected override void CollisionChecks(){
        base.CollisionChecks();
        bool cd1 = Physics2D.Raycast(ceilingCheck1.position, Vector2.up, ceilingCheckDistance, whatIsGround);
        bool cd2 = Physics2D.Raycast(ceilingCheck2.position, Vector2.up, ceilingCheckDistance, whatIsGround);
        ceilingDetected =  cd1 || cd2;
        platformDetected = Physics2D.Raycast(platformCheck.position, Vector2.up, platformCheckDistance,whatIsGround);
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetFloat("xVelocity",rb.velocity.x);
        anim.SetBool("groundDetected", groundDetected);
        anim.SetBool("jumping", jumping);
    }
    private void Jump(){
        rb.velocity = new Vector2(speed, jumpForce*4);
        //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //rb.AddForce(new Vector2(speed,jumpForce),ForceMode2D.Impulse);
    }
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawLine(ceilingCheck1.position, new Vector2(ceilingCheck1.position.x, ceilingCheck1.position.y + ceilingCheckDistance));
        Gizmos.DrawLine(ceilingCheck2.position, new Vector2(ceilingCheck2.position.x, ceilingCheck2.position.y + ceilingCheckDistance));
        Gizmos.DrawLine(platformCheck.position, new Vector2(platformCheck.position.x, platformCheck.position.y + platformCheckDistance));
    }
    protected override void WalkAround(){
        if(idleTimeCounter <= 0 && !isKnocked){
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
            if(wallDetected){
                idleTimeCounter = idleTime;
                Flip();
            }
        }
        else{
            rb.velocity = new Vector2(0,0);
        }
        idleTimeCounter -= Time.deltaTime;
    }
}
