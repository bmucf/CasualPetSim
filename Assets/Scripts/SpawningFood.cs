using System.Collections;
using TMPro;
using UnityEngine;

public class SpawningFood : MonoBehaviour
{
    private UI uiManager;
    public GameObject food;
    public int foodCount;
    private float zRange = 4;
    private Vector3 spawnPos;
    public float spawnDelay;
    public float spawnRate;
    public TextMeshProUGUI foodScore;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UI>();
        InvokeRepeating("BeginFoodSpawn", spawnDelay, spawnRate);
        uiManager.minigameHasStarted = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foodCount >= 6)
        {
           uiManager.minigameHasStarted = false;
           foodCount = 0;
           uiManager.mainHUD.SetActive(true);
           uiManager.eatMinigame.SetActive(false);
        }


    }

    private void BeginFoodSpawn()
    {
        {
            if (uiManager.minigameHasStarted)
            {
                spawnPos = new Vector3(8, 24, (Random.Range(-zRange, zRange)));
                Instantiate(food, spawnPos, Quaternion.identity);
                
            }
           
        }
    }

    public void UpdateScore()
    {
        foodCount++;
        foodScore.text = "Food Score: " + foodCount;
    }


}
