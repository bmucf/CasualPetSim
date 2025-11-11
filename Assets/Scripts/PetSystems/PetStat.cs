using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static Pet;

// Istvan Wallace

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
    }

    void Update()
    {
        {
            if (pet.hungerMain < 100 || pet.dirtinessMain < 100 || pet.sleepinessMain < 100 || pet.sadnessMain < 100)
            {
                pet.UpdateStats(Time.deltaTime);
                // Debug.Log("Stat update called.");
            }
            if (pet.hungerMain > 100 || pet.hungerMain < 100)
                pet.ClampStat(StatType.Hunger);
            if (pet.dirtinessMain > 100 || pet.dirtinessMain < 100)
                pet.ClampStat(StatType.Dirtiness);
            if (pet.sleepinessMain > 100 || pet.sleepinessMain < 100)
                pet.ClampStat(StatType.Sleepiness);
            if (pet.sadnessMain > 100 || pet.sadnessMain < 100)
                pet.ClampStat(StatType.Sadness);
        }
        // VFX
        {
            // If pet stats are low play death particle
            if (pet.hungerMain > 90 && pet.dirtinessMain > 90 && pet.sleepinessMain > 90 && pet.sadnessMain > 90)
                pet.vfx.TryPlayEffect("Death", pet);
            // If pet sadness is 50 or above play angry particle effect
            if (pet.sadnessMain > 50)
                pet.vfx.TryPlayEffect("Angry", pet);
            // If pet sadness and hunger are lower than 40 play happy particle effect
            if (pet.sadnessMain <= 40 && pet.hungerMain <= 40)
                pet.vfx.TryPlayEffect("Happy", pet);

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