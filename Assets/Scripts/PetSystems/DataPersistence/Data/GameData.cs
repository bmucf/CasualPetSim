using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

// ~ Istvan Wallace

[System.Serializable]
public class GameData
{
    // What is being saved
    // Ex: Current Stats, Clothes, Equipment, Currency
    public string lastSavedTime;

    public Dictionary<string, PetStatsData> allPetStats;


    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to Load

    // Initialize references
    public GameData()
    {
        this.lastSavedTime = DateTime.Now.ToString(); // ISO 8601 format
        allPetStats = new Dictionary<string, PetStatsData>();
    }

}
