using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawningFood : MonoBehaviour
{
    [SerializeField] private string currentPetID;

    [Header("Food prefab & parent")]
    public GameObject food;

    [Header("Spawn settings")]    
    public BoxCollider spawnArea;
    public float spawnDelay = 0.5f;
    public float spawnRate = 0.5f;
    public float xRange = 1f;
    public float spawnY = 4f;
    public float spawnZ = 0f;

    [Header("Minigame settings")]
    public int targetFood = 10;
    public float roundDuration = 10f;

    [Header("Text objects")] 
    public TextMeshProUGUI foodScore;
    public TextMeshProUGUI foodTimer;

    private int foodCount;
    private float mgTimer;
    private bool spawningActive = false;
    private Coroutine spawnRoutine;



    private void BeginFoodSpawn()
    {
        Bounds b = spawnArea.bounds;
        float x = Random.Range(b.min.x, b.max.x);
        float y = Random.Range(b.min.y, b.max.y);
        float z = Random.Range(b.min.z, b.max.z);

        Vector3 spawnPos = new Vector3(x, y, z);
        GameObject instance = Instantiate(food, spawnPos, Quaternion.identity);

        FallingFood ff = instance.GetComponent<FallingFood>();
        if (ff != null)
        {
            ff.Init(this);
        }


        Debug.Log("Spawned object");
    }


    private void Start()
    {
        
        // Instantiate(food);
        
        if (SessionContent.CurrentPetID != null)
            currentPetID = SessionContent.CurrentPetID;

        if (currentPetID != null) 
        { 
        BeginMinigameRound();
        }
        else
        {
            Debug.LogError("No pet to save data to.");
        }
    }

    private void Update()
    {
        if (!spawningActive) return;

        mgTimer -= Time.deltaTime;

        if (foodTimer != null)
            foodTimer.text = $"Time: {Mathf.CeilToInt(mgTimer)}";

        if (mgTimer <= 0f || foodCount >= targetFood)
            EndMinigame();
    }

    public void BeginMinigameRound()
    {
        if (food == null)
        {
            Debug.LogError("SpawningFood: 'food' prefab is not assigned.");
            return;
        }

        // Reset round state
        spawningActive = true;
        mgTimer = roundDuration;
        foodCount = 0;

        if (foodScore != null)
            foodScore.text = $"Score: {foodCount} / {targetFood}";

        // Start spawning
        if (spawnRoutine != null) StopCoroutine(spawnRoutine);
        spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        // Initial delay
        yield return new WaitForSeconds(spawnDelay);

        // Loop while active and conditions not met
        var wait = new WaitForSeconds(spawnRate);
        while (spawningActive && mgTimer > 0f && foodCount < targetFood)
        {
            BeginFoodSpawn();
            yield return wait;
        }
    }

    public void UpdateScore()
    {
        if (!spawningActive) return;

        foodCount = Mathf.Min(foodCount + 1, targetFood);

        if (foodScore != null)
            foodScore.text = $"Score: {foodCount} / {targetFood}";

        // Early end if target met
        if (foodCount >= targetFood)
            EndMinigame();
    }

    private void EndMinigame()
    {
        if (!spawningActive) return;

        spawningActive = false;

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        // If you want to apply rewards, call it here
        ApplyFeedingRewardToPet(currentPetID);
        SceneManager.LoadScene("Home");
    }

    private void ApplyFeedingRewardToPet(string petID)
    {        
        float hungerReducePerFood = 5f;
        float totalReduce = foodCount * hungerReducePerFood;

        // ------------------- Current goes over clamp values. Need to fix ------------------- 
        DataPersistenceManager.instance.UpdatePetStat(petID, s => s.hungerMain += totalReduce);
    }
}