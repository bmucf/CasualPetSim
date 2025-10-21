using System.Collections.Generic;
using UnityEngine;

// ~ Istvan Wallace

// PetStats
[System.Serializable]
public class PetStatsData
{
    // public string uniqueID;
    public string petName;
    public string typeName;

    public float hungerMain;
    public float dirtinessMain;
    public float sadnessMain;
    public float sleepinessMain;

    public List<string> traitNames = new();

}
