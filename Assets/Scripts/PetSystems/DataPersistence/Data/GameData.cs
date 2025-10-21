using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

// ~ Istvan Wallace

[System.Serializable]
public class GameData
{
    /// <summary>
    /// What is being saved
    /// </summary>
    // Ex: Current Stats, Clothes, Equipment, Currency

    // Unique ID list
    public List<string> petIDList;

    // Unique ID is dictionary key
    public Dictionary<string, PetStatsData> allPetStats; // Name, Type, Stats, and Traits
    public Dictionary<string, string> allPetLastSavedTimes; // Last saved time


    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to Load

    public void AddPetID(string uniqueID)
    {
        if (!petIDList.Contains(uniqueID))
        {
            petIDList.Add(uniqueID);
        }
    }

    // Initialize references
    public GameData()
    {
        petIDList = new List<string>();
        allPetLastSavedTimes = new Dictionary<string, string>();
        allPetStats = new Dictionary<string, PetStatsData>();

    }

}
