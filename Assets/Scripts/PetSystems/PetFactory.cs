using System;
using System.Collections.Generic;
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
public class PetFactory : IDataPersistence
{
    // TODO - Finish the pet creation using a dictionary to store the information

    // TODO - Create methods for saving data and loading data and connect it to the corresponding scripts
    private readonly PetTypeRegistrySO registry;
    private readonly TraitRegistrySO traitRegistry;

    private readonly Dictionary<string, (Pet pet, List<TraitDefinition> traits)> petDict
    = new Dictionary<string, (Pet, List<TraitDefinition>)>();

    private readonly Dictionary<string, GameObject> petPrefabs;


    [Serializable]
    public class PetData
    {
        public string name;
        public string type;
        public List<string> traitNames;
    }

    // ---------------- SAVE ----------------
    public void SaveData(ref GameData data)
    {
        Debug.Log("Save data called");

        data.allPetStats.Clear();

        foreach (var kvp in petDict)
        {
            string id = kvp.Key;
            Pet pet = kvp.Value.pet;
            List<TraitDefinition> traits = kvp.Value.traits;

            Debug.Log($"Saving pet {pet.petName} with {traits.Count} traits.");
            foreach (var t in traits)
                Debug.Log($" -> {t.traitName}");

            PetStatsData stats = new PetStatsData
            {
                uniqueID = id,
                petName = pet.petName,
                typeName = pet.typeName,

                hungerMain = pet.hungerMain,
                dirtinessMain = pet.dirtinessMain,
                sadnessMain = pet.sadnessMain,
                sleepinessMain = pet.sleepinessMain,

                traitNames = traits.ConvertAll(t => t.traitName) // use traitName from SO
            };

            data.allPetStats[id] = stats;
        }
    }



    // ---------------- LOAD ----------------
    public void LoadData(GameData data)
    {
        petDict.Clear();

        foreach (var kvp in data.allPetStats)
        {
            PetStatsData stats = kvp.Value;

            if (!registry.TryGet(stats.typeName, out PetTypeDefinition def))
            {
                Debug.LogError($"No PetTypeDefinition found for {stats.typeName}");
                continue;
            }

            GameObject go = GameObject.Instantiate(def.prefab);
            Pet pet = go.GetComponent<Pet>();

            if (pet == null)
            {
                Debug.LogError($"Prefab for {stats.typeName} missing Pet component.");
                continue;
            }

            // Restore identity
            pet.SetUniqueID(stats.uniqueID);
            pet.Initialize(stats.typeName, stats.petName);

            // Restore stats
            pet.hungerMain = stats.hungerMain;
            pet.dirtinessMain = stats.dirtinessMain;
            pet.sadnessMain = stats.sadnessMain;
            pet.sleepinessMain = stats.sleepinessMain;

            // Restore traits
            List<TraitDefinition> traits = new();
            foreach (string traitName in stats.traitNames)
            {
                if (traitRegistry.TryGet(traitName, out TraitDefinition trait))
                    traits.Add(trait);
                else
                    Debug.LogWarning($"Trait '{traitName}' not found in registry.");
            }
            Debug.Log($"Loaded pet {pet.petName} with traits:");
            foreach (var t in traits)
            {
                Debug.Log($" - {t.traitName}");
            }

            // Register in dictionary
            petDict[pet.UniqueID] = (pet, traits);
        }
    }

    public PetFactory(PetTypeRegistrySO registry, TraitRegistrySO traitRegistry)
    {
        this.registry = registry;
        this.traitRegistry = traitRegistry;
    }

    // Sets how common the number of traits a pet gets is
    private readonly Dictionary<int, float> traitRarity = new Dictionary<int, float>()
    {
        { 4, 1f },      // 1% chance
        { 3, 5f },      // 5% chance
        { 2, 14f },     // 14% chance
        { 1, 30f },     // 30% chance
        { 0, 50f }      // 50% chance
    };

    private int GetTraitCountByRarity()
    {
        float roll = UnityEngine.Random.Range(0f, 100f);
        float cumulative = 0f;

        foreach (var kvp in traitRarity)
        {
            cumulative += kvp.Value;
            if (roll < cumulative)
                return kvp.Key;
        }

        return 1; // fallback
    }

    // If no pet exists, create default pet
    public PetFactory(Dictionary<string, GameObject> prefabDict)
    {
        petPrefabs = prefabDict;

        if (petDict.Count == 0)
        {
            CreatePet("Rocko", "Pet_Rocko");
        }
    }


    /// Create a new pet instance by name and pet type.

    public Pet CreatePet(string petName, string typeName)
    {
        if (!registry.TryGet(typeName, out PetTypeDefinition def))
        {
            Debug.LogError($"No PetTypeDefinition found for {typeName}");
            return null;
        }

        GameObject go = GameObject.Instantiate(def.prefab);
        Pet pet = go.GetComponent<Pet>();

        if (pet == null)
        {
            Debug.LogError($"Prefab for {typeName} has no Pet component.");
            return null;
        }

        // Initialize identity
        pet.Initialize(typeName, petName);

        // Apply defaults from definition
        pet.hungerMain = def.defaultHunger;
        pet.dirtinessMain = def.defaultDirtiness;
        pet.sadnessMain = def.defaultSadness;
        pet.sleepinessMain = def.defaultSleepiness;

        // ---  Assign traits ---
        int traitCount = GetTraitCountByRarity();
        List<TraitDefinition> traits = GetRandomTraits(traitCount);
        Debug.Log($"Pet {petName} ({typeName}) got {traits.Count} traits:");
        foreach (var t in traits)
        {
            Debug.Log($" - {t.traitName}: {t.description}");
        }


        // Register in dictionary
        petDict[pet.UniqueID] = (pet, traits);

        return pet;
    }



    // Get a pet by name.
    public Pet GetPet(string name)
    {
        if (petDict.TryGetValue(name, out var data))
        {
            return data.pet;
        }
        return null;
    }


    // Get a read-only list of all pets.
    public IReadOnlyDictionary<string, (Pet pet, List<TraitDefinition> traits)> GetAllPetData()
    {
        return petDict;
    }


    // Create a Pet instance based on the type name.
    private Pet InstantiatePetType(string prefabKey, string petName = null, string uniqueID = null)
    {
        if (!petPrefabs.TryGetValue(prefabKey, out GameObject prefab))
        {
            Debug.LogError($"No prefab found for key: {prefabKey}");
            return null;
        }

        GameObject go = GameObject.Instantiate(prefab);
        Pet pet = go.GetComponent<Pet>();

        if (pet == null)
        {
            Debug.LogError($"Prefab {prefabKey} does not contain a Pet component.");
            return null;
        }

        pet.Initialize(prefabKey, petName);
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
