using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IDataPersistance
{

    [SerializeField] public string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

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

    public void LoadData(GameData data)
    {

        if (data.kornjacaPos.TryGetValue(id, out Vector3 savedPos))
        {
            transform.position = savedPos;
        }

        if (data.kornjacaNextWayPoint.TryGetValue(id, out Vector3 savedWaypoint))
        {
            // Postavi najbliži waypoint kao `nextWaypoint`
            nextWaypoint = GetClosestWaypoint(savedWaypoint);
            waypointNumber = waypoints.IndexOf(nextWaypoint);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.kornjacaPos.ContainsKey(id))
            data.kornjacaPos[id] = transform.position;
        else
            data.kornjacaPos.Add(id, transform.position);

        if (data.kornjacaNextWayPoint.ContainsKey(id))
            data.kornjacaNextWayPoint[id] = nextWaypoint.position;
        else
            data.kornjacaNextWayPoint.Add(id, nextWaypoint.position);
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
    private Transform GetClosestWaypoint(Vector3 target)
    {
        Transform closest = waypoints[0];
        float minDistance = Vector3.Distance(target, closest.position);

        foreach (Transform wp in waypoints)
        {
            float dist = Vector3.Distance(target, wp.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = wp;
            }
        }

        return closest;
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
