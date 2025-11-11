using Newtonsoft.Json.Bson;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SheepSpawning : MonoBehaviour
{
    private float spawnDelay = 2f;
    private float spawnInterval = 1f;
    public GameObject sheep;
    public bool minigameActive;

    public int targetSheep = 5;
    private int sheepCount;
    public TextMeshProUGUI sheepCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
                SceneManager.LoadScene("Home");
                //Calling Rewards Function Goes Here
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
    }

    public void SheepScore()
    {
        sheepCount++;
        sheepCounter.text = $"Sheep Count: {sheepCount} / {targetSheep}";
    }

    private void ApplyRewardForPetSleep(string petID)
    {

    }
}
