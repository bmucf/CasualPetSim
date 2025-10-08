using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetNeedsUI : MonoBehaviour
{
    [Header("References")]
    public PetNeeds petNeeds;

    [Header("UI Elements")]
    public TextMeshProUGUI fullnessText;
    public TextMeshProUGUI hydrationText;
    public TextMeshProUGUI restText;
    public TextMeshProUGUI happinessText;

    [Header("Restore Amounts")]
    public float feedAmount = 20f;
    public float drinkAmount = 20f;
    public float sleepAmount = 20f;

    void Start()
    {
        if (petNeeds == null)
        {
            petNeeds = GetComponent<PetNeeds>();
            if (petNeeds == null)
            {
                Debug.LogError("PetNeedsUI needs a PetNeeds reference!");
            }
        }
    }

    void Update()
    {
        if (petNeeds == null) return;

        // Update UI texts
        if (fullnessText != null)
            fullnessText.text = $"Fullness: {Mathf.RoundToInt(petNeeds.fullness)}";

        if (hydrationText != null)
            hydrationText.text = $"Hydration: {Mathf.RoundToInt(petNeeds.hydration)}";

        if (restText != null)
            restText.text = $"Rest: {Mathf.RoundToInt(petNeeds.rest)}";

        if (happinessText != null)
            happinessText.text = $"Happiness: {Mathf.RoundToInt(petNeeds.happiness)}";
    }

    // Button Functions
    public void FeedPet()
    {
        if (petNeeds == null) return;
        petNeeds.fullness = Mathf.Clamp(petNeeds.fullness + feedAmount, 0f, 100f);
    }

    public void GiveWater()
    {
        if (petNeeds == null) return;
        petNeeds.hydration = Mathf.Clamp(petNeeds.hydration + drinkAmount, 0f, 100f);
    }

    public void LetRest()
    {
        if (petNeeds == null) return;
        petNeeds.rest = Mathf.Clamp(petNeeds.rest + sleepAmount, 0f, 100f);
    }
}
