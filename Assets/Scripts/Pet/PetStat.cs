using UnityEngine;

// Component attached to the pet GameObject
// Presentation and Interaction Logic
public class PetStat : MonoBehaviour
{
    private Pet pet = new Pet();

    void Start()
    {
        LoadPetState();
    }

    void Update()
    {
        pet.UpdateStats(Time.deltaTime);
        ApplyStatEffects();
    }

    void ApplyStatEffects()
    {
        // Example: clamp values and trigger animations or feedback
        // E.g. If Hunger > 80 trigger sadSound and sadAnimation

        pet.hunger = Mathf.Clamp(pet.hunger, 0f, 100f);
        pet.happiness = Mathf.Clamp(pet.happiness, 0f, 100f);
        // TODO: Add visual/audio feedback based on thresholds
    }

    // Load previous pet data
    void LoadPetState()
    {
        // TODO: Load from PlayerPrefs or JSON
    }

    // Save current pet data
    void SavePetState()
    {
        // TODO: Save current stats
    }
}
