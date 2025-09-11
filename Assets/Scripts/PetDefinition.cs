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

    Vector3 size = new Vector3(0.5f, 0.5f, 0.5f);
    public float growthRate = 1.1f;

    Vector3 scaleChange = new Vector3(1.1f,1.1f ,1.1f );

    private void Update()
    {
        Hunger();
        Thirst();
        Exhaust();
        Growth();
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

    public void Growth()
    {
        transform.localScale *= growthRate  * Time.deltaTime;
    }
}
