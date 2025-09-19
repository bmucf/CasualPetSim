using System;
using System.Collections;
using UnityEngine;

// Component attached to the pet GameObject
// Presentation and Interaction Logic

public class PetStat : MonoBehaviour, IDataPersistence
{
    // Reference outsourced classes
    // private Pet pet = new Pet();
    private GameData gameData = new GameData();

    public DataPersistenceManager dataPersistenceManager;

    private string lastSavedTime = "";

    void Awake( ) 
    {
        dataPersistenceManager = DataPersistenceManager.instance;
    }
    IEnumerator Start()
    {
        while (DataPersistenceManager.instance == null)
            yield return null;

        dataPersistenceManager = DataPersistenceManager.instance;

        LoadPetState();
    }


    void Update()
    {
        // pet.UpdateStats(Time.deltaTime);
        ApplyStatEffects();
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
        dataPersistenceManager.LoadData();


        // Debug what's inside gameData after loading
        Debug.Log("=== GameData Debug ===");
        Debug.Log($"lastSavedTime: {lastSavedTime}");

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
        Debug.Log("Number of seconds passed: " + secondsPassed);
        // TODO

        // Ex: currentHunger = Mathf.Max(0, data.hunger - (float)(secondsPassed * hungerGrowthRate));
    }
}
