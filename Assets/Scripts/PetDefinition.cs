using UnityEngine;

public class PetDefinition : MonoBehaviour
{
    public PetNeed[] needs;

    private void Update()
    {
        foreach (var need in needs)
        {
            need.UpdateNeed();
        }
    }
}