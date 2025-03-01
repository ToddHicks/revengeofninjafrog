using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSelection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>()!= null){
            GetComponent<Animator>().SetTrigger("activate");
            PlayerManager.instance.respawnPoint = transform;
            GameManager.instance.SavePlayerStats();
            AudioManager.instance.PlayConsistentSFX(4);
            Invoke("LoadLevelMenu", 3f);
        }
    }
    private void LoadLevelMenu(){
        PlayerManager.instance.inGame = true;
        SceneManager.LoadScene("MainSelector");
    }
}
