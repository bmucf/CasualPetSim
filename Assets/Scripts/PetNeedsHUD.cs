using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class PetNeedsHUD : MonoBehaviour
{
    public PetNeeds pet;

    public Image fullnessFill;
    public Image hydrationFill;
    public Image restFill;
    public Image happinessFill;

    public float smoothSpeed = 4f;

    void Start()
    {
        if (pet == null)
            pet = FindObjectOfType<PetNeeds>();

        
        if (fullnessFill == null) Debug.LogWarning("PetNeedsHUD: fullnessFill not assigned.");
    }

    void Update()
    {
        if (pet == null) return;

        UpdateBar(fullnessFill, pet.fullness);
        UpdateBar(hydrationFill, pet.hydration);
        UpdateBar(restFill, pet.rest);
        if (happinessFill != null) UpdateBar(happinessFill, pet.happiness);
    }

    void UpdateBar(Image img, float valueOutOf100)
    {
        if (img == null) return;
        float target = Mathf.Clamp01(valueOutOf100 / 100f);
        float current = img.fillAmount;
        float next = Mathf.MoveTowards(current, target, smoothSpeed * Time.deltaTime);
        img.fillAmount = next;

    }
}
