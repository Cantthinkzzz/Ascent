using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPosition : MonoBehaviour, IDataPersistance
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {

        if(data.boxPositions.TryGetValue(id, out Vector3 boxPosition)){ 
            transform.position = boxPosition;
        }
    }

    public void SaveData(ref GameData data)
    {
        //Debug.Log("Saving essence with ID: " + id + " | collected = " + collected);
        if (data.boxPositions.ContainsKey(id))
        {
            data.boxPositions[id] = transform.position;
        }
        else {
            data.boxPositions.Add(id, transform.position);
        }
    }
}
