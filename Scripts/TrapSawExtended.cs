using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSaw2 : Trap
{
    private Animator anim;
    [SerializeField] private Transform[] movePoint;
    [SerializeField] private float speed;
    private bool reverse = false;
    
    private int movePointIndex;


    void Start(){
        anim = GetComponent<Animator>();
        anim.SetBool("isWorking", true);
        transform.position = movePoint[0].position;
    }
    void Update(){
        
        transform.position = Vector3.MoveTowards(transform.position, movePoint[movePointIndex].position, speed * Time.deltaTime);
        
        if(Vector2.Distance(transform.position, movePoint[movePointIndex].position) < 0.25f){
            
            if(movePointIndex == movePoint.Length-1){
                reverse = true;
                Flip();
            }
            else if (movePointIndex == 0){
                reverse = false;
                Flip();
            }
            if(!reverse){
                movePointIndex++;
            }
            else{
                movePointIndex--;
            }
        }
    }
    private void Flip(){
        transform.localScale = new Vector3(1, transform.localScale.y * -1);
    }
}
