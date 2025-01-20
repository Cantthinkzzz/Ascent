using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    public Vector2 safeCoordinates;
    public Transform respawnPoint;
    public LastSafePoint lastSafePoint;

    private void Start()
    {
        GameObject lastSafePointObject = GameObject.Find("LastSafePoint");
        if (lastSafePointObject != null)
        {
            lastSafePoint = lastSafePointObject.GetComponent<LastSafePoint>();
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Debug.Log("Usao u safe point");
            safeCoordinates = respawnPoint.position;
            safeCoordinates.y += 11;
            lastSafePoint.lastSafePoint = safeCoordinates;
        }
    }
}
