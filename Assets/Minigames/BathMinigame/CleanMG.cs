using UnityEngine;
using UnityEngine.SceneManagement;

public class CleanMG : MonoBehaviour
{
    public SwipeRocko swipeRocko;
    // public GameObject bathMG;

    void Update()
    {
        if (swipeRocko.cleanCount >= 20)
        {
            SceneManager.LoadScene("Home");
            Debug.Log("Sending Home!");
            ApplyWashRewardToPet();
        }
    }

    void ApplyWashRewardToPet()
    {
        Pet pet = FindObjectOfType<Pet>();
        if (pet == null) return;


        pet.dirtinessMain = Mathf.Max(0f, pet.dirtinessMain - 30f);
    }
}
