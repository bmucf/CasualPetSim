using System;
using System.Collections;
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

    private string lastSavedTime = "";


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

        LoadPetState();
    }

    void Update()
    {
        if (pet.hungerMain < 100 || pet.dirtinessMain < 100 || pet.sleepinessMain < 100 || pet.sadnessMain < 100)
        {
            pet.UpdateStats(Time.deltaTime);
            Debug.Log("Stat update called.");
        }

        // ApplyStatEffects();
    }

    public void LoadData(GameData data)
    {
        this.lastSavedTime = data.lastSavedTime;
    }

    public void SaveData(ref GameData data)
    {
        data.lastSavedTime = DateTime.Now.ToString();
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
        if (dataPersistenceManager == null)
        {
            Debug.LogError("DataPersistenceManager is not assigned.");
            return;
        }

        // TODO: Load from PlayerPrefs or JSON
        GameData loadedData = dataPersistenceManager.LoadData();

        if (loadedData == null)
        {
            Debug.LogError("Failed to load GameData.");
            return;
        }

        this.gameData = loadedData;
        this.lastSavedTime = loadedData.lastSavedTime;

        // Debug what's inside gameData after loading
        // Debug.Log("=== GameData Debug ===");
        // Debug.Log($"lastSavedTime: {lastSavedTime}");
        // Debug.Log($"CurrentTime: {DateTime.Now}");

        if (!DateTime.TryParse(lastSavedTime, out DateTime lastTime))
        {
            Debug.LogWarning("Invalid lastSavedTime. Using current time.");
            lastTime = DateTime.Now;
        }

        TimeSpan elapsed = DateTime.Now - lastTime;

        SimulateOfflineProgress(elapsed.TotalSeconds, gameData);
    }

    // Save current pet data
    void SavePetState()
    {
        // TODO: Save current stats
        dataPersistenceManager.SaveGame();
    }

    void SimulateOfflineProgress(double secondsPassed, GameData data)
    {
        /*
        Debug.Log("Number of seconds passed: " + secondsPassed);
        Debug.Log($"{pet.hungerMain}");
        pet.UpdateStats((float)secondsPassed);
        Debug.Log($"{pet.hungerMain}");
        */
        // TODO

        // Ex: currentHunger = Mathf.Max(0, data.hunger - (float)(secondsPassed * hungerGrowthRate));
    }
}