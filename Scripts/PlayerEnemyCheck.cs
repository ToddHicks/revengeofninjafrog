using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyCheck : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        Debug.Log("Here");
        Player player = PlayerManager.instance.currentPlayer.GetComponent<Player>();
        if(collision.GetComponent<Enemy>()!=null && player.isFalling){
            Enemy newEnemy = collision.GetComponent<Enemy>();
            if(newEnemy.invincible){
                return;
            }
            // This is we only want them to damage when falling.
            // I like damaging while going up if timed right, why not?
            //if(rb.velocity.y < 0){
            // using the isFalling variable outside CheckForEnemies instead.
            newEnemy.Damage();
            player.isWallSliding = false;
            player.canWallSlide = false;
            AudioManager.instance.PlaySFX(3);
            player.Jump();
        }
    }
}
