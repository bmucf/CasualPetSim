using UnityEngine;

public class PetSpawner : MonoBehaviour
{
    public PP_BuildTool petTypeToSpawn;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to spawn
        {
            if (petTypeToSpawn != null && petTypeToSpawn.petModel != null)
            {
                Instantiate(petTypeToSpawn.petModel, Vector3.zero, Quaternion.identity);
                Debug.Log($"A {petTypeToSpawn.speciesName} spawned!");
            }
            else
            {
                Debug.LogWarning("PetType or prefab is missing!");
            }
        }
    }
}