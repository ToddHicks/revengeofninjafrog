using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lives;
    [SerializeField] private TextMeshProUGUI hitpoints;
    [SerializeField] private TextMeshProUGUI apples;
    [SerializeField] private TextMeshProUGUI bananas;
    [SerializeField] private TextMeshProUGUI cherries;
    [SerializeField] private TextMeshProUGUI melons;
    [SerializeField] private TextMeshProUGUI pineapples;
    [SerializeField] private TextMeshProUGUI messages;
    public static StatsPresenter instance;
    public void Start(){
        instance = this;
        if(PlayerManager.instance != null){
            PlayerManager.instance.Start();
        }
    }
    public void SetLives(int count){
        lives.text=count.ToString();
    }
    public void SetHitPoints(int count){
        hitpoints.text=count.ToString();
    }
    public void SetApples(int count){
        apples.text=count.ToString();
    }
    public void SetBananas(int count){
        bananas.text=count.ToString();
    }
    public void SetCherries(int count){
        cherries.text=count.ToString();
    }
    public void SetMelons(int count){
        melons.text=count.ToString();
    }
    public void SetPineapples(int count){
        pineapples.text=count.ToString();
    }
    public void SetMessage(string message, float duration){
        messages.text=message;
        Invoke("ClearMessage",duration);
    }
    private void ClearMessage(){
        messages.text="";
    }
}
