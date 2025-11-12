using Newtonsoft.Json.Bson;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SheepSpawning : MonoBehaviour
{
    [SerializeField] private string currentPetID;

    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float spawnInterval = 1f;
    public GameObject sheep;
    public bool minigameActive;

    public int targetSheep = 5;
    private int sheepCount;
    public TextMeshProUGUI sheepCounter;

    public int pointsPerSheep = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SessionContent.CurrentPetID != null)
            currentPetID = SessionContent.CurrentPetID;

        minigameActive = true;
        StartCoroutine(SheepLoop());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (minigameActive)
        {
            if (sheepCount == targetSheep)
            {
                minigameActive = false;
                SheepEnd();
            }
        }
    }

    IEnumerator SheepLoop()
    {
        while (minigameActive)
        {
            yield return new WaitForSeconds(spawnDelay);
            var wait = new WaitForSeconds(spawnInterval);
            Vector3 spawnPos = new Vector3(-17, -7, 55);
            Instantiate(sheep, spawnPos, Quaternion.identity);
        }
    }

    public void SheepEnd()
    {
        minigameActive = false;
        StopCoroutine(SheepLoop());

        // Calling Rewards Function Goes Here
        ApplyRewardForPetSleep(currentPetID);

        // Go back to "Home" scene
        SceneManager.LoadScene("Home");
    }

    public void SheepScore()
    {
        sheepCount++;
        sheepCounter.text = $"Sheep Count: {sheepCount} / {targetSheep}";
    }

    private void ApplyRewardForPetSleep(string petID)
    {
        float totalReduce = sheepCount * pointsPerSheep;
        Debug.Log($"You have just earned {totalReduce} points!");

        DataPersistenceManager.instance.UpdatePetStat(petID, s => s.sleepinessMain -= totalReduce);
    }
}
