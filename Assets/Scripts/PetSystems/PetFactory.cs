using System;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

// ~ Istvan W

/*
 * This script only creates pet objects
 * and give them their random traits. E.g. -
 * Drowsy, Energetic, etc.
 * 
 * It should run a check to see if atleast one pet object has been made.
 * If not, create a new starting pet 'Rocko'
*/

public class PetFactory
{
    // TODO - Finish the pet creation using a dictionary to store the information

    // TODO - Create methods for saving data and loading data and connect it to the corresponding scripts
    private readonly PetTypeRegistrySO registry;
    private readonly TraitRegistrySO traitRegistry;

    // private readonly Dictionary<string, GameObject> petPrefabs;
    
    readonly GameData data = DataPersistenceManager.instance.GetGameData();
    private PetManager petManager;

    // If no spawnPoint then spawn 
    private Vector3 spawnPos;
    private Quaternion spawnRot;

    private void Init()
    {
        // Fallback: pick a default position/rotation
        spawnPos = new Vector3(0f, 0f, 0f); // origin, or wherever you want
        spawnRot = Quaternion.identity;     // no rotation

    }

    // ---------------- LOAD ----------------
    public Pet LoadPet(string ID, Transform spawnPoint)
    {
        Init();

        Pet pet = null;
        PetStatsData stats = data.allPetStats[ID];

        if (!registry.TryGet(stats.typeName, out PetTypeDefinition def))
        {
            Debug.LogError($"No PetTypeDefinition found for {stats.typeName}");
        }
        else
        {
            GameObject go;
            if (spawnPoint != null)
            {
                go = GameObject.Instantiate(def.prefab, spawnPoint.position, spawnPoint.rotation);
                pet = go.GetComponent<Pet>();
            }
            else
            {
                go = GameObject.Instantiate(def.prefab, spawnPos, spawnRot);
                pet = go.GetComponent<Pet>();
            }



            // Add pet to dictionary
            this.petManager.petInstances.Add(ID, go);
            this.petManager.petObjectReference.Add(ID, pet);
            // Debug.Log($"Added {ID} to pet dictionary");
            // Debug.Log($"{petManager.petInstances.Count}");


            if (pet == null)
            {
                Debug.LogError($"Prefab for {stats.typeName} missing Pet component.");
            }
            else
            {
                // Set ID to pet object
                pet.SetUniqueID(ID);

                // Apply loaded stats into Pet object
                pet.petName = stats.petName;
                pet.typeName = stats.typeName;
                pet.hungerMain = stats.hungerMain;
                pet.dirtinessMain = stats.dirtinessMain;
                pet.sadnessMain = stats.sadnessMain;
                pet.sleepinessMain = stats.sleepinessMain;

                // Restore traits
                List<TraitDefinition> traits = new();
                foreach (string traitName in stats.traitNames)
                {
                    if (traitRegistry.TryGet(traitName, out TraitDefinition trait))
                    {
                        traits.Add(trait);
                        // Debug.Log($"{trait} added to {pet.petName}");
                    }
                    else
                        Debug.LogWarning($"Trait '{traitName}' not found in registry.");
                }
                pet.traitList = new List<TraitDefinition>(traits);
                pet.traitNames = traits.ConvertAll(t => t.traitName);


            }
            go.SetActive(false);


            // Debug.Log($"{ID}: {pet.petName}, {pet.typeName}, {pet.hungerMain}, {pet.dirtinessMain}, {pet.sadnessMain}, {pet.sleepinessMain}");
        }
        return pet;
    }

    public PetFactory(PetTypeRegistrySO registry, TraitRegistrySO traitRegistry, PetManager petManager)
    {
        this.registry = registry;
        this.traitRegistry = traitRegistry;
        this.petManager = petManager;
    }

    // Sets how common the number of traits a pet gets is
    // Highest to lowest trait counts, explicit order guarantees correct cumulative checks.
    private readonly (int count, float percent)[] traitRarity = new[]
    {
    (4, 5f),   // 5%
    (3, 10f),   // 10%
    (2, 20f),  // 20%
    (1, 30f),  // 30%
    (0, 40f)   // 40%
    };

    private int GetTraitCountByRarity()
    {
        float roll = UnityEngine.Random.Range(0f, 100f); // [0,100)
        float cumulative = 0f;

        foreach (var entry in traitRarity)
        {
            cumulative += entry.percent;
            if (roll < cumulative)
                return entry.count;
        }

        // Fallback shouldn't happen if totals == 100
        return 1;
    }

    /// Create a new pet instance by name and pet type.
    public Pet CreatePet(string petName, string typeName, Transform spawnPoint)
    {
        Init();
        if (!registry.TryGet(typeName, out PetTypeDefinition def))
        {
            Debug.LogError($"No PetTypeDefinition found for {typeName}");
            return null;
        }

        GameObject go;
        Pet pet;

        if (spawnPoint != null)
        {
            go = GameObject.Instantiate(def.prefab, spawnPoint.position, spawnPoint.rotation);
            pet = go.GetComponent<Pet>();
        }
        else
        {
            go = GameObject.Instantiate(def.prefab, spawnPos, spawnRot);
            pet = go.GetComponent<Pet>();
        }

        if (pet == null)
        {
            Debug.LogError($"Prefab for {typeName} has no Pet component.");
            return null;
        }

        // Create unique ID
        string ID = pet.CreateUniqueID();
        // Debug.Log($"New pet's ID: {ID}");

        // Initialize identity
        pet.Initialize(typeName, petName);

        // Add pet to dictionary
        this.petManager.uniqueIDs.Add(ID);
        this.petManager.petInstances.Add(ID, go);
        this.petManager.petObjectReference.Add(ID, pet);
        // Debug.Log($"Added {ID} to pet dictionary");
        // Debug.Log($"{petManager.petInstances.Count}");

        // Apply defaults from definition
        pet.hungerMain = def.defaultHunger;
        pet.dirtinessMain = def.defaultDirtiness;
        pet.sadnessMain = def.defaultSadness;
        pet.sleepinessMain = def.defaultSleepiness;

        // ---  Assign traits ---
        int traitCount = GetTraitCountByRarity();
        List<TraitDefinition> traits = GetRandomTraits(traitCount);
        // Debug.Log($"Pet {petName} ({typeName}) got {traits.Count} traits:");
        foreach (var t in traits)
        {
            // Debug.Log($" - {t.traitName}: {t.description}");
        }

        // Register in traits list
        pet.traitList = new List<TraitDefinition>(traits);
        pet.traitNames = traits.ConvertAll(t => t.traitName);

        go.SetActive(false);

        return pet;
    }

    // Return a random list of traits.
    private List<TraitDefinition> GetRandomTraits(int count)
    {
        List<TraitDefinition> selected = new();
        List<TraitDefinition> pool = new(traitRegistry.traits);

        while (selected.Count < count && pool.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);
            TraitDefinition candidate = pool[index];

            bool hasConflict = selected.Exists(existing =>
                candidate.incompatibleWith.Contains(existing));

            if (!hasConflict)
                selected.Add(candidate);

            pool.RemoveAt(index);
        }

        return selected;
    }
}
