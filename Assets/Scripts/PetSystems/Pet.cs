using System;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

// ~ Istvan W

// Handles all the factors a pet can inherit related to pet stats
// Data and Simulation Logic

public abstract class Pet
{
    public string TypeName { get; private set; }

    // Main stats - Starting Values
    public float hungerMain = 50f;
    public float dirtinessMain = 50f;
    public float happinessMain = 50f;
    public float sleepinessMain = 50f;

    // Sub-system will be averaged out to get the main stats value. Sub systems will not be clamped

    // Sub-system stats - Starting Values
    [Header("Hunger Systems")]
    public float fullness = 100f;
    public float hydration = 100f;

    [Header("Sleep Systems")]
    public float sleepiness = 0f;

    [Header("Happiness Systems")]
    public float playfulness = 100f;
    public float excercise = 100f;
    public float attention = 100f;

    [Header("Dirtiness Systems")]
    public float dirtinesss = 0f;

    // Default Growth/Decay Rates as virtual properties
    public virtual float hungerGrowthRate => 0.01f;      // Per second v
    public virtual float dirtinessGrowthRate => 0.01f;   //
    public virtual float sleepinessGrowthRate => 0.01f;  //
    public virtual float happinessDecayRate => 0.01f;    //

    // Debug Variables
    private int rateOfChange = 1; // Use to speed up growth/decay rates


    public virtual void UpdateStats(float time)
    {
        if (hungerMain <= 0)
            hungerMain += (time * hungerGrowthRate) * rateOfChange;            // Increase

        if (dirtinessMain <= 0)
            dirtinessMain += (time * dirtinessGrowthRate) * rateOfChange;      // Increase

        if (sleepinessMain <= 0)
            sleepinessMain += (time * sleepinessGrowthRate) * rateOfChange;    // Increase

        if (happinessMain >= 0)
            happinessMain -= (time * happinessDecayRate) * rateOfChange;       // Decrease

        ClampStats(ref hungerMain, ref dirtinessMain, ref sleepinessMain, ref happinessMain);

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

    public void Initialize(string typeName)
    {
        TypeName = typeName;
    }

}


