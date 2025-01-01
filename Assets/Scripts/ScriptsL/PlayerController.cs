using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    TouchingDirections2 touchingDirections;

    [Header("Jump and fall")]
    public float jumpImpulse = 10f;   //strength of the jump
    public float baseGravity = 1f;    //base gravity scale
    public float maxFallSpeed = 12f;  //maximum allowed speed for falling
    public float fallSpeedMultiplier = 2f;  //change in gravity while falling

    public float jumpHangVelocity = 0.15f;  //influences how long the gravity scale will be lowered while at the peak of the player jump
    public float jumpHangGravity = 0.2f; //lowers gravity at the peak of a jump
    public float coyoteTime = 0.5f;
    public float jumpBufferTime = 0.5f;

    [Header("Movement")]
    public float moveSpeed = 10f;       //walk speed
    public float runSpeed = 15f;        //run speed
    public float accelRate = 0.1f;           //brzina ubrzavanja u istom smjeru, npr između hodanja i trčanja
    public float deaccelRate = 0.1f;        //brzina usporavanja u istom smjeru, npr između trčanja i hodanja

    public float turnDeaccelRate = 1f;      //brzina okreta na podu

    public float airTurnDeaccelRate = 1f;   //brzina okreta u zraku

    public float airDeaccelRate = 0.5f;    //brzina usporavanja u zraku npr između trčanja i skoka

    public float airAccelRate = 4f;        //brzina ubrzavanja u zraku, npr između mirovanja i skoka

    public float maxAirSpeed = 15f;        //tu se konvergira brzina u zraku



    [Header("Dash")]
    public float dashForce = 20f;       //strength of dash
    public float dashDuration = 0.5f;   //duration of coroutine
   

    [Header("Wall jump")]
    
     public float wallSlideSpeed = 2f;
     public float wallJumpForce = 10f;   //strength of walljump
    public Vector2 wallJumpVector = new Vector2(5f, 10f);
    public float wallJumpTime = 0.5f;
    private float walljumptimer;
    private float wallJumpDirection;

     [Header("Status")]

    private bool isDashing = false;      //traje li dash
    [SerializeField]
    private bool isJumping = false;      //traje li jump
    private bool isWallJumping = false;
    public bool unlockedJumping = false;
    public bool unlockedDash = false;
    public bool unlockedDoubleJump = false;
    public bool unlockedWallJump = false;

    [SerializeField]
    private bool usedDoubleJump = false;
    [SerializeField]
    private bool usedAirDash = false;
    private bool _isFacingRight = true;
    [SerializeField]
    private float coyotetimer;
    private float jumpBuffertimer;
    //private float bufferedJumpDuration;
    //private float bufferedJumpStart;
    public bool CanMove { 
        get {
            return animator.GetBool(AnimationStrings.canMove);

        } 
    }
    
    private bool _isMoving = false;
    private bool _isRunning = false;
    [SerializeField]
    private bool isWallSliding = false;
    Vector2 moveInput;
    private bool jumpInitiated = false;
   


   




    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        touchingDirections=GetComponent<TouchingDirections2>();
        jumpBuffertimer= -1f;
    }
    private void FixedUpdate() {
        
        jumpParams();
        if(!isDashing && !isWallJumping) //rb.AddForce(calculateForce()* Vector2.right); 
        rb.velocity = new Vector2(fixedSpeed(), rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

    }

    private float fixedSpeed() {
        
    float targetSpeed = moveInput.x * currentMoveSpeed; // Desired speed (can be positive or negative)
    float speedDifference = targetSpeed - rb.velocity.x; // Difference between current speed and desired speed
    float activeAcc;
    if (touchingDirections.IsGrounded) {
        // On ground: use normal accel/decel rates
        activeAcc = (Mathf.Abs(targetSpeed) > Mathf.Abs(rb.velocity.x)) ? accelRate : deaccelRate;
    } else {
        // Midair: adjust for snappier turning
        if (Mathf.Sign(targetSpeed) != Mathf.Sign(rb.velocity.x) && targetSpeed != 0) {
            // If turning midair, use a boosted turn rate
            activeAcc = airTurnDeaccelRate; // Use a higher deceleration rate for turning
        } else if(Mathf.Abs(targetSpeed)<Mathf.Abs(rb.velocity.x)) {
            //dok se usporavamo u zraku, a da ne mijenjamo smjer
            activeAcc = airDeaccelRate;
        } 
        else {
            // dok se ubrzavamo u zraku
            activeAcc = airAccelRate;
        }
    }
    // Apply acceleration
    float movement = Mathf.Sign(speedDifference) * activeAcc; // Move in the correct direction

    // Clamp movement to ensure we don't overshoot target speed
    if (Mathf.Abs(movement) > Mathf.Abs(speedDifference))
        movement = speedDifference; // Prevent overshooting

    // Return the new velocity
    return rb.velocity.x + movement;
    
    }




     public bool IsMoving { 
    get{
        return _isMoving;
    } private set {
        _isMoving=value;
        animator.SetBool(AnimationStrings.isMoving, value);
    } 
    }

    public void jumpParams() {
        
        if(touchingDirections.IsGrounded) {
            usedDoubleJump = false;
        }
        
        
        if(touchingDirections.IsOnWall && !touchingDirections.IsGrounded && unlockedWallJump && moveInput.x!= 0) {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -wallSlideSpeed));
            isWallSliding = true;
        }
        else {
            
            
        
        isWallSliding = false;
        if(touchingDirections.IsGrounded) {
            if(!jumpInitiated)isJumping = false;
            isWallSliding=false;
            usedAirDash = false;
            rb.gravityScale = baseGravity;
            coyotetimer= coyoteTime;

            if(jumpBuffertimer > 0) {
                bufferedJump();
                jumpBuffertimer=-1f;
            }



        }
        else {
            jumpInitiated=false;
            isWallSliding = false;
            coyotetimer-= Time.deltaTime;
            jumpBuffertimer-= Time.deltaTime;
            if(isJumping) {
                if(Mathf.Abs(rb.velocity.y) < jumpHangVelocity) {
                    rb.gravityScale=jumpHangGravity * baseGravity;
                }
                
            } 
             if(rb.velocity.y < -1* jumpHangVelocity) {
                    rb.gravityScale= baseGravity*fallSpeedMultiplier;
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
                }
        }
        }
    }


    public float currentMoveSpeed {
        get{
           if(CanMove) {
             if(IsMoving && !touchingDirections.IsOnWall) {
              if(touchingDirections.IsGrounded) {
                    if(IsRunning) {
                        return runSpeed;
                    }
                    else return moveSpeed;
              }
              else return maxAirSpeed;
                
            
            }
            
           
            return 0; //idleSpeed is 0

           }
           else {
            return 0; //Movement locked
           }
           
        }
        
    }
     public bool IsRunning {
        get{
            return _isRunning;
        }
        private set {
            _isRunning=value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }
    public void onMove(InputAction.CallbackContext context) {

        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetMovingDirection(moveInput);
    }

    private void SetMovingDirection(Vector2 moveInput)
    {
        if(moveInput.x>0 &&!IsFacingRight) {
            //face the right
            IsFacingRight=true;
        }
        else if(moveInput.x<0 && IsFacingRight) {
            //face the right
            IsFacingRight=false;
        }
    }

    public bool IsFacingRight { 
        get {
            return _isFacingRight;
            } 
        private set
            {
            if(_isFacingRight!= value) {
                transform.localScale = new Vector3(-1* transform.localScale.x,transform.localScale.y,transform.localScale.z);
            }


            _isFacingRight=value;
            } 
    }



    //called upon input action detecting a space press
     public void onJump(InputAction.CallbackContext context) {
        if(unlockedJumping) {
        if (CanMove) {
            
            if(!touchingDirections.IsGrounded && (usedDoubleJump || !unlockedDoubleJump) && !isWallSliding) {
                if(context.started) {
                    jumpBuffertimer= jumpBufferTime;              //ako je timer jos pozitivan nakon sto se uzemlji onda se svejedno obavi skok
                    //bufferedJumpStart= Time.time;



                }
                //bufferedJumpDuration = Time.time - bufferedJumpStart;

            }


            if(context.performed && isWallSliding) {
                    wallJump();
                }
            else {

            if(touchingDirections.IsGrounded                                         //može skočiti kad je uzemljen
             || (!touchingDirections.IsGrounded && !usedDoubleJump && unlockedDoubleJump && !isWallSliding)                 //ili kad nije iskoristio double jump
             || (!touchingDirections.IsGrounded && coyotetimer>0 && !isWallSliding)) {                 //ili ako nije prosao coyoteTime otkad je pao s platforme
                     
                     rb.gravityScale= baseGravity;                                   //resetira gravitaciju(važno za double jump u slučaju da je počeo dok je gravitacija bila smanjena)
                     animator.SetTrigger(AnimationStrings.jumpTrigger);
                     isJumping=true; //pokreće jump animaciju
                     
                     if(context.performed) {
                        isJumping = true;
                        jumpInitiated=true;
                        rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
                        if(!touchingDirections.IsGrounded && coyotetimer<0) usedDoubleJump=true;  
                     }
                     
                     

                }
            }
                if(context.canceled && rb.velocity.y>0f) {                              //kad se spusti tipka za skakanje dok se igrač još uzdiže
                        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
                }

        }
        }
        else {
            Debug.Log("Cannot jump yet");
        }
    }

     void bufferedJump() {
         rb.gravityScale= baseGravity;
        isJumping= true;
         Debug.Log("BUFFERING");
        rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
    }

    void wallJump() {
          if (isWallSliding && CanMove) {
        wallJumpDirection = -transform.localScale.x; // Opposite of current facing direction
        rb.velocity = new Vector2(wallJumpDirection * wallJumpVector.x, wallJumpVector.y); // Apply wall jump force
        isWallJumping = true; // Set wall jump state
        isJumping = false; // Disable normal jumping state
        isDashing = false; // Disable dashing
        jumpInitiated = true; // Prevent grounded logic during this frame

        Debug.Log("Wall jump performed!");

        // Handle facing direction
        if (Mathf.Sign(transform.localScale.x) != Mathf.Sign(wallJumpDirection)) {
            IsFacingRight = !IsFacingRight; // Flip direction
        }

        walljumptimer = 0; // Reset wall jump timer
        Invoke(nameof(CancelWallJump), wallJumpTime); // Schedule cancel of wall jump
    } else {
        walljumptimer -= Time.deltaTime; // Decrease wall jump timer if not sliding
    }
    }

    private void CancelWallJump() {
        isWallJumping=false;
        jumpInitiated = false; // Allow grounded logic again
    }



    

    public void onRun(InputAction.CallbackContext context) {
        //if(touchingDirections.IsGrounded) {
            if(context.started) {
            IsRunning=true;
        }
        else if(context.canceled) {
            IsRunning=false;
        }
        //}
        
    }
    public void OnDash(InputAction.CallbackContext context) {
             Debug.Log("Dashing through the snow");
             if(context.started && unlockedDash)StartCoroutine("Dash");
    }

    public IEnumerator Dash() {
        
        Vector2 dashDirection = IsFacingRight ? Vector2.right : Vector2.left;
        Debug.Log(dashDirection.x);
        if(unlockedDash && CanMove) {
            //canDashInAir se postavlja na true prelaskom iz grounded stanja u air state
            Debug.Log("on a one horse open sleigh");
            if(touchingDirections.IsGrounded || (!touchingDirections.IsGrounded && !usedAirDash)) {
                animator.SetBool(AnimationStrings.isDashing, true);
                bool keepJump = isJumping;
                isJumping= false;
                Debug.Log("Dashing with force: " + dashForce);
                Debug.Log("Dash direction: " + dashDirection);
                isDashing= true;
                rb.velocity = new Vector2(dashDirection.x * dashForce, 0);
                //canMove ce se staviti na true kad lik ude u stanje za dash,vratiti ce se na true nakon sto izade iz stanja(za to se brine animator)
                Debug.Log("New Rigidbody velocity: " + rb.velocity);
                rb.gravityScale = 0;
                yield return new WaitForSeconds(dashDuration);
                rb.gravityScale = baseGravity;
                animator.SetBool(AnimationStrings.isDashing, false);
                isDashing=false;
                isJumping=keepJump;
                
            }
            if(!touchingDirections.IsGrounded && !usedAirDash) usedAirDash = true;
            //kad se dash izvede u zraku ne moze se iz zraka izvesti dok se opet ne vrati u zračno stanje iz 
        }
        else {
            Debug.Log("Cannot dash yet");
        }


    }





    
}
