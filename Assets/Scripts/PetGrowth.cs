using UnityEngine;

public class PetGrowth : MonoBehaviour
{
    public float baseGrowthRate = 1f;
    private Vector3 scaleChange;

    public PetNeeds petNeeds;

    void Awake()
    {
        //makes pet face camera
        gameObject.transform.Rotate(0, 180, 0);
    }

    void Update()
    {
        Growth();
    }

    public void Growth()
    {
        float growthModifier = CalculateGrowthRate();


        scaleChange = new Vector3(growthModifier, growthModifier, growthModifier);
        gameObject.transform.localScale += scaleChange * Time.deltaTime;
    }

    private float CalculateGrowthRate()
    {
        float fullness = petNeeds.fullness;
        float hydration = petNeeds.hydration;
        float rest = petNeeds.rest;
        float growthRate;

        // Modify growth rate based on pet's needs
        if (fullness <= 20 || hydration <= 20 || rest <= 20)
        {
            growthRate = baseGrowthRate;
            growthRate *= 0f; // Decrease growth if any need is low
            Debug.Log("Pet Stopped Growing");
        }
        else if (fullness <= 50 || hydration <=50 || rest <= 50)
        {
            growthRate = baseGrowthRate;
            growthRate *= 0.001f; // Moderate growth if any need is below 50
            Debug.Log("Pet Growing Slowly");
        }
        else if (fullness > 98 || hydration > 98 || rest > 98)
        {
            growthRate = baseGrowthRate;
            growthRate *= 0.05f; // Increase growth if all needs are high
            Debug.Log("Pet Growing Quickly");
        }
        else
        {
            growthRate = baseGrowthRate;
            growthRate *= 0.01f; // Normal growth if all needs are above 50
            Debug.Log("Pet Growing Normally");
        }

        return growthRate;
    }
}