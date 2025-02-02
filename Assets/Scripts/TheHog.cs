using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TheHog : MonoBehaviour
{
    public Animator animator;
    public GameObject theHog;
    public List<Transform> waypoints;
    public float waypointReachedDistance = 0.01f;
    private Transform nextWaypoint;
    public Transform finalDestination;
    public Transform essenceLocation;
    public GameObject essence;
    public Rigidbody2D rb;
    public PlayerInput playerInput;
    public AudioClip jumpClip;
    public AudioSource audioSource;
    
    private int waypointNumber = 0;

    private bool running=false;
    public float sonicSpeed=12f;
    public void RunHog() {
        animator.SetBool("running", true);
        //theHog.transform.localScale= new Vector3(theHog.transform.localScale.x*-1, theHog.transform.localScale.y, theHog.transform.localScale.y);
        theHog.GetComponent<SpriteRenderer>().flipX = true;
        running=true;
        rb.velocity=Vector2.zero;
        rb.isKinematic=true;
        playerInput.enabled=false;


        
    }
    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint= waypoints[waypointNumber];
    }
    void FixedUpdate() {
        if(running) {
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, sonicSpeed * Time.deltaTime);
            float distance = Vector2.Distance(nextWaypoint.position, transform.position);
            if(distance <= waypointReachedDistance) {
                waypointNumber++;
                if(waypointNumber==4) {
                    if(audioSource!= null) {
                        audioSource.PlayOneShot(jumpClip);
                    }
                }
                if(waypointNumber >= waypoints.Count) {
                    transform.position=finalDestination.position;
                    essence.transform.position= essenceLocation.position;
                    animator.SetBool("running", false);
                    rb.isKinematic=false;
                    playerInput.enabled=true;
                    running=false;
                    theHog.GetComponent<SpriteRenderer>().flipX = false;

                }
                else nextWaypoint= waypoints[waypointNumber];
        }
        } else Debug.Log("stopped running");
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            animator.SetBool("curious", true);
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            animator.SetBool("curious", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
