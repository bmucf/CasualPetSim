using System;
using UnityEngine;

// Component attached to the pet GameObject
// Presentation and Interaction Logic
public class PetStat : MonoBehaviour, IDataPersistence
{
    // Reference outsourced classes
    // private Pet pet = new Pet();
    private GameData gameData = new GameData();
    private DataPersistenceManager dataPersistenceManager = new DataPersistenceManager();

    private string lastSavedTime = "";

    void Start()
    {
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
        data.lastSavedTime = this.lastSavedTime;
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
        // TODO: Load from PlayerPrefs or JSON
        dataPersistenceManager.LoadData();


        DateTime lastTime = DateTime.Parse(gameData.lastSavedTime);
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
