using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Elder_1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Button next;
    [SerializeField] private GameObject conversationScreen; // canvas with text and button.
    [SerializeField] private GameObject regularScreen;
    [SerializeField] private string[] conversation;
    [SerializeField] private string[] levelsToUnlock;
    private bool interacting = false;
    private bool inConversation = false;
    private int conversationTracker = 0;
    private bool conversed = false;
    private void Awake(){
        next.onClick.RemoveAllListeners();
        next.onClick.AddListener(NextMessage);
    }
    private void Update(){
        if(PlayerManager.instance.pcMode && interacting && inConversation){
            if(Input.GetKeyDown(KeyCode.Space)){
                NextMessage();
            }
        }
        if(interacting && !inConversation && !conversed){
            PlayerManager.instance.currentPlayer.GetComponent<Player>().canBeControlled = false;
            PlayerManager.instance.currentPlayer.GetComponent<Player>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            inConversation = true;
            SwitchMenuTo(conversationScreen);
        }
        else if(interacting && inConversation){
            if(conversationTracker == 0){
                SetMessage("I am so glad you returned...");
                conversationTracker++;
            }
        }
        else if(!interacting && inConversation){
            inConversation = false;
            conversationTracker = 0;
            SwitchMenuTo(regularScreen);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        // if(collision.GetComponent<Player>()!=null)
        if(collision.tag == "Player" && !interacting){
            interacting = true;
            // do I maybe need a timer? wait 5 seconds.
        }
    }
    public void SetMessage(string message){
        textDisplay.text=message;
    }
    private void ClearMessage(){
        textDisplay.text="";
    }
    public void SwitchMenuTo(GameObject menu){
        regularScreen.SetActive(false);
        conversationScreen.SetActive(false);
        menu.SetActive(true);
    }
    private void NextMessage(){
        if(conversationTracker==1){
            SetMessage("Crisp P. Bacon waited for us to vulnerable and attacked.");
        }
        else if(conversationTracker==2){
            SetMessage("We need your help!");
        }
        else if(conversationTracker==2){
            SetMessage("Please save our people. They have been taken and you \nare our only hope!");
        }
        else if(conversationTracker==3){
            ClearMessage();
            PlayerManager.instance.highestLevelUnlocked=0;
            interacting = false;
            PlayerManager.instance.currentPlayer.GetComponent<Player>().canBeControlled = true;
            conversed = true;
            SwitchMenuTo(regularScreen);
            GameManager.instance.SavePlayerStats();
        }
        conversationTracker++;
    }
}
