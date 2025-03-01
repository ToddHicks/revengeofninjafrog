using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float pushForce = 20;
    [SerializeField] private bool canBeUsed = true;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>() != null && canBeUsed){
            GetComponent<Animator>().SetTrigger("activate");
            AudioManager.instance.PlaySFX(14);
            collision.GetComponent<Player>().Push(pushForce + (PlayerManager.instance.activeJumpLevel-1)*2);
            canBeUsed = false;
        }
    }
    private void CanUse(){
        canBeUsed = true;
    }
}
