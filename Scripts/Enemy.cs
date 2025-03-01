using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    protected int facingDirection = -1;
    [Header("Environment info")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform groundCheck2;
    [SerializeField] protected Transform wallCheck;
    [Header("Stat info")]
    [SerializeField] protected int hitPoints;
    [SerializeField] protected float idleTime = 2;
    [SerializeField] protected float speed;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected float playerCheckDistance;
    [Header("Destroy Effects")]
    [SerializeField] protected GameObject deathFx;
    [SerializeField] protected GameObject fruitDrop;
    protected bool wallDetected;
    protected bool groundDetected;
    protected bool facingEdge;
    protected bool isKnocked;
    protected float idleTimeCounter;
    protected bool isAggressive;
    protected bool playerDetected;

    [HideInInspector] public bool invincible;

    protected virtual void Start(){
        facingDirection = -1;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if(groundCheck == null)
            groundCheck = transform;
        if(groundCheck2 == null)
            groundCheck2 = transform;
        if(wallCheck == null)
            wallCheck = transform;
    }
    protected virtual void WalkAround(){
        if(idleTimeCounter <= 0 && !isKnocked){
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
            if(wallDetected || facingEdge){
                idleTimeCounter = idleTime;
                Flip();
            }
        }
        else{
            rb.velocity = new Vector2(0,0);
        }
        idleTimeCounter -= Time.deltaTime;
    }
    public virtual void Damage(){

        if(!isKnocked && !invincible){
            isKnocked = true;
            anim.SetBool("isKnocked", isKnocked);
        }
    }
    public virtual void DestroyMe(){
        int atk = Player.instance.attackLevel;
        hitPoints = hitPoints - atk;
        if(hitPoints<=0){
            if(deathFx != null){
                GameObject newDeathFx = Instantiate(deathFx, transform.position, transform.rotation);
                Destroy(newDeathFx, .3f);
            }
            if(fruitDrop != null){
                Instantiate(fruitDrop, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        isKnocked = false;
        anim.SetBool("isKnocked", isKnocked);
        // Needs some love yet.
        //invincible = true;
        //Invoke("CancelInvincible", 3f);
    }
    private void CancelInvincible(){
        invincible = false;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>() != null){
            Player player = collision.GetComponent<Player>();
            player.Knockback(transform);
        }
    }
    protected virtual void Flip(){
        transform.Rotate(0,180,0);
        facingDirection = facingDirection *-1;
    }
    protected virtual void CollisionChecks(){
        playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection,playerCheckDistance, whatIsPlayer);
        bool gd1 = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        bool gd2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, whatIsGround);
        groundDetected =  gd1 || gd2;
        facingEdge = !gd1;
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance,whatIsGround);
    }
    protected virtual void CollisionChecksAndVelocityUpdate(){
        CollisionChecks();
        anim.SetFloat("xVelocity", rb.velocity.x);
    }
    protected virtual void OnDrawGizmos(){
        if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if(groundCheck2 != null)
            Gizmos.DrawLine(groundCheck2.position, new Vector2(groundCheck2.position.x, groundCheck2.position.y - groundCheckDistance));
        if(wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerCheckDistance * facingDirection, wallCheck.position.y));
        }
    }
}
