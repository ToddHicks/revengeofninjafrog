using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenUI : MonoBehaviour
{
    private bool paused = false;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject regularyScreen;
    [SerializeField] private GameObject levelScreen;
    private void Start(){
        Time.timeScale = 1;
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused){
                Cancel();
            }
            else{
                Time.timeScale = 0;
                SwitchMenuTo(pauseScreen);
                paused = true;
            }
        }
    }
    public void SwitchMenuTo(GameObject menu){
        if(menu == pauseScreen ){
            Time.timeScale = 0;
        }
        else if (menu == regularyScreen){
            Time.timeScale = 1;
        }
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
        menu.SetActive(true);
    }
    public void LoadMainMenu(){
        Time.timeScale = 1;
        PlayerManager.instance.inGame = false;
        SceneManager.LoadScene("MainSelector");
    }
    public void RetryLevel(){
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Cancel(){
        Time.timeScale = 1;
        paused = false;
        SwitchMenuTo(regularyScreen);
    }
}
