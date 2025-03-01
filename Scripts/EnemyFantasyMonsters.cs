using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Scripts;
//using Assets.FantasyMonsters.Scripts.Monster;
//using Assets.FantasyMonsters.Scripts.MonsterState;

public class EnemyFantasyMonsters : Enemy
{
    protected Assets.FantasyMonsters.Scripts.Monster monster;
    float originalSpeed;
    // Start is called before the first frame update
    protected override void Start()
    { 
        monster = gameObject.GetComponent<Monster>();
        base.Start();
        originalSpeed = speed;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        WalkAround();
        CollisionChecksAndVelocityUpdate();
        if(playerDetected){
            if(originalSpeed == speed){
                speed = originalSpeed * 2;
            }
            monster.SetState(MonsterState.Jump);
        }
        else{
            speed = originalSpeed;
            monster.SetState(MonsterState.Walk);
        }
        if(hitPoints <= 0){
            speed = 0;
            monster.SetState(MonsterState.Death);
        }
        else if (isKnocked){
            speed = 0;
            monster.SetState(MonsterState.Death);
        }
    }
    protected override void WalkAround(){
        base.WalkAround();
    }
    public override void Damage(){
        if(!isKnocked && !invincible){
            isKnocked = true;
            //monster.SetState(MonsterState.Attack);
            //Invoke("DestroyMe", 0.5f);
            DestroyMe();
        }
    }
    public override void DestroyMe(){
        int atk = Player.instance.attackLevel;
        hitPoints = hitPoints - atk;
        if(hitPoints<=0){
            Invoke("Destroyed", 1.5f);
            //Destroy(gameObject);
        }
        else{
            //monster.Attack();
            monster.SetState(MonsterState.Death);
            Invoke("UnKnocked", 0.5f);
        }
        //isKnocked = false;
        // Needs some love yet.
        //invincible = true;
        //Invoke("CancelInvincible", 3f);
    }
    public void UnKnocked(){
        isKnocked = false;
    }
    public void Destroyed(){
        if(deathFx != null)
        {
            GameObject newDeathFx = Instantiate(deathFx, transform.position, transform.rotation);
            Destroy(newDeathFx, .3f);
        }
        if(fruitDrop != null){
            Instantiate(fruitDrop, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
