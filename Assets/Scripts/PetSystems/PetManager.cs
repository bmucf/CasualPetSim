using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// ~ Istvan W

public class PetManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PetTypeRegistrySO registry;
    [SerializeField] private TraitRegistrySO traitRegistry;

    private PetFactory petFactory;
    // private Pet pet;

    public List<string> uniqueIDs = new List<string>();
    public string currentPetID;
    private int currentIndex = 0;

    public Dictionary<string, GameObject> petInstances = new Dictionary<string, GameObject>();



    public void LoadData(GameData data)
    {
        // Debug.Log("PetManager LoadData Called");

        if (data.petIDList.Count != 0)
        {
            int i = 1;
            uniqueIDs = data.petIDList;
            
            /*// Debug
            ///
            Debug.Log("List of pet IDs:");

            foreach (var id in data.petIDList)
            {
                Debug.Log($"{i}: {id}\n");
                i++;
            }
            ///*/
        }
        else
        {
            Debug.Log("No data was found, creating default pet");
            InitializeFactory();
            NewGame();
        }
    }
    public void SaveData(ref GameData data)
    {
        // Debug.Log("PetManager SaveData Called");
    }
    private void Awake()
    {
        InitializeFactory();
    }

    private void InitializeFactory()
    {
        if (petFactory == null) // guard against double init
        {
            registry = Resources.Load<PetTypeRegistrySO>("SO/PetTypeRegistrySO");
            traitRegistry = Resources.Load<TraitRegistrySO>("SO/TraitRegistrySO");
            petFactory = new PetFactory(registry, traitRegistry, this);
        }
    }

    void Start()
    {
        // GameData data = DataPersistenceManager.instance.GetGameData();

        // Load the pet(s)
        if (uniqueIDs != null)
        {
            foreach (var id in uniqueIDs)
            {
                petFactory.LoadPet(id);
            }
        }
        if (uniqueIDs.Count > 0)
        {
            currentIndex = 0;
            currentPetID = uniqueIDs[0];
            petInstances[currentPetID].SetActive(true);
        }
        /*
        // Place it in the scene under the PetManager
        var go = pet.gameObject;
        go.transform.SetParent(this.transform);   // parent under PetManager
        go.transform.position = Vector3.zero;     // put at world origin (or any position you want)
        */
    }

    public void NewGame()
    {
        petFactory.CreatePet("Rocko", "PetRocko");
    }

    // TESTING
    private void Update()
    {
        //
        // Spawn another pet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NewGame();
            Debug.Log("New Pet Added");
        }
        // Switch Pet
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("'J' key pressed");
            SwitchPet();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("'K' key pressed");
            int i= 0;
            foreach (var pet in petInstances)
            {
                Debug.Log($"{i}: {pet}");
                i++;
            }

        }

        //
    }

    private void SwitchPet()
    {
        if (uniqueIDs == null || uniqueIDs.Count == 0)
            return;
        // Turn off previous pet
        petInstances[currentPetID].SetActive(false);

        // advance index and wrap around
        currentIndex = (currentIndex + 1) % uniqueIDs.Count;

        // update currentPetID
        currentPetID = uniqueIDs[currentIndex];

        // Turn on current pet
        petInstances[currentPetID].SetActive(true);

        // Debug.Log($"Switched to pet {currentPetID}");
    }
}
