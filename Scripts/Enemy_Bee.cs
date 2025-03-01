using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bee : Enemy
{
    [Header("Bee info")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private float yOffset;

    [Header("Bullet info")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;

    private Transform player;
    private float defaultSpeed;
    private int idlePointIndex;

    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
        player = PlayerManager.instance.currentPlayer.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = PlayerManager.instance.currentPlayer.transform;
        }
        bool idle = idleTimeCounter > 0;
        anim.SetBool("idle", idle);
        idleTimeCounter -= Time.deltaTime;
        if(idle){
            return;
        }
        playerDetected = Physics2D.OverlapCircle(playerCheck.position, checkRadius, whatIsPlayer);
        if(playerDetected && !isAggressive){
            isAggressive = true;
            speed = defaultSpeed *2;
        }
        if(!isAggressive){
            transform.position = Vector2.MoveTowards(transform.position, idlePoint[idlePointIndex].position, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, idlePoint[idlePointIndex].position) < .1f){
                idlePointIndex ++;
                if(idlePointIndex >= idlePoint.Length){
                    idlePointIndex = 0;
                }
            }

        }
        else{
            Vector2 newPos = new Vector2(player.transform.position.x, player.transform.position.y + yOffset);
            transform.position = Vector2.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
            float xDifference = transform.position.x - player.position.x;
            if(Mathf.Abs(xDifference)< .15f){
                anim.SetTrigger("attack");
                idleTimeCounter = idleTime;
                isAggressive = false;
            }
        }
    }
    private void AttackEvent(){
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        newBullet.GetComponent<Bullet>().SetSpeed(0, -bulletSpeed);
        idleTimeCounter = idleTime;
        isAggressive = false;
        speed = defaultSpeed;
    }

    protected override void OnDrawGizmos(){
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(playerCheck.position, checkRadius);
    }
}
