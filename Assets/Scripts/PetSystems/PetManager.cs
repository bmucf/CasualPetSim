using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ~ Istvan W

public class PetManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PetTypeRegistrySO registry;
    [SerializeField] private TraitRegistrySO traitRegistry;

    private PetFactory petFactory;
    private Pet pet;

    public List<string> uniqueIDs = new List<string>();

    public void LoadData(GameData data)
    {
        // Debug.Log("PetManager LoadData Called");

        if (data != null)
        {
            int i = 1;
            uniqueIDs = data.petIDList;

            Debug.Log("List of pet IDs:");

            foreach (var id in data.petIDList)
            {
                Debug.Log($"{i}: {id}\n");
                i++;
            }

        }

    }
    public void SaveData(ref GameData data)
    {
        // Debug.Log("PetManager SaveData Called");
    } 
    private void Awake()
    {
        registry = Resources.Load<PetTypeRegistrySO>("SO/PetTypeRegistrySO");
        traitRegistry = Resources.Load<TraitRegistrySO>("SO/TraitRegistrySO");
        petFactory = new PetFactory(registry, traitRegistry);
    }
    void Start()
    {
        GameData data = DataPersistenceManager.instance.GetGameData();

        // Create the pet
        foreach (var id in uniqueIDs)
        {
            petFactory.LoadPet(id);


        }
        
        /*
        // Place it in the scene under the PetManager
        var go = pet.gameObject;
        go.transform.SetParent(this.transform);   // parent under PetManager
        go.transform.position = Vector3.zero;     // put at world origin (or any position you want)
        */
    }
}
