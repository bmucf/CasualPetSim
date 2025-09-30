using System;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

[System.Serializable]
public class GameData
{
    // What is being saved
    // Ex: Current Stats, Clothes, Equipment, Currency
    public string lastSavedTime;

    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to Load

    public GameData()
    {
        this.lastSavedTime = DateTime.Now.ToString(); // ISO 8601 format
    }

}
