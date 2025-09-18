using System;
using UnityEngine;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Found more than one Data Persistence Manager in scene");

        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadData();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
        Debug.Log("New game initialized");
    }

    public void SaveGame()
    {
        {
            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(ref gameData);
            }
        }
        // save that data to a file using the data handler
        dataHandler.Save(gameData);

        /*
        string json = JsonUtility.ToJson(gameData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        */
        Debug.Log("Game saved!");
    }

    public void LoadData()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        // if no data can be loaded, initializ to a new gaem
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initialing data to defaults.");
            NewGame();
        }

        /*
        string path = Application.persistentDataPath + "/save.json";
        if (!System.IO.File.Exists(path)) return;

        string json = System.IO.File.ReadAllText(path);
        GameData gameData = JsonUtility.FromJson<GameData>(json);
        */

        // TODO - push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Game loaded!");
    }
   

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
 

}
