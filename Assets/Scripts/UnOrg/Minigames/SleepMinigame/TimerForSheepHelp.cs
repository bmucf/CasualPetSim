using UnityEngine;

public class TimerForSheepHelp : MonoBehaviour
{
    private SheepSpawning sheepSpawning;
    public GameObject sheepTap;
    public float sheepTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sheepSpawning = GameObject.Find("SheepSpawner").GetComponent<SheepSpawning>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sheepSpawning.minigameActive) 
        {
            sheepTap.SetActive(true);
            sheepTime -= Time.deltaTime;
            if (sheepTime < 0)
            {
                sheepTap.SetActive(false);
            }
        }
        else
        {
            sheepTap.SetActive(false);
            sheepTime = 2f;
        }
    }
}
