using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : Enemy
{
    private float defaultIdleTime;
    protected override void Start(){
        base.Start();
        defaultIdleTime = idleTime;
    }
    void Update()
    {

        CollisionChecksAndVelocityUpdate();
        WalkAround();
    }
    public override void DestroyMe(){
        base.DestroyMe();
        if(isAggressive){
            speed = speed / 2;
            isAggressive = false;
            idleTime = defaultIdleTime;
        }
        else{
            speed = speed * 2;
            isAggressive = true;
            idleTime = 0;
        }
    }
}
