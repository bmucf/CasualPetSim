using UnityEngine;

public class RockPet : PetDefinition
{
    private void Start()
    {
        petName = "Rocko";
        petSpecies = "rock";

        foodLevel = 100f;
        hungerRate = 0.1f;
        waterLevel = 100f;
        thirstRate = 0.2f;
        restLevel = 100f;
        tireRate = 0.5f;

        size = 1;
    }
}
