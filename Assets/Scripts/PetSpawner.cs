using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public GameObject petPrefab;  // The pet prefab to spawn
    public Transform spawnPoint;  // The position where pets will spawn
    public float minValue = 20f;  // Minimum value for fullness, hydration, and rest
    public float maxValue = 100f; // Maximum value for fullness, hydration, and rest

    void Update()
    {
        // Check if the Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spawn a pet when the spacebar is pressed
            SpawnPet();
        }
    }

    /// <summary>
    /// Spawns a pet prefab and randomizes its fullness, hydration, and rest values.
    /// </summary>
    public void SpawnPet()
    {
        // Instantiate the pet prefab at the spawn point
        GameObject pet = Instantiate(petPrefab, spawnPoint.position, Quaternion.identity);

        // Get the PetNeeds component attached to the spawned pet
        PetNeeds petNeeds = pet.GetComponent<PetNeeds>();

        if (petNeeds != null)
        {
            // Randomize the fullness, hydration, and rest values
            petNeeds.fullness = Random.Range(minValue, maxValue);
            petNeeds.hydration = Random.Range(minValue, maxValue);
            petNeeds.rest = Random.Range(minValue, maxValue);

            // Optionally, log the values for debugging
            Debug.Log($"Pet Spawned! Fullness: {petNeeds.fullness}, Hydration: {petNeeds.hydration}, Rest: {petNeeds.rest}");
        }
        else
        {
            Debug.LogError("PetPrefab does not have a PetNeeds component attached!");
        }
    }
}
