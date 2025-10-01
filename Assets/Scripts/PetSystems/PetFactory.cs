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
public class PetFactory
{
    // TODO - Finish the pet creation using a dictionary to store the information

    // TODO - Generated traits need to be added to a dict that goes inside of PetDict | Wrapper class recommended

    private readonly Dictionary<string, (Pet pet, Dictionary<string, bool> traits)> petDict
    = new Dictionary<string, (Pet, Dictionary<string, bool>)>();

    private readonly List<string> availableTraits = new List<string> 
    { 
        "Drowsy", "Energetic", 
        "Playful", "Lazy",
        "Stinky", "Prestine",
        "Hungry", "Light Eater"
    };

    // Builds a dictionary of traits for a pet
    private Dictionary<string, bool> BuildTraitDictionary(List<string> traits)
    {
        var dict = new Dictionary<string, bool>();
        foreach (var trait in traits)
        {
            dict[trait] = true;
        }
        return dict;

    }


    // Traits that cannot coexist
    private readonly Dictionary<string, List<string>> conflictingTraits = new Dictionary<string, List<string>>()
{
    { "Drowsy", new List<string> { "Energetic" } },
    { "Energetic", new List<string> { "Drowsy" } },

    { "Lazy", new List<string> { "Playful" } },
    { "Playful", new List<string> { "Lazy" } },

    { "Stinky", new List<string> { "Prestine" } },
    { "Prestine", new List<string> { "Stinky" } },

    { "Hungry", new List<string> { "Light Eater" } },
    { "Light Eater", new List<string> { "Hungry" } }
};

    // Sets the commoness of number of traits

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

    public PetFactory()
    {
        // If no pets exist, create a default pet
        if (petDict.Count == 0)
        {
            CreatePet("Rocko", "Rocko");
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
        List<string> traits = GetRandomTraits(traitCount);

        // Build dictionary of traits
        Dictionary<string, bool> traitDict = BuildTraitDictionary(traits);

        // Store both Pet and its traits in the main dictionary
        petDict.Add(name, (newPet, traitDict));

        return newPet;
    }

    /*
    /// <summary>
    /// Get a pet by name.
    /// </summary>
    public Pet GetPet(string name)
    {
        petDict.TryGetValue(name, out Pet pet);
        return pet;
    }

    /// <summary>
    /// Get a read-only list of all pets.
    /// </summary>
    public IReadOnlyDictionary<string, Pet> GetAllPets()
    {
        return petDict;
    }
    */

    // Create a Pet instance based on the type name.
    private Pet InstantiatePetType(string typeName)
    {
        Type petType = Type.GetType(typeName);
        if (petType == null || !typeof(Pet).IsAssignableFrom(petType))
        {
            Debug.LogError($"Invalid pet type: {typeName}");
            return null;
        }

        return (Pet)Activator.CreateInstance(petType);
    }

    // Return a random list of traits.
    // TODO - Add trait logic to prevent contrasting traits from being selected.
    private List<string> GetRandomTraits(int count)
    {
        List<string> selected = new List<string>();
        List<string> pool = new List<string>(availableTraits);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);
            string candidate = pool[index];

            // Check conflicts
            bool conflicts = false;
            foreach (string existing in selected)
            {
                if (conflictingTraits.ContainsKey(candidate) && conflictingTraits[candidate].Contains(existing))
                {
                    conflicts = true;
                    break;
                }
            }

            if (!conflicts)
            {
                selected.Add(candidate);
            }

            pool.RemoveAt(index); // remove from pool regardless to avoid infinite loop
        }

        return selected;
    }
}
