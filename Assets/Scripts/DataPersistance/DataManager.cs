using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    public static DataManager instance { get; private set; }

    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FIleDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null) {
            Debug.Log("Vec ima data manager");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FIleDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        loadGame();
    }

    public void newGame() {
        this.gameData = new GameData();
    }

    public void loadGame() {

        this.gameData = dataHandler.Load();

        if (this.gameData == null) {
            newGame();
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects) {
            dataPersistanceObj.LoadData(gameData);
        }

        //Debug.Log("Ucitao sam za skakanje " + gameData.unlockedJumping);
    }

    public void saveGame() {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }
        //Debug.Log("Spremio sam za skakanje " + gameData.unlockedJumping);

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() {
        saveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects() {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
