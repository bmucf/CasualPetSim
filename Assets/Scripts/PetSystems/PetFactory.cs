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

    private readonly Dictionary<string, Pet> petDict = new Dictionary<string, Pet>();
    private readonly List<string> availableTraits = new List<string> 
    { "Drowsy", "Energetic", 
        "Playful", "Lazy" 
    };

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
            return petDict[name];
        }

        Pet newPet = InstantiatePetType(type);
        if (newPet == null)
        {
            Debug.LogError($"Could not create pet of type: {type}");
            return null;
        }

        // Optionally you can add a trait field to Pet for storing traits
        List<string> traits = GetRandomTraits(UnityEngine.Random.Range(0,3)); 
        // Store traits in pet subclass if needed

        petDict.Add(name, newPet);
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

    /// <summary>
    /// Create a Pet instance based on the type name.
    /// </summary>
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
    */

    // Return a random list of traits.

    // TODO - Add trait logic to prevent contrasting traits from being selected.
    private List<string> GetRandomTraits(int count)
    {
        List<string> selected = new List<string>();
        List<string> pool = new List<string>(availableTraits);

        for (int i = 0; i < count && pool.Count > 0; i++)
        {
            int index = UnityEngine.Random.Range(0, pool.Count);
            selected.Add(pool[index]);
            pool.RemoveAt(index); // avoid duplicates
        }

        return selected;
    }
}
