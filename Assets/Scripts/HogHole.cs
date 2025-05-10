using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HogHole : MonoBehaviour, IDataPersistance
{

    public TheHog hog;
    public bool boxInHole = false;

    public void LoadData(GameData data)
    {
        this.boxInHole = data.boxInHole;
    }

    public void SaveData(ref GameData data)
    {
        data.boxInHole = this.boxInHole;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Box") && !boxInHole) {
            hog.RunHog();
            boxInHole = true;
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
