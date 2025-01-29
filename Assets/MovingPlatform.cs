using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public List<Transform> waypoints;
    public float moveSpeed=2f;
    public float waypointReachedDistance = 0.01f;

    public bool stickyPlatform = true;

    private Transform nextWaypoint;
    private int waypointNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint= waypoints[waypointNumber];
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, moveSpeed * Time.deltaTime);
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);
        if(distance <= waypointReachedDistance) {
            waypointNumber++;
            if(waypointNumber >= waypoints.Count) {
                waypointNumber=0;
            }
            nextWaypoint= waypoints[waypointNumber];
        }
    }


    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player") && stickyPlatform) {
            collision.gameObject.transform.parent= transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            collision.gameObject.transform.parent= null;
        }
    }
}
