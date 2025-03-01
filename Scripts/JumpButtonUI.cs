using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonUI : MonoBehaviour , IPointerDownHandler
{
    private Player player;
    public void OnPointerDown(PointerEventData eventData){
        if(PlayerManager.instance.currentPlayer!=null){
            if(PlayerManager.instance.currentPlayer.GetComponent<Player>().canBeControlled){
                /*player = PlayerManager.instance.currentPlayer.GetComponent<Player>();
                player.JumpButton();*/
                PlayerManager.instance.currentPlayer.GetComponent<Player>().JumpButton();
            }
        }
    }
}
