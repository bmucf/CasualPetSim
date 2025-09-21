using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
/*
This script goes on an empty game object within the scene
the category 'File Name' should be named: '{Name}.json'
*/


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
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one Data Persistence Manager in scene");
            Destroy(gameObject);
            return;
        }
            
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        instance = this;

    }

    private void Start()
    {

        LoadData();

        Debug.Log($"Found {dataPersistenceObjects.Count} IDataPersistence objects.");
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

        // Debug.Log("Game saved!");
    }

    public GameData LoadData()
    {
        // load any saved data from a file using the data handler
        if (dataHandler == null)
        {
            Debug.LogError("dataHandler is null before calling Load(). Check initialization in Start().");
        }

        this.gameData = dataHandler?.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initialing data to defaults.");
            NewGame();
        }

        if (dataPersistenceObjects == null)
        {
            Debug.LogError("dataPersistenceObjects list is null before iteration.");
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (dataPersistenceObj == null)
            {
                Debug.LogWarning("Null IDataPersistence object found before LoadData call.");
            }
            else
            {
                try
                {
                    dataPersistenceObj.LoadData(gameData);
                    Debug.Log($"Loaded IDataPersistence: {dataPersistenceObj.GetType().Name}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception during LoadData for {dataPersistenceObj.GetType().Name}: {ex.Message}");
                }
            }
        }

        return gameData;
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return dataPersistences.ToList();
    }
 

}
