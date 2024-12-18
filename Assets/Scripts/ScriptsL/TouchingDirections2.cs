using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections2 : MonoBehaviour
{
     public Transform groundCheck;
      public float groundCheckRadius = 0.2f; 
    public LayerMask groundLayer;
     public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;  



    
    
    
    
    public ContactFilter2D castFilter;
    Animator animator;
    public float groundDistance  = 0.05f;
    public float wallDistance  = 0.2f;
    public float ceilingDistance  = 0.05f;
    [SerializeField]
    private bool _isGrounded;
    private bool _isOnWall;
    private bool _isOnCeiling;

    private Vector2 WallCheckDirection =>gameObject.transform.localScale.x>0 ? Vector2.right: Vector2.left; 
    Rigidbody2D rb;

    public bool IsGrounded { get{
        return _isGrounded;
    } private set{
        _isGrounded=value;
        animator.SetBool(AnimationStrings.isGrounded, value);


    } }
     public bool IsOnWall { get{
        return _isOnWall;
    } private set{
        _isOnWall=value;
        animator.SetBool(AnimationStrings.isOnWall, value);


    } }
     public bool IsOnCeiling { get{
        return _isOnCeiling;
    } private set{
        _isOnCeiling=value;
        animator.SetBool(AnimationStrings.isOnCeiling, value);


    } }


    private void Awake(){
        rb=GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate() {
               IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
               IsOnWall = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);
    }
}
