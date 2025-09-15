using System;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

// Handles all the factors a pet can inherit related to pet stats
// Data and Simulation Logic
public class Pet
{
    // Main stats - Starting Values
    public float hunger = 50f;
    public float dirtiness = 50f;
    public float happiness = 50f;
    public float sleepiness = 50f;

    // Default Growth/Decay Rates
    public float hungerGrowthRate = 0.01f;      // Per second v
    public float dirtinessGrowthRate = 0.01f;   //
    public float sleepinessGrowthRate = 0.01f;  //
    public float happinessDecayRate = 0.01f;    //

    // Debug Variables
    [SerializeField]
    private int rateOfChange = 1; // Use to speed up growth/decay rates


    public virtual void UpdateStats(float deltaTime)
    {
        hunger += (deltaTime * hungerGrowthRate) * rateOfChange;            // Increase
        dirtiness += (deltaTime * dirtinessGrowthRate) * rateOfChange;      // Increase
        sleepiness += (deltaTime * sleepinessGrowthRate) * rateOfChange;    // Increase
        happiness -= (deltaTime * happinessDecayRate) * rateOfChange;       // Decrease

        // Clamp values between 0 and 100
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        dirtiness = Mathf.Clamp(dirtiness, 0f, 100f);
        sleepiness = Mathf.Clamp(sleepiness, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
    }
}

[System.Serializable]
public class SaveData
{
    private Pet pet = new Pet(); // Reference Pet class

    // What is being saved
    // Ex: Current Stats, Clothes, Equipment, Currency

     public string lastSavedTime;
    public void SaveGame()
    {
        SaveData data = new SaveData
        {
            lastSavedTime = DateTime.Now.ToString() // ISO 8601 format

            // TODO : Save the values of things being saved here: (Don't forget to add commas after each entry
        };

        string json = JsonUtility.ToJson(data);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/save.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (!System.IO.File.Exists(path)) return;

        string json = System.IO.File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        DateTime lastTime = DateTime.Parse(data.lastSavedTime);
        TimeSpan elapsed = DateTime.Now - lastTime;

        SimulateOfflineProgress(elapsed.TotalSeconds, data);
    }

    void SimulateOfflineProgress(double secondsPassed, SaveData data)
    {
        // TODO

        // Ex: currentHunger = Mathf.Max(0, data.hunger - (float)(secondsPassed * hungerGrowthRate));
    }
}
