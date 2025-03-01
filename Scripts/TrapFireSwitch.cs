using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSwitch : MonoBehaviour
{
    public TrapFire myTrap;
    private Animator anim;

    private float countDown;
    [SerializeField] private float timeNotActive = 2;

    private void Start(){
        anim = GetComponent<Animator>();
    }
    // private void Update(){
    //     countDown -= Time.deltaTime;
    // }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>() != null){
            // if(countdown > 0){
            //     return;
            // }
            // countdown = timeNotActive;
            // anim.SetTrigger("pressed");
            // mytrap.FireSwitchAfter(timeNotActive);

            anim.SetTrigger("pressed");
            myTrap.FireSwitch();
        }
    }
}
