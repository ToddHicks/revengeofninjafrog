using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    [SerializeField] private int unlockLevel;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>()!= null){
            GetComponent<Animator>().SetTrigger("activate");
            PlayerManager.instance.respawnPoint = transform;
            if(PlayerManager.instance.highestLevelUnlocked < unlockLevel)
                PlayerManager.instance.highestLevelUnlocked = unlockLevel;
            GameManager.instance.SavePlayerStats();
            AudioManager.instance.PlayConsistentSFX(4);
            Invoke("LoadNextLevel", 1f);
        }
    }
    private void LoadNextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
