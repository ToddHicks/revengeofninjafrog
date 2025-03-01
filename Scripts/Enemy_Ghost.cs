using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    [Header("Ghost info")]
    [SerializeField] private float activeTime;
    private float activeTimeCounter;
    private SpriteRenderer sr;

    private Transform player;
    
    protected override void Start(){
        base.Start();
        player = PlayerManager.instance.currentPlayer.transform;
        sr = GetComponent<SpriteRenderer>();
        activeTimeCounter = activeTime;
        isAggressive = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(player == null)
            player = PlayerManager.instance.currentPlayer.transform;
        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if(activeTimeCounter > 0){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && isAggressive){
            anim.SetTrigger("disappear");
            idleTimeCounter = idleTime;
            isAggressive = false;
            invincible = true;
        }
        else if(activeTimeCounter < 0 && idleTimeCounter < 0 && !isAggressive){
            Reposition();
            anim.SetTrigger("appear");
            activeTimeCounter = activeTime;
            isAggressive = true;
            invincible = false;
        }
        if(facingDirection == -1 && transform.position.x < player.transform.position.x){
            Flip();
        }
        else if(facingDirection == 1 && transform.position.x > player.transform.position.x){
            Flip();
        }
    }
    private void Reposition(){
        float yOffset = Random.Range(2,6);
        float xOffSet = Random.Range(2,6);
        System.Random rnd = new System.Random();
        bool negativeY = rnd.Next(2) == 0;
        bool negativeX = rnd.Next(2) == 0;
        if (negativeY){
            yOffset = yOffset * -1;
        }
        if(negativeX){
            xOffSet = xOffSet * -1;
        }

        transform.position = new Vector2(player.transform.position.x + xOffSet, player.transform.position.y + yOffset);
        
    }
    public void Disappear() => sr.enabled = false;
    
    public void Appear() => sr.enabled = true;

    protected override void OnTriggerEnter2D(Collider2D collision){
        if(isAggressive){
            base.OnTriggerEnter2D(collision);
        }
    }
    /**
    Other ghost idea. Override DestroyMe() and make it force isAggressive false
    and reset the timers. This way, it can't be killed, but can be forced invisible for a period.

    */
}
