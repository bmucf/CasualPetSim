using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// ~ Istvan W

public class PetManager : MonoBehaviour
{
    [SerializeField] private PetTypeRegistrySO registry;
    [SerializeField] private TraitRegistrySO traitRegistry;

    private PetFactory petFactory;
    private Pet myPet;

    private void Awake()
    {
        registry = Resources.Load<PetTypeRegistrySO>("SO/PetTypeRegistrySO");
        traitRegistry = Resources.Load<TraitRegistrySO>("SO/TraitRegistrySO");

    }
    void Start()
    {
        // Create the pet
        petFactory = new PetFactory(registry, traitRegistry);
        myPet = petFactory.CreatePet("Rocko", "PetRocko");

        // Place it in the scene under the PetManager
        var go = myPet.gameObject;
        go.transform.SetParent(this.transform);   // parent under PetManager
        go.transform.position = Vector3.zero;     // put at world origin (or any position you want)
    }



}

