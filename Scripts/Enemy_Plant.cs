using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Plant : Enemy
{
    [Header("Plant info")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool facingRight;
    // Start is called before the first frame update
    protected override void Start()
    {
        if(facingRight){
            Flip();
            bulletSpeed = bulletSpeed * -1;
            playerCheckDistance = playerCheckDistance * -1;
        }
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionChecks();
        idleTimeCounter -= Time.deltaTime;

        if(idleTimeCounter < 0 && playerDetected){
            anim.SetTrigger("attack");
            idleTimeCounter=idleTime;
        }
    }
    private void AttackEvent(){
        GameObject newBullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        newBullet.GetComponent<Bullet>().SetSpeed(bulletSpeed * facingDirection, 0);
    }
}
