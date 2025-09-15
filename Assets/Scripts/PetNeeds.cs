using UnityEngine;

public class PetNeeds : MonoBehaviour
{
    public float fullness = 100f;
    public float hydration = 100f;
    public float rest = 100f;

    public float happiness = 100f;

    public float changeRate = 0.25f;

    // Update is called once per frame
    void Update()
    {
        fullness -= changeRate * Time.deltaTime;
        hydration -= changeRate * Time.deltaTime;
        rest -= changeRate * Time.deltaTime;

        fullness = Mathf.Clamp(fullness, 0, 100);
        hydration = Mathf.Clamp(hydration, 0, 100);
        rest = Mathf.Clamp(rest, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);

        FullnessHappiness();
        HydrationHappiness();
        RestHappiness();
    }
    public void FullnessHappiness()
    {
        if (fullness <= 20)
        {
            Debug.Log("Your pet is famished!");
            happiness += changeRate * -5f * Time.deltaTime;
        }
        else if (fullness <= 50)
        {
            Debug.Log("Your pet is getting hungry!");
            happiness += changeRate * -3f * Time.deltaTime;
        }
        else
        {
            happiness += changeRate * Time.deltaTime;
        }
    }

    public void HydrationHappiness()
    {
        if (hydration <= 20)
        {
            Debug.Log("Your pet is parched!");
            happiness += changeRate * -5f * Time.deltaTime;
        }
        else if (hydration <= 50)
        {
            Debug.Log("Your pet is getting thirsty!");
            happiness += changeRate * -3f * Time.deltaTime;
        }
        else
        {
            happiness += changeRate * Time.deltaTime;
        }
    }

    public void RestHappiness()
    {
        if (rest <= 20)
        {
            Debug.Log("Your pet is exhausted!");
            happiness += changeRate * -5f * Time.deltaTime;
        }
        else if (rest <= 50)
        {
            Debug.Log("Your pet is getting tired!");
            happiness += changeRate * -3f * Time.deltaTime;
        }
        else
        {
            happiness += changeRate * Time.deltaTime;
        }
    }
}
