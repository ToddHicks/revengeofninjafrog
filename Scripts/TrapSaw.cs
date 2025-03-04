using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw : Trap
{
    private Animator anim;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown;

    private float cooldownTimer;
    private int movePointIndex;


    void Start(){
        anim = GetComponent<Animator>();
        transform.position = movePoint[0].position;
    }
    void Update(){
        cooldownTimer -= Time.deltaTime;
        bool isWorking = cooldownTimer < 0;
        anim.SetBool("isWorking", isWorking);
        if(isWorking)
            transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.25f){
            Flip();
            cooldownTimer = cooldown;
            movePointIndex++;
            if(movePointIndex >= movePoint.Length){
                movePointIndex=0;
            }
        }
    }
    private void Flip(){
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
}
