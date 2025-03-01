using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrunk : Enemy
{
    [Header("Trunk info")]
    [SerializeField] private float fleeSpeed;
    [SerializeField] private float fleeTriggerDistance;
    [SerializeField] private Transform fleePlayerCheck;
    private bool triggerFlee;
    private bool fleeing;
    private float defaultSpeed;
    [SerializeField] private float fleeTime;
    private float fleeTimeCounter;
    [Header("Bullet info")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootDelay;
    private float shootDelayCounter;

    
    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
        isAggressive = false;
    }


    void Update()
    {
        CollisionChecks();
        if(triggerFlee && !isAggressive){
            speed = -fleeSpeed;
            fleeing = true;
            fleeTimeCounter = fleeTime;
        }
        if(fleeing){

            fleeTimeCounter -= Time.deltaTime;
            if(fleeTimeCounter <= 0){
                speed = defaultSpeed;
                fleeing = false;
            }
        }
        if(!isAggressive)
        {
            WalkAround();
            shootDelayCounter -= Time.deltaTime;
        }
        anim.SetFloat("xVelocity", rb.velocity.x);
        if(playerDetected && !fleeing && !isAggressive && shootDelayCounter <= 0){
            isAggressive = true;
            anim.SetTrigger("attack");
            shootDelayCounter = shootDelay;
            rb.velocity = new Vector2(0,0);
        }
    }
    protected override void CollisionChecks(){
        base.CollisionChecks();
        triggerFlee = Physics2D.OverlapCircle(fleePlayerCheck.position, fleeTriggerDistance, whatIsPlayer);

    }
    private void AttackEvent(){
        //shootDelayCounter = shootDelay;
        isAggressive = false;
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        if(newBullet)
            newBullet.GetComponent<Bullet>().SetSpeed(bulletSpeed * facingDirection, 0);
    }
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        // Flees just in front
        //Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + fleeTriggerDistance * facingDirection, wallCheck.position.y));
        Gizmos.DrawWireSphere(fleePlayerCheck.position, fleeTriggerDistance);
    }
}
