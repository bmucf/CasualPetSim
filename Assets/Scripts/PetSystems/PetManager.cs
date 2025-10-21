using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ~ Istvan W

public class PetManager : MonoBehaviour
{
    [SerializeField] private PetTypeRegistrySO registry;
    [SerializeField] private TraitRegistrySO traitRegistry;
    [SerializeField] private Transform petContainer;

    private PetFactory petFactory;

    // Runtime dictionary of spawned pets
    private Dictionary<string, Pet> pets = new Dictionary<string, Pet>();

    private string currentPetID;

    // Public API
    public Pet CurrentPet => pets.TryGetValue(currentPetID, out var pet) ? pet : null;
    public string CurrentPetID => currentPetID;

    private void Awake()
    {
        registry = Resources.Load<PetTypeRegistrySO>("SO/PetTypeRegistrySO");
        traitRegistry = Resources.Load<TraitRegistrySO>("SO/TraitRegistrySO");
        petFactory = new PetFactory(registry, traitRegistry);
    }

    void Start()
    {
        LoadPetsFromSavedData();

        if (pets.Count > 0)
        {
            SetActivePet(pets.Keys.First()); // Set the first one active by default
        }
    }

    private void LoadPetsFromSavedData()
    {
        GameData data = DataPersistenceManager.instance.GetGameData();

        if (data == null || data.petIDList == null || data.allPetStats == null)
        {
            Debug.LogWarning("No saved pet data found.");
            return;
        }

        foreach (string id in data.petIDList)
        {
            if (data.allPetStats.TryGetValue(id, out PetStatsData stats))
            {
                // Create pet from factory using saved data
                Pet pet = petFactory.CreatePet(stats.petName, stats.typeName);

                pet.SetUniqueID(id); // Assign the saved ID
                pet.LoadData(data); // Load stats into the pet

                // Position & parent in scene
                pet.transform.SetParent(petContainer != null ? petContainer : transform);
                pet.transform.localPosition = Vector3.zero + new Vector3(pets.Count * 2, 0, 0);

                pet.gameObject.SetActive(false); // Initially hide all pets

                pets[id] = pet;
            }
        }
    }

    public void SetActivePet(string petID)
    {
        if (!pets.ContainsKey(petID))
        {
            Debug.LogWarning($"Pet with ID {petID} not found.");
            return;
        }

        foreach (var kvp in pets)
        {
            kvp.Value.gameObject.SetActive(kvp.Key == petID);
        }

        currentPetID = petID;
        Debug.Log($"Switched to pet: {pets[petID].petName}");
    }

    public void SwitchToNextPet()
    {
        if (pets.Count == 0) return;

        var keys = pets.Keys.ToList();
        int currentIndex = keys.IndexOf(currentPetID);
        int nextIndex = (currentIndex + 1) % keys.Count;

        SetActivePet(keys[nextIndex]);
    }

    // TESTING PURPOSES
    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.nKey.wasPressedThisFrame)
        {
            SwitchToNextPet();
        }
    }

}
