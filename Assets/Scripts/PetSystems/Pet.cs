using System;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

// ~ Istvan W

// Handles all the factors a pet can inherit related to pet stats
// Data and Simulation Logic

public abstract class Pet : MonoBehaviour 
{
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
    public float sadnessDecayRate => 0.01f;    //

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
            sadnessMain += (time * sadnessDecayRate) * rateOfChange;       // Increase

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

}


