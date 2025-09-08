using UnityEngine;

public class PetProcesses : RockPet
{
    private void Update()
    {
        foodLevel -= hungerRate * Time.deltaTime;
        waterLevel -= thirstRate * Time.deltaTime;
        restLevel -= tireRate * Time.deltaTime;
    }
}
