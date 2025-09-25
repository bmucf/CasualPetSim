using JetBrains.Annotations;
using UnityEngine;

public class PetHunger : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxFoodLevel;
    public float currentFoodLevel;
    public float metabolicRate;

    [Header("Feeding Settings)")]
    public float nutritionPerFood;



    // Update is called once per frame
    void Update()
    {


    }

    public void Hungering()
    {
        currentFoodLevel -= metabolicRate * Time.deltaTime;
        currentFoodLevel = Mathf.Clamp(currentFoodLevel, 0f, maxFoodLevel);
    }
    public void FeedPet()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentFoodLevel += nutritionPerFood;
        }

    }

    public void DisplayFoodLevels()
    {
        float fullnessPercent = (currentFoodLevel / maxFoodLevel) * 100f;
        Debug.Log($"Fullness: {fullnessPercent.ToString("F0")}%");
    }
}
