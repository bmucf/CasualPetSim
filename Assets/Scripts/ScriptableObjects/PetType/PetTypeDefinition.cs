using UnityEngine;

[CreateAssetMenu(menuName = "Pets/Pet Type")]
public class PetTypeDefinition : ScriptableObject
{
    [Header("Identity")]
    public string typeName;          // e.g. "Dog", "Cat", "Rocko"
    public GameObject prefab;        // Prefab with Pet component

    [Header("Default Stats")]
    public float defaultHunger = 50f;
    public float defaultDirtiness = 50f;
    public float defaultSadness = 50f;
    public float defaultSleepiness = 50f;

    [Header("Growth Rates")]
    public float hungerGrowthRate = 0.01f;
    public float dirtinessGrowthRate = 0.01f;
    public float sleepinessGrowthRate = 0.01f;
    public float sadnessGrowthRate = 0.01f;

    // Optional: trait pools, rarity weights, icons, sounds, etc.
}
