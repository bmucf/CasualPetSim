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
    private int rateOfChange = 1; // Use to speed up growth/decay rates


    public virtual void UpdateStats(float deltaTime)
    {
        if (hunger < 0)
            hunger += (deltaTime * hungerGrowthRate) * rateOfChange;            // Increase

        if (dirtiness < 0)
            dirtiness += (deltaTime * dirtinessGrowthRate) * rateOfChange;      // Increase

        if (sleepiness < 0)
            sleepiness += (deltaTime * sleepinessGrowthRate) * rateOfChange;    // Increase

        if (happiness > 0)
            happiness -= (deltaTime * happinessDecayRate) * rateOfChange;       // Decrease

        ClampStats(ref hunger, ref dirtiness, ref sleepiness, ref happiness);
    }

    protected virtual void ClampStats(ref float hunger, ref float dirtiness, ref float sleepiness, ref float happiness)
    {   
        // Clamp values between 0 and 100
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        dirtiness = Mathf.Clamp(dirtiness, 0f, 100f);
        sleepiness = Mathf.Clamp(sleepiness, 0f, 100f);
        happiness = Mathf.Clamp(happiness, 0f, 100f);

        return;
    }

}


