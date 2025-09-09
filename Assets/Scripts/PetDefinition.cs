using UnityEngine;

public class PetDefinition : MonoBehaviour
{
    //declaring basic needs of pet
    public float foodLevel;
    public float hungerRate;

    public float hydrationLevel;
    public float thirstRate;

    public float restLevel;
    public float exhaustRate;

    public float size;
    public float growthRate;

    private void Update()
    {
        Hunger();
        Thirst();
        Exhaust();
    }

    public void Hunger()
    {
        foodLevel -= hungerRate * Time.deltaTime;
    }

    public void Thirst()
    {
        hydrationLevel -= thirstRate * Time.deltaTime;
    }

    public void Exhaust()
    {
        restLevel -= exhaustRate * Time.deltaTime;
    }
}
