using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;
using static GameData;

// ~ Istvan W

// Handles all the factors a pet can inherit related to pet stats
// Data and Simulation Logic

public abstract class Pet : MonoBehaviour, IDataPersistence
{
    public DataPersistenceManager dataPersistenceManager;

    public void LoadData(GameData data)
    {
        
        if (string.IsNullOrEmpty(uniqueID))
        {
            Debug.LogError("Pet uniqueID is empty! Cannot load.");
            return;
        }

        if (data.allPetStats.TryGetValue(uniqueID, out PetStatsData stats))
        {
            petName = stats.petName;
            typeName = stats.typeName;

            hungerMain = stats.hungerMain;
            dirtinessMain = stats.dirtinessMain;
            sadnessMain = stats.sadnessMain;
            sleepinessMain = stats.sleepinessMain;

            traitNames = stats.traitNames;
        }
        else
        {
            Debug.LogWarning($"No save data found for pet with ID: {uniqueID}");
        }
    }

    public void SaveData(ref GameData data)
    {
        if (string.IsNullOrEmpty(uniqueID))
        {
            Debug.LogError("Pet uniqueID is empty! Cannot save.");
            return;
        }

        if (data == null)
        {
            Debug.LogError("GameData is null in Pet.SaveData!");
            return;
        }

        if (data.allPetStats == null)
        {
            Debug.LogWarning("allPetStats dictionary is null, initializing...");
            data.allPetStats = new Dictionary<string, PetStatsData>();
        }

        // Add unique ID to list
        data.AddPetID(uniqueID);

        PetStatsData stats = new()
        {
            petName = petName,
            typeName = typeName,

            hungerMain = hungerMain,
            dirtinessMain = dirtinessMain,
            sadnessMain = sadnessMain,
            sleepinessMain = sleepinessMain,

            traitNames = traitNames
        };

        data.allPetStats[uniqueID] = stats;
    }




    [SerializeField] private string uniqueID;
    public string UniqueID => uniqueID;

    public virtual string petName { get; set; }

    public string typeName;

    public List<TraitDefinition> traitList;
    [HideInInspector] 
    public List<string> traitNames;


    // Main stats - Starting Values
    public float hungerMain = 50f;
    public float dirtinessMain = 50f;
    public float sadnessMain = 50f;
    public float sleepinessMain = 50f;

    // Sub-system will be averaged out to get the main stats value. Sub systems will not be clamped

    // Sub-system stats - Starting Values
    /*
    [Header("Hunger Systems")]
    private float fullness = 100f;
    private float hydration = 100f;

    [Header("Sleep Systems")]
    private float sleepiness = 0f;

    [Header("Happiness Systems")]
    private float playfulness = 100f;
    private float excercise = 100f;
    private float attention = 100f;

    [Header("Dirtiness Systems")]
    private float dirtinesss = 0f;
    */

    // Default Growth/Decay Rates as virtual properties
    public float hungerGrowthRate => 0.01f;      // Per second v
    public float dirtinessGrowthRate => 0.01f;   //
    public float sleepinessGrowthRate => 0.01f;  //
    public float sadnessGrowthRate => 0.01f;    //

    // Debug Variables
    private int rateOfChange = 100; // Use to speed up growth/decay rates


    public void UpdateStats(float time)
    {
        if (hungerMain < 100)
            hungerMain += (time * hungerGrowthRate) * rateOfChange;            // Increase

        if (dirtinessMain < 100)
            dirtinessMain += (time * dirtinessGrowthRate) * rateOfChange;      // Increase

        if (sleepinessMain < 100)
            sleepinessMain += (time * sleepinessGrowthRate) * rateOfChange;    // Increase

        if (sadnessMain < 100)
            sadnessMain += (time * sadnessGrowthRate) * rateOfChange;       // Increase

        ClampStats(ref hungerMain, ref dirtinessMain, ref sleepinessMain, ref sadnessMain);

    }

    void ClampStats(ref float hunger, ref float dirtiness, ref float sleepiness, ref float sadness)
    {   
        // Clamp values between 0 and 100
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        dirtiness = Mathf.Clamp(dirtiness, 0f, 100f);
        sleepiness = Mathf.Clamp(sleepiness, 0f, 100f);
        sadness = Mathf.Clamp(sadness, 0f, 100f);

        return;
    }

    public void Initialize(string typeName, string petName = null)
    {
        this.typeName = typeName;
        if (!string.IsNullOrEmpty(petName))
            this.petName = petName;

        if (string.IsNullOrEmpty(uniqueID))
            uniqueID = Guid.NewGuid().ToString();
    }

    public void SetUniqueID(string ID)
    {
        if (!string.IsNullOrEmpty(ID))
        {
            uniqueID = ID;
        }
        else
        {
            uniqueID = Guid.NewGuid().ToString();
            Debug.Log("No ID found. Generating new ID.");
        }
    }

}


