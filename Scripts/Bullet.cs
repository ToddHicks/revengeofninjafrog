using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Trap
{
    private Rigidbody2D rb;
    private float xSpeed;
    private float ySpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    public void SetSpeed(float x, float y){
        xSpeed = x;
        ySpeed = y;
    }
    protected override void OnTriggerEnter2D(Collider2D collision){
        Destroy(gameObject);
        base.OnTriggerEnter2D(collision);
    }
}
