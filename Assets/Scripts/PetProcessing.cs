using UnityEngine;

public class PetProcessing : MonoBehaviour
{
    void Start()
    {
        GetComponent<MetabolismCalculator>();
        GetComponent<PetHunger>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
