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

//TODO - Problems in LoadData()

/*
NullReferenceException: Object reference not set to an instance of an object
DataPersistenceManager.LoadData () (at Assets/Scripts/Pet/DataPersistence/DataPersistenceManager.cs:72)
PetStat.LoadPetState()(at Assets / Scripts / Pet / PetStat.cs:68)
PetStat +< Start > d__4.MoveNext()(at Assets / Scripts / Pet / PetStat.cs:29)
UnityEngine.SetupCoroutine.InvokeMoveNext(System.Collections.IEnumerator enumerator, System.IntPtr returnValueAddress)(at < 98fbc9de20ae47d9bb2559ab79ec6643 >:0)
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
            

        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
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

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
            if (dataPersistenceObj == null)
                Debug.LogWarning("Null IDataPersistence object found.");
            else
                Debug.Log($"Loaded IDataPersistence: {dataPersistenceObj.GetType().Name}");

        }

        //Debug.Log("Game loaded!");
    }
   

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return dataPersistences.ToList();
    }
 

}
