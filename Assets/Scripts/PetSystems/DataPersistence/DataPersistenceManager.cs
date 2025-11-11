using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// Istvan Wallace


//
///This script goes on an empty game object within the scene
///the category 'File Name' should be named: '{Name}.json'
//

// Note: When pet stats are individually updated they are not updated till next load currently


public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData data;
    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Found more than one Data Persistence Manager in scene");
            Destroy(gameObject);
            // return;
        }
        instance = this;

        if (string.IsNullOrEmpty(fileName))
        {
            // Debug.LogWarning("File name is empty!");
            fileName = "Test.json";
        }
        else
        {
            // Debug.Log($"The file name is '{fileName}'");
        }

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        data = LoadData() ?? new GameData();
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.data = new GameData();
        Debug.Log("New game initialized");
    }

    public void SaveGame()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // Debug.Log($"Found {dataPersistenceObjects.Count} IDataPersistence objects on quit.");
        // Debug.Log($"Saving info to '{fileName}'.");

        // Debug.Log($"Saving game, GameData is null? {data == null}");

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref data);
        }
        // save that data to a file using the data handler
        dataHandler.Save(data);

        // Debug.Log("Game saved!");
    }

    public GameData LoadData()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        // load any saved data from a file using the data handler
        if (dataHandler == null)
        {
            Debug.LogError("dataHandler is null before calling Load(). Check initialization in Start().");
        }

        this.data = dataHandler?.Load();

        if (this.data == null)
        {
            // Debug.LogWarning("No data was found. Initialing data to defaults.");
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
                    dataPersistenceObj.LoadData(data);
                    // Debug.Log($"Loaded IDataPersistence: {dataPersistenceObj.GetType().Name}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Exception during LoadData for {dataPersistenceObj.GetType().Name}: {ex.Message}");
                }
            }
        }
        // Debug.Log($"Found {dataPersistenceObjects.Count} IDataPersistence objects on load.");

        return data;
    }


    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return dataPersistences.ToList();
    }

    public GameData GetGameData()
    {
        return this.data;
    }

    public void UpdatePetStat(string petID, Action<PetStatsData> updateAction)
    {
        if (data.allPetStats.TryGetValue(petID, out var stats))
        {
            updateAction(stats);
            dataHandler.Save(data); // immediately persist
        }
        else
        {
            Debug.LogWarning($"No stats found for pet {petID}");
        }
    }
}
