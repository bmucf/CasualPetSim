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
    public float mgTimer;
    public TextMeshProUGUI foodTimer;

    
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
        if (uiManager.minigameHasStarted)
        {
            mgTimer -= Time.deltaTime;
            foodTimer.text = "Time: " + mgTimer;
            if (mgTimer <= 0 || foodCount >= 10)
            {
                uiManager.minigameHasStarted = false;
                foodCount = 0;
                uiManager.mainHUD.SetActive(true);
                uiManager.eatMinigame.SetActive(false);
                mgTimer = 10;
            }

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
        foodScore.text = "Food Score: " + foodCount + " / 10";
    }


}
