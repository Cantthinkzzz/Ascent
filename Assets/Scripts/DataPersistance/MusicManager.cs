using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour, IDataPersistance
{
    public AudioSource bgm1;
    public AudioSource bgm2;

    private string currentMusicName = "";

    public void SaveData(ref GameData data)
    {
        if (bgm1.volume > 0.5f)
        {
            data.currentMusicName = bgm1.clip.name;
        }
        else if (bgm2.volume > 0.5f)
        {
            data.currentMusicName = bgm2.clip.name;
        }
        Debug.Log("Spremljena muzika je: " + data.currentMusicName);
    }

    public void LoadData(GameData data)
    {
        if (data.currentMusicName == bgm1.clip.name)
        {
            bgm1.volume = 1f;
            bgm2.volume = 0f;
            bgm1.Play();
        }
        else if (data.currentMusicName == bgm2.clip.name)
        {
            bgm2.volume = 1f;
            bgm1.volume = 0f;
            bgm2.Play();
        }
    }
}
