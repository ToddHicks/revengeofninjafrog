using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRhino : Enemy
{
    [Header("Rhino info")]
    [SerializeField] private float aggressiveSpeed;
    [SerializeField] private float shockTime;
    private float shockTimeCounter;


    protected override void Start(){
        base.Start();
        invincible = true;
    }

    private void Update(){
        CollisionChecksAndVelocityUpdate();
        if(playerDetected){
            isAggressive = true;
        }
        if(!isAggressive){
            WalkAround();
        }
        else{
            rb.velocity = new Vector2(aggressiveSpeed * facingDirection, rb.velocity.y);
            if(wallDetected && invincible){
                invincible = false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter <= 0 && !invincible) {
                invincible = true;
                Flip();
                isAggressive = false;
            }
            shockTimeCounter -= Time.deltaTime;
        }
        anim.SetBool("invincible", invincible);
        
    }
    public override void DestroyMe(){
        base.DestroyMe();
        // I don't know how many of these are needed. Goal was to force out of shock.
        // Turn around, and high tail it! (if not dead).
        invincible = true;
        anim.SetBool("invincible", invincible);
        idleTimeCounter = idleTime;
        shockTimeCounter = 0;
        Flip();
        wallDetected = false;
    }
}
