using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Boar : EnemyFantasyMonsters
{
    private bool playerAttackThreat;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private int unlockLevel;
    protected override void Update(){
        base.Update();
        if(playerAttackThreat && hitPoints > 0 && !invincible){
            isAggressive = true;
            invincible = true;
            monster.Attack();
            Invoke("UnInvincible", 0.4f);
        }
    }
    private void UnInvincible(){
        invincible = false;
    }

    protected override void CollisionChecks(){
        base.CollisionChecks();
        playerAttackThreat = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer);
    }

    public override void DestroyMe() {
        if(hitPoints <= 1) { // it'll get subtracted next, so check 1 instead of 0.
            if(PlayerManager.instance.highestLevelUnlocked < unlockLevel)
                PlayerManager.instance.highestLevelUnlocked = unlockLevel;
            GameManager.instance.SavePlayerStats();
            AudioManager.instance.PlayConsistentSFX(4); // Does this need to be better? Victory music of some sort?
            Invoke("LoadNextLevel", 1f); // Is this too short?
        }
        base.DestroyMe();
    }
    private void LoadNextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position,checkRadius);
    }
}
