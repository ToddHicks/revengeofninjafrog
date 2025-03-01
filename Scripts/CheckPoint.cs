using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>()!= null){
            GetComponent<Animator>().SetTrigger("activate");
            PlayerManager.instance.respawnPoint = transform;
            GameManager.instance.SavePlayerStats();
            AudioManager.instance.PlayConsistentSFX(4);
        }
    }
}
