using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Friend : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Button next;
    [SerializeField] private GameObject conversationScreen; // canvas with text and button.
    [SerializeField] private GameObject regularScreen;
    [SerializeField] private string[] conversation;
    [SerializeField] private string[] levelsToUnlock;
    [SerializeField] private int highestLevelToUnlock;
    [SerializeField] private string uniqueName;
    private bool interacting = false;
    private bool inConversation = false;
    private int conversationTracker = 0;
    private bool conversed = false;
    private void Awake(){
        // Moved these calls to on colision so we can reuse the next button
        // for all other dialog as well.
        //next.onClick.RemoveAllListeners();
        //next.onClick.AddListener(NextMessage);
        if(uniqueName != null){
            // Get information about interacted with. If already done, destroy.
            if(PlayerPrefs.GetInt(uniqueName)==1){
                Destroy(gameObject);
            }
        }
    }
    private void Update(){
        if(PlayerManager.instance.pcMode && interacting && inConversation){
            if(Input.GetKeyDown(KeyCode.Space)){
                NextMessage();
            }
        }
        if(interacting && !inConversation && !conversed){
            PlayerManager.instance.currentPlayer.GetComponent<Player>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            inConversation = true;
            SwitchMenuTo(conversationScreen);
        }
        else if(interacting && inConversation){
            if(conversationTracker == 0){
                PlayerManager.instance.currentPlayer.GetComponent<Player>().canBeControlled = false;
                SetMessage(conversation[0]);
                conversationTracker++;
            }
        }
        else if(!interacting && inConversation){
            inConversation = false;
            conversationTracker = 0;
            SwitchMenuTo(regularScreen);
            if(uniqueName != null){
                PlayerPrefs.SetInt(uniqueName, 1);
                AudioManager.instance.PlaySFX(8); // ? just selected random.
                Destroy(gameObject);
            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        // if(collision.GetComponent<Player>()!=null)
        if(collision.tag == "Player" && !interacting){
            interacting = true;
            next.onClick.RemoveAllListeners();
            next.onClick.AddListener(NextMessage);
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
        if(conversation.Length > conversationTracker){
            SetMessage(conversation[conversationTracker]);
            conversationTracker++;
        }
        else {
            ClearMessage();
            PlayerManager.instance.highestLevelUnlocked=highestLevelToUnlock;
            interacting = false;
            PlayerManager.instance.currentPlayer.GetComponent<Player>().canBeControlled = true;
            conversed = true;
            SwitchMenuTo(regularScreen);
            GameManager.instance.SavePlayerStats();
            foreach(string level in levelsToUnlock){

            }

        }
    }
}
