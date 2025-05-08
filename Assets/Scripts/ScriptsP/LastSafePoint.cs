using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSafePoint : MonoBehaviour, IDataPersistance
{
    public Vector2 lastSafePoint;

    public void LoadData(GameData data)
    {
        this.lastSafePoint = data.lastCheckpoint;
        //Debug.Log("Zadnji checkpoint je: " + lastSafePoint);
    }

    public void SaveData(ref GameData data)
    {
        data.lastCheckpoint = this.lastSafePoint;
    }
}
