using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private VolumeControllerUI[] volumeController;
    [SerializeField] private GameObject levelSelection;
    private void Start(){
        bool showButton = PlayerPrefs.GetInt("played")==1?true:false;
        continueButton.SetActive(showButton);
        for(int i = 0; i < volumeController.Length; ++i){
            volumeController[i].SetSliderValue();
        }
        if(PlayerManager.instance.inGame == true){
            SwitchMenuTo(levelSelection);
        }
    }
    public void SwitchMenuTo(GameObject menu){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).gameObject.SetActive(false);
        }
        AudioManager.instance.PlaySFX(7);
        menu.SetActive(true);
    }
    public void NewGame(){
        GameManager.instance.NewGame();
        PlayerPrefs.SetInt("played",1);
    }
}
