using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Pet Reference")]
    [SerializeField] private Pet pet;

    [Header("Sliders")]
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image dirtinessBar;
    [SerializeField] private Image sadnessBar;
    [SerializeField] private Image sleepinessBar;

    public float smoothSpeed = 4f;

    public int stepSize = 10;

    private void Start()
    {
        if (PetManager.Instance != null)
        {
            PetManager.Instance.OnPetChanged += UpdateTrackedPet;

            // immediate sync with current pet
            if (PetManager.Instance.CurrentPet != null)
                UpdateTrackedPet(PetManager.Instance.CurrentPet);

        }
    }

    private void OnDestroy()
    {
        if (PetManager.Instance != null)
            PetManager.Instance.OnPetChanged -= UpdateTrackedPet;
    }

    private void Update()
    {
        if (pet == null) return;

        if (hungerBar != null)
            UpdateBar(hungerBar, Quantize(pet.hungerMain));

        if (dirtinessBar != null)
            UpdateBar(dirtinessBar, Quantize(pet.dirtinessMain));

        if (sadnessBar != null)
            UpdateBar(sadnessBar, Quantize(pet.sadnessMain));

        if (sleepinessBar != null)
            UpdateBar(sleepinessBar, Quantize(pet.sleepinessMain));
    }

    // Helper: snap stat into steps, then normalize 0?
    private float Quantize(float statValue)
    {
        // snap to nearest multiple of stepSize
        int snapped = Mathf.RoundToInt(statValue / stepSize) * stepSize;

        // normalize and invert (so 0 = full, 100 = empty)
        return 1f - (snapped / 100f);
    }
    private void UpdateTrackedPet(Pet pet)
    {
        // Debug.Log($"HUD now tracking {pet.name}");
        this.pet = pet;
    }
    void UpdateBar(Image img, float targetValue)
    {
        {
            // Smooth animation toward target
            float currentValue = img.fillAmount;
            img.fillAmount = Mathf.MoveTowards(currentValue, targetValue, smoothSpeed * Time.deltaTime);
        }
    }
}