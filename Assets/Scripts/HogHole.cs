using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HogHole : MonoBehaviour
{

    public TheHog hog;


    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Box")) {
            hog.RunHog();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
