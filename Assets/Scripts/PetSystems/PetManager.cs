using System.Collections.Generic;
using UnityEngine;

// ~ Istvan W

public class PetManager : MonoBehaviour
{
    [SerializeField] private GameObject rockoPrefab;

    private PetFactory petFactory;

    private void Awake()
    {
        rockoPrefab = Resources.Load<GameObject>("Prefabs/Pets/rockoPrefab"); // filepath without extension
        if (rockoPrefab == null)
        {
            Debug.LogError("Rocko prefab not found in Resources!");
        }

    }
    void Start()
    {
        var prefabDict = new Dictionary<string, GameObject>
        {
            { "Pet_Rocko", rockoPrefab }
        };

        petFactory = new PetFactory(prefabDict);

        // Debug/Test
        /*
        var myPet = petFactory.CreatePet("Rocko", "Pet_Rocko");
        var petData = petFactory.GetAllPetData();
        foreach (var entry in petData)
        {
            Debug.Log($"Pet: {entry.Key}");
            foreach (var trait in entry.Value.traits)
            {
                Debug.Log($" - Trait: {trait.Name} ({trait.Description})");
            }
        }
        */
    }
}
