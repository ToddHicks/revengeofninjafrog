using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static Player instance;
    void Awake()
    {
        instance = this;
    }
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Move info")]
    [SerializeField] private float jumpForce;
    // Uncomment all associated values if we want
    // to make a weaker double jump.
    //public float doubleJumpForce = 8;
    //private float defaultJumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 wallJumpDirection;
    [Header("Levels")]

    [SerializeField] public int jumpLevel = 1;
    [SerializeField] public int speedLevel = 1;
    [SerializeField] public int attackLevel = 1;
    [SerializeField] public int hitPointLevel = 1;
    public int maxHitPoints = 3;
    public int currentHitPoints;
    // Creating this to limit speed up while jumping.
    private float maxJumpSpeed;

    private bool canDoubleJump;
    private bool canMove;

    //buffer jump is when you can hit jump just before you hit the ground.
    //[SerializeField] private float bufferJumpTime;
    //private float bufferJumpTimer;

    [SerializeField] private float coyoteJumpTime;
    private float coyoteJumpTimer;

    private float movingInput;
    public bool canBeControlled;

    private bool facingRight = true;
    private int facingDirection =1;

    [Header("Knockback info")]
    [SerializeField] private Vector2 knockbackDirection;
    private bool isKnocked;
    [SerializeField] private float knockbackTime;
    private bool canBeKnocked = true;

    [Header("Collision info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform groundCheck2;

    [Header("Damage")]
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;

    [Header("Control Info")]
    public bool pcMode;
    public VariableJoystick joystick;

    private bool isGrounded;
    private bool isWallDetected;
    public bool canWallSlide;
    public bool isWallSliding;
    public bool isFalling; // I added this, so that wall jumps can eventually
    // have standard controls like what we get when you normal jump.
    // We'll see if we keep it!
    private bool canHaveCoyoteJump;
    private float defaultGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHitPoints = maxHitPoints;
        defaultGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }

    void Update(){
        AnimationContollers();
        if(isKnocked)
            return;
        FlipController();
        CollisionChecks();
        InputChecks();
        if(isFalling)
            CheckForEnemies();
        //bufferJumpTimer -= Time.deltaTime;
        coyoteJumpTimer -= Time.deltaTime;

        if(isGrounded){
            canDoubleJump = true;
            canMove = true;
            isFalling = false;
            // if(bufferJumpTimer > 0){
            //     bufferJumpTimer = -1;
            //     Jump();
            // }
            canHaveCoyoteJump = true;
        }
        else{
            if(canHaveCoyoteJump){
                coyoteJumpTimer = coyoteJumpTime;
                canHaveCoyoteJump = false;
            }
        }
        if(canWallSlide){
            isWallSliding=true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.1f);
        }
        if(rb.velocity.y < 0 && !isWallSliding){
            isFalling = true;
            canMove = true;
        }
        Move();
    }
    private void CheckForEnemies(){
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        //Collider2D[] hitColliders = Physics2D.OverlapBox(enemyCheck.position, enemyCheck.transform, 0);
        foreach(var enemy in hitColliders){
            if(enemy.GetComponent<Enemy>() != null){
                Enemy newEnemy = enemy.GetComponent<Enemy>();
                if(newEnemy.invincible){
                    return;
                }
                // This is we only want them to damage when falling.
                // I like damaging while going up if timed right, why not?
                //if(rb.velocity.y < 0){
                // using the isFalling variable outside CheckForEnemies instead.
                newEnemy.Damage();
                isWallSliding = false;
                canWallSlide = false;
                AudioManager.instance.PlaySFX(3);
                Jump();
                //}
            }
        }
    }
    private void AnimationContollers(){
        bool isMoving = rb.velocity.x != 0;
        anim.SetBool("isMoving",isMoving);
        anim.SetBool("isGrounded",isGrounded);
        anim.SetBool("isWallDetected",isWallDetected);
        anim.SetFloat("yVelocity",rb.velocity.y);
        anim.SetBool("isKnocked", isKnocked);
        anim.SetBool("canBeControlled", canBeControlled);
        //anim.SetBool("canWallSlide", canWallSlide);
        anim.SetBool("isWallSliding", isWallSliding);
    }

    private void InputChecks(){
        if(canBeControlled){
            if(pcMode){
                movingInput = Input.GetAxis("Horizontal");

                if(Input.GetAxis("Vertical")< 0){
                    canWallSlide=false;
                }
                if(Input.GetKeyDown(KeyCode.Space)){
                    JumpButton();
                }
            }
            else{
                movingInput = joystick.Horizontal;
                if(joystick.Vertical < 0){
                    canWallSlide=false;
                }
            }
        }
    }
    public void ReturnControl(){
        canBeControlled = true;
        rb.gravityScale = defaultGravityScale;
    }
    public void LoseControl(){
        canWallSlide=false;
        isWallSliding=false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        canMove=false;
        canBeControlled = false;
        anim.SetBool("isDead", true);
    }

    public void JumpButton(){
        // if(isGrounded){
        //     bufferJumpTimer = bufferJumpTime;
        // }
        // This should make wall jumps more immediate.
        if(/*isWallSliding*/ isWallDetected && !isGrounded){
            WallJump();
            AudioManager.instance.PlaySFX(5);
            // if we want to allow double jump after wall jump.
            //canDoubleJump = true;
        }
        else if (isGrounded || coyoteJumpTimer > 0){
        //else if(isGrounded){
            Jump();
            AudioManager.instance.PlaySFX(5);
        }
        else if(canDoubleJump){
            canDoubleJump=false;
            //jumpForce = doubleJumpForce;
            // Disabling Double jump...
            //Jump();
            //jumpForce = defaultJumpForce;
        }
        canWallSlide = false;
    }
    public void Knockback(Transform damageTransform){
        if(canBeKnocked){
            AudioManager.instance.PlaySFX(11);
            #region Define horizontal direction for knockback
            int hDirection = 0;
            if(transform.position.x > damageTransform.position.x){
                hDirection=1;
            }
            else if(transform.position.x < damageTransform.position.x){
                hDirection=-1;
            }
            #endregion
            
            canBeKnocked = false;
            isKnocked = true;
            isWallSliding = false;
            rb.velocity = new Vector2(knockbackDirection.x * hDirection, knockbackDirection.y);
            Invoke("CancelKnockback", knockbackTime);
            Invoke("CanBeKnocked", knockbackTime*1.4f);
            //Hitpoint updates
            currentHitPoints -= 1;
            // Is this the wrong spot for this?
            StatsPresenter.instance.SetHitPoints(currentHitPoints);
            if(currentHitPoints <=0){
                //Invoke("Kill",knockbackTime);
                //anim.SetBool("isKnocked", isKnocked);
                LoseControl();
            }
            else{
            }
        }
    }
    private void CancelKnockback(){
        // Stops them from turning away!
        rb.velocity = new Vector2(0, 0);
        isKnocked = false;
    }
    private void CanBeKnocked(){
        canBeKnocked= true;
    }
    private void Kill(){
        PlayerManager.instance.KillPlayer();
    }
    private void Move(){
        if(canMove && canBeControlled)
        {
            // Use level up speed if necessary.
            float netSpeed = moveSpeed + speedLevel -1;
            if(!isGrounded && (movingInput == 1 || movingInput == -1)){
                // If we are moving faster than max speed, continue moving at max speed.
                if(Math.Abs(rb.velocity.x) > netSpeed){
                    netSpeed = Math.Abs(rb.velocity.x);
                }
            }
            rb.velocity = new Vector2(netSpeed * movingInput, rb.velocity.y);
            /*if(isGrounded || movingInput != 0)
                rb.velocity = new Vector2(netSpeed * movingInput, rb.velocity.y);
            else if(!isGrounded && (movingInput == 1 || movingInput == -1)){
                if(Math.Abs(rb.velocity.x) > netSpeed){
                    netSpeed = Math.Abs(rb.velocity.x);
                }
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
            else
            {
                //Debug.Log(movingInput + " : " + rb.velocity.x);
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }*/
        }
    }

    private void WallJump(){
        canMove = false;

        int xMod = jumpLevel;
        if(jumpLevel < speedLevel)
            xMod = speedLevel;
        rb.velocity = new Vector2((wallJumpDirection.x + (xMod -1)) * -facingDirection, wallJumpDirection.y + (jumpLevel -1));
    }
    public void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, jumpForce + (jumpLevel -1)*2);
    }
    public void Push(float pushForce){
        rb.velocity = new Vector2(rb.velocity.x, pushForce);
    }

    private void FlipController(){
        if(facingRight && rb.velocity.x < 0)
            Flip();
        else if(facingRight && rb.velocity.x == 0 && movingInput<0)
            Flip();
        else if(!facingRight && rb.velocity.x > 0)
            Flip();
        else if(!facingRight && rb.velocity.x == 0 && movingInput > 0)
            Flip();
    }
    private void Flip(){
        transform.Rotate(0,180,0);
        facingRight = !facingRight;
        facingDirection = facingDirection *-1;
    }

    private void CollisionChecks(){
        //isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        bool gd1 = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        bool gd2 = Physics2D.Raycast(groundCheck2.position, Vector2.down, groundCheckDistance, whatIsGround);
        isGrounded =  gd1 || gd2;
        isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, whatIsWall);
        // Comment out the necessity of y < 0. Trying to make it more Mario like. I tried this, didn't like it.
        if (isWallDetected && rb.velocity.y < 0){
            canWallSlide=true;
        }
        if (!isWallDetected){
            canWallSlide=false;
            isWallSliding=false;
        }
    }

    private void OnDrawGizmos(){
        if(groundCheck != null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if(groundCheck2 != null)
            Gizmos.DrawLine(groundCheck2.position, new Vector2(groundCheck2.position.x, groundCheck2.position.y - groundCheckDistance));
        //Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + wallCheckDistance * facingDirection, transform.position.y));
        Gizmos.DrawWireSphere(enemyCheck.position, enemyCheckRadius);
    }
}
