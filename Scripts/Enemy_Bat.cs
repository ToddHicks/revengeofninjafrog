using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{
    [Header("Bat info")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    private Vector2 destination;
    private bool canBeAggressive = true;
    //[SerializeField] private LayerMask whatIsPlayer;
    private Transform player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = PlayerManager.instance.currentPlayer.transform;
        destination = idlePoint[0].position;
        transform.position = idlePoint[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = PlayerManager.instance.currentPlayer.transform;
        }
        anim.SetBool("canBeAggressive", canBeAggressive);
        idleTimeCounter -= Time.deltaTime;
        if(idleTimeCounter > 0){
            return;
        }
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        if(playerDetected && !isAggressive && canBeAggressive){
            isAggressive = true;
            canBeAggressive = false;
            destination = player.transform.position;
        }
        if(isAggressive){
            anim.SetFloat("speed", speed);
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, destination) < .001f){
                isAggressive = false;

                int i = Random.Range(0, idlePoint.Length);
                destination = idlePoint[i].position;

            }
        }
        else{
            anim.SetFloat("speed", speed*.5f);
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * .5f * Time.deltaTime);
            if(Vector2.Distance(transform.position, destination) < .001f){
                if(!canBeAggressive){
                    idleTimeCounter = idleTime;
                    canBeAggressive = true;
                }

            }
        }

        anim.SetBool("canBeAggressive", canBeAggressive);
        FlipController();
    }
    private void FlipController(){
        if(facingDirection == -1 && transform.position.x < destination.x){
            Flip();
        }
        else if(facingDirection == 1 && transform.position.x > destination.x){
            Flip();
        }
    }
    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position,checkRadius);
    }
}
