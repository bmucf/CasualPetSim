using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

// ~ Istvan W

/*
Component attached to the pet GameObject
Presentation and Interaction Logic
*/


public class PetStat : MonoBehaviour, IDataPersistence
{
    // Reference outsourced classes
    public Pet pet;
    private GameData gameData = new GameData();

    public DataPersistenceManager dataPersistenceManager;

    private string lastSavedTime;


    void Start()
    {
        if (pet == null)
        {
            pet = GetComponent<Pet>();
            if (pet == null)
            {
                Debug.LogError("No Pet-derived component found on this GameObject!");
            }
        }

        dataPersistenceManager = DataPersistenceManager.instance;

        // LoadPetState();
    }

    void Update()
    {
        if (pet.hungerMain < 100 || pet.dirtinessMain < 100 || pet.sleepinessMain < 100 || pet.sadnessMain < 100)
        {
            pet.UpdateStats(Time.deltaTime);
            // Debug.Log("Stat update called.");
        }

        // ApplyStatEffects();
    }

    public void LoadData(GameData data)
    {
        Debug.Log("PetStat LoadData Called");

        if (pet == null)
        {
            pet = GetComponent<Pet>();
            if (pet == null)
            {
                Debug.LogError("Pet reference is missing.");
                return;
            }
        }

        string petID = pet.UniqueID;

        // Load per-pet time
        if (!data.allPetLastSavedTimes.TryGetValue(petID, out string savedTimeStr))
        {
            Debug.Log($"No saved time found for pet '{petID}'. Using current time.");
            savedTimeStr = DateTime.Now.ToString();
            data.allPetLastSavedTimes[petID] = savedTimeStr;
        }

        if (!DateTime.TryParse(savedTimeStr, out DateTime lastTime))
        {
            Debug.LogWarning("Invalid lastSavedTime. Using current time.");
            lastTime = DateTime.Now;
        }

        TimeSpan elapsed = DateTime.Now - lastTime;

        SimulateOfflineProgress(elapsed.TotalSeconds, data);
    }



    public void SaveData(ref GameData data)
    {
        if (pet == null)
        {
            Debug.LogError("Pet reference is missing. Cannot save lastSavedTime.");
            return;
        }

        if (data == null)
        {
            Debug.LogError("GameData reference is null in PetStat.SaveData!");
            return;
        }

        if (data.allPetLastSavedTimes == null)
        {
            Debug.LogWarning("allPetLastSavedTimes dictionary is null. Initializing...");
            data.allPetLastSavedTimes = new Dictionary<string, string>();
        }

        data.allPetLastSavedTimes[pet.UniqueID] = DateTime.Now.ToString();
    }


    void ApplyStatEffects()
    {
        // Example: clamp values and trigger animations or feedback
        // E.g. If Hunger > 80 trigger sadSound and sadAnimation

        // pet.hunger = Mathf.Clamp(pet.hunger, 0f, 100f);
        // pet.happiness = Mathf.Clamp(pet.happiness, 0f, 100f);

        // TODO: Add visual/audio feedback based on thresholds
    }

    // Load previous pet data
    void LoadPetState()
    {
        if (dataPersistenceManager == null || pet == null)
        {
            Debug.LogError("DataPersistenceManager or Pet reference is missing.");
            return;
        }

        GameData loadedData = dataPersistenceManager.LoadData();
        if (loadedData == null)
        {
            Debug.LogError("Failed to load GameData.");
            return;
        }

        this.gameData = loadedData;

        string petID = pet.UniqueID;

        string savedTimeStr;
        if (!loadedData.allPetLastSavedTimes.TryGetValue(petID, out savedTimeStr))
        {
            // Debug.Log($"No saved time found for pet '{petID}'. Using current time.");
            savedTimeStr = DateTime.Now.ToString();
            loadedData.allPetLastSavedTimes[petID] = savedTimeStr; // Optional: init the value
        }

        if (!DateTime.TryParse(savedTimeStr, out DateTime lastTime))
        {
            Debug.LogWarning("Invalid lastSavedTime. Using current time.");
            lastTime = DateTime.Now;
        }

        TimeSpan elapsed = DateTime.Now - lastTime;

        SimulateOfflineProgress(elapsed.TotalSeconds, loadedData);
    }



    // Save current pet data
    void SavePetState()
    {
        // TODO: Save current stats
        dataPersistenceManager.SaveGame();
    }

    void SimulateOfflineProgress(double secondsPassed, GameData data)
    {

        // Debug.Log("Number of seconds passed: " + secondsPassed);
        // Debug.Log($"{pet.hungerMain}");
        pet.UpdateStats((float)secondsPassed);
        // Debug.Log($"{pet.hungerMain}");

        // TODO

        // Ex: currentHunger = Mathf.Max(0, data.hunger - (float)(secondsPassed * hungerGrowthRate));
    }
}