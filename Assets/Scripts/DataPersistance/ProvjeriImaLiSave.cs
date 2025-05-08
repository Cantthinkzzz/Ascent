using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProvjeriImaLiSave : MonoBehaviour
{ 
    public GameObject continueButton;

    private void Start()
    {
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path))
        {
            continueButton.SetActive(true);
        }
        else {
            continueButton.SetActive(false);
        }
    }
}
