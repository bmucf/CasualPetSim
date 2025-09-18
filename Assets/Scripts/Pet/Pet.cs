using System;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

// Handles all the factors a pet can inherit related to pet stats
// Data and Simulation Logic
public abstract class Pet
{
    // Main stats - Starting Values
    public float hunger = 50f;
    public float dirtiness = 50f;
    public float happiness = 50f;
    public float sleepiness = 50f;

    // Default Growth/Decay Rates as virtual properties
    public virtual float hungerGrowthRate => 0.01f;      // Per second v
    public virtual float dirtinessGrowthRate => 0.01f;   //
    public virtual float sleepinessGrowthRate => 0.01f;  //
    public virtual float happinessDecayRate => 0.01f;    //

    // Debug Variables
    [SerializeField]
    private int rateOfChange = 1; // Use to speed up growth/decay rates


    public virtual void UpdateStats(float deltaTime)
    {
        hunger += (deltaTime * hungerGrowthRate) * rateOfChange;            // Increase
        dirtiness += (deltaTime * dirtinessGrowthRate) * rateOfChange;      // Increase
        sleepiness += (deltaTime * sleepinessGrowthRate) * rateOfChange;    // Increase
        happiness -= (deltaTime * happinessDecayRate) * rateOfChange;       // Decrease

        ClampStats();
    }

    protected virtual void ClampStats()
    {   
        // Clamp values between 0 and 100
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        dirtiness = Mathf.Clamp(dirtiness, 0f, 100f);
        sleepiness = Mathf.Clamp(sleepiness, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);
    }

    void SimulateOfflineProgress(double secondsPassed, GameData data)
    {
        // TODO

        // Ex: currentHunger = Mathf.Max(0, data.hunger - (float)(secondsPassed * hungerGrowthRate));
    }
}


