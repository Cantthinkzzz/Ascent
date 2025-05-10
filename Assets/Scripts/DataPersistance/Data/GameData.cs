using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public bool unlockedJumping;
    public bool unlockedWallJump;
    public bool unlockedDoubleJump;
    public bool unlockedCircle;
    public bool unlockedDash;
    public Vector3 playerPosition;
    public Vector3 lastCheckpoint;
    public SeriallizableDictionary<string, bool> essenceCollected;
    public SeriallizableDictionary<string, Vector3> boxPositions;
    public int liveOnCount;
    public int fightOnCount;
    public SeriallizableDictionary<string, bool> spiritEssenceCollected;
    public SeriallizableDictionary<string, Vector3> kornjacaPos;
    public SeriallizableDictionary<string, Vector3> kornjacaNextWayPoint;
    public bool slavkoQuestItemPickedUp;
    public bool slavkoHasMetPlayer;
    public bool daoJabuku;
    public bool hogRunning;
    public Vector3 hogPos;
    public int hogWaypoint;
    public string currentMusicName;
    public bool loopRommOver;
    public bool boxInHole;

    public GameData() {
        this.unlockedJumping = false;
        this.unlockedWallJump = false;
        this.unlockedDoubleJump = false;
        this.unlockedCircle = false;
        this.unlockedDash = false;
        playerPosition = new Vector3(-82, 399, 0);
        essenceCollected = new SeriallizableDictionary<string, bool>();
        boxPositions = new SeriallizableDictionary<string, Vector3>();
        playerPosition = new Vector3();
        liveOnCount = 0;
        fightOnCount = 0;
        spiritEssenceCollected = new SeriallizableDictionary<string, bool>();
        kornjacaPos = new SeriallizableDictionary<string, Vector3>();
        kornjacaNextWayPoint = new SeriallizableDictionary<string, Vector3>();
        slavkoQuestItemPickedUp = false;
        slavkoHasMetPlayer = false;
        daoJabuku = false;
        hogRunning = false;
        hogPos = new Vector3();
        hogWaypoint = 0;
        currentMusicName = "";
        loopRommOver = false;
        this.boxInHole = false;
    }
}
