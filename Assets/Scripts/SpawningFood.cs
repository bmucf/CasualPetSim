using UnityEngine;
using TMPro;

public class SpawningFood : MonoBehaviour
{
    public RoomUIManager uiManager;

    public GameObject food;
    public float spawnDelay = 0.5f;
    public float spawnRate = 0.5f;
    public float xRange = 1f;

    public int foodCount;
    public float mgTimer = 10f;

    public TextMeshProUGUI foodScore;
    public TextMeshProUGUI foodTimer;

    Vector3 spawnPos;
    bool spawningActive = false;

    void Start()
    {
        if (uiManager == null)
            uiManager = FindObjectOfType<RoomUIManager>();

        InvokeRepeating(nameof(BeginFoodSpawn), spawnDelay, spawnRate);

        spawningActive = false;
        ResetUIForStart();
    }

    void Update()
    {
        if (uiManager != null && uiManager.minigameHasStarted)
        {
            spawningActive = true;

            mgTimer -= Time.deltaTime;

            if (foodTimer != null)
                foodTimer.text = "Time: " + Mathf.CeilToInt(mgTimer);

            if (mgTimer <= 0f || foodCount >= 10)
            {
                EndMinigame();
            }
        }
        else
        {
            spawningActive = false;
        }
    }

    void BeginFoodSpawn()
    {
        if (!spawningActive || food == null) return;

        spawnPos = new Vector3(Random.Range(-xRange, xRange), 5f, 0f);
        Instantiate(food, spawnPos, Quaternion.identity);
    }

    public void UpdateScore()
    {
        foodCount++;

        if (foodScore != null)
            foodScore.text = "Food Score: " + foodCount + " / 10";
    }

    void ResetUIForStart()
    {
        if (foodScore != null)
            foodScore.text = "Food Score: " + foodCount + " / 10";

        if (foodTimer != null)
            foodTimer.text = "Time: " + Mathf.CeilToInt(mgTimer);
    }

    void ApplyFeedingRewardToPet()
    {
        Pet pet = FindObjectOfType<Pet>();
        if (pet == null) return;

        float hungerReducePerFood = 5f;
        float totalReduce = foodCount * hungerReducePerFood;

        pet.hungerMain = Mathf.Max(0f, pet.hungerMain - totalReduce);
    }

    void EndMinigame()
    {
        spawningActive = false;

        ApplyFeedingRewardToPet();

        mgTimer = 10f;
        foodCount = 0;
        ResetUIForStart();

        if (uiManager != null)
            uiManager.ExitFeedingMinigame();
    }

    public void BeginMinigameRound()
    {
        spawningActive = true;
        mgTimer = 10f;
        foodCount = 0;
        ResetUIForStart();
    }
}
