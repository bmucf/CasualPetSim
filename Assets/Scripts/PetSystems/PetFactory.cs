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
    [Serializable]
    public class PetData
    {
        public string name;
        public string type;
        public List<string> traitNames;
    }

    public void SaveData(ref GameData data)
    {
        data.allPets.Clear();

        foreach (var kvp in petDict)
        {
            PetData petData = new PetData
            {
                name = kvp.Key,
                type = kvp.Value.pet.TypeName, // Assuming Pet has a TypeName property
                traitNames = kvp.Value.traits.ConvertAll(t => t.Name)
            };

            data.allPets.Add(petData);
        }
    }

    public void LoadData(GameData data)
    {
        petDict.Clear();

        foreach (var petData in data.allPets)
        {
            Pet pet = InstantiatePetType(petData.type);
            if (pet == null)
            {
                Debug.LogError($"Failed to load pet: {petData.name}");
                continue;
            }

            List<Trait> traits = new();
            foreach (string traitName in petData.traitNames)
            {
                if (TraitRegistry.AllTraits.TryGetValue(traitName, out Trait trait))
                {
                    traits.Add(trait);
                }
                else
                {
                    Debug.LogWarning($"Trait '{traitName}' not found in registry.");
                }
            }

            petDict.Add(petData.name, (pet, traits));
        }
    }


    private readonly Dictionary<string, (Pet pet, List<Trait> traits)> petDict
        = new Dictionary<string, (Pet, List<Trait>)>();


    private readonly Dictionary<string, GameObject> petPrefabs;


    // Sets how common the number of traits a pet gets is

    // Key = number of traits, Value = chance (out of 100)
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
    public Pet CreatePet(string name, string type)
    {
        if (petDict.ContainsKey(name))
        {
            Debug.LogWarning($"A pet with name '{name}' already exists.");
            return petDict[name].pet; // return the existing Pet
        }

        Pet newPet = InstantiatePetType(type);
        if (newPet == null)
        {
            Debug.LogError($"Could not create pet of type: {type}");
            return null;
        }

        int traitCount = GetTraitCountByRarity();

        List<Trait> traits = GetRandomTraits(traitCount);
        petDict.Add(name, (newPet, traits));


        return newPet;
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
    public IReadOnlyDictionary<string, (Pet pet, List<Trait> traits)> GetAllPetData()
    {
        return petDict;
    }


    // Create a Pet instance based on the type name.
private Pet InstantiatePetType(string prefabKey)
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

    pet.Initialize(prefabKey); // Set the TypeName here

    return pet;
}

    // Return a random list of traits.
    private List<Trait> GetRandomTraits(int count)
    {
        List<Trait> selected = new();
        List<Trait> pool = new(TraitRegistry.AllTraits.Values);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);
            Trait candidate = pool[index];

            // Check for conflicts
            bool hasConflict = selected.Exists(existing =>
                candidate.IncompatibleWith.Contains(existing.Name));

            if (!hasConflict)
            {
                selected.Add(candidate);
            }

            pool.RemoveAt(index);
        }

        return selected;
    }



}
