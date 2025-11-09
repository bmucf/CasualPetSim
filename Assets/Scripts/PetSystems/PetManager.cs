using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Istvan Wallace

public class PetManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private PetTypeRegistrySO registry;
    [SerializeField] private TraitRegistrySO traitRegistry;

    private PetFactory petFactory;
    
    public static PetManager Instance { get; private set; }
    public Pet CurrentPet { get; private set; }


    public List<string> uniqueIDs = new List<string>();
    public string currentPetID;
    private int currentIndex = 0;
    public bool newGame = false;

    public Dictionary<string, GameObject> petInstances = new Dictionary<string, GameObject>();
    public Dictionary<string, Pet> petObjectReference = new Dictionary<string, Pet>();

    [SerializeField] private Transform spawnPoint;

    public event System.Action<Pet> OnPetChanged;


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
            newGame = true;
            InitializeFactory();
            NewGame();
            currentPetID = uniqueIDs[0];
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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {

        if (!newGame)
        {
            // Load the pet(s)
            foreach (var id in uniqueIDs)
            {
                petFactory.LoadPet(id, spawnPoint);
            }
        }
        if (uniqueIDs.Count > 0 && SessionContent.CurrentPetID == null)
        {
            currentIndex = 0;
            currentPetID = uniqueIDs[0];
            SetActivePet(); 
        }
        else
        {
            currentPetID = SessionContent.CurrentPetID;
            SetActivePet();
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
        petFactory.CreatePet("Rocko", "PetRocko", spawnPoint);
    }

    // TESTING
    private void Update()
    {
        //
        // Spawn another pet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NewGame();
            SwitchPet();
            SetActivePet();
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
        // Save before switch
        DataPersistenceManager.instance.SaveGame();

        // Turn off previous pet
        petInstances[currentPetID].SetActive(false);

        // advance index and wrap around
        currentIndex = (currentIndex + 1) % uniqueIDs.Count;

        // update currentPetID
        currentPetID = uniqueIDs[currentIndex];

        SetActivePet();
    }

    private void OnDestroy()
    {
        SessionContent.CurrentPetID = currentPetID;
    }
    public void SetActivePet()
    {
        if (!petInstances.ContainsKey(currentPetID) || !petObjectReference.ContainsKey(currentPetID))
        {
            Debug.LogWarning($"PetManager: Tried to set active pet with ID {currentPetID}, but it wasn't found.");
            return;
        }

        petInstances[currentPetID].SetActive(true);
        CurrentPet = petObjectReference[currentPetID];
        SessionContent.CurrentPetID = currentPetID;

        // Notify subscribers of pet change
        OnPetChanged?.Invoke(CurrentPet);
    }
}
