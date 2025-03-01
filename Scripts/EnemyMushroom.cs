using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{
    private void Update(){
        WalkAround();
        CollisionChecksAndVelocityUpdate();
    }
}

