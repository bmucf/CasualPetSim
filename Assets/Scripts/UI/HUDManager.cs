using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Pet Reference")]
    [SerializeField] private Pet pet;   // assign your Pet in Inspector

    [Header("Sliders")]
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider dirtinessSlider;
    [SerializeField] private Slider sadnessSlider;
    [SerializeField] private Slider sleepinessSlider;

    private void Update()
    {
        if (pet == null) return;

        // Normalize 0–100 stats into 0–1 slider values
        // Only update if the slider is assigned
        if (hungerSlider != null)
            hungerSlider.value = 1f - (pet.hungerMain / 100f);

        if (dirtinessSlider != null)
            dirtinessSlider.value = 1f - (pet.dirtinessMain / 100f);

        if (sadnessSlider != null)
            sadnessSlider.value = 1f - (pet.sadnessMain / 100f);

        if (sleepinessSlider != null)
            sleepinessSlider.value = 1f - (pet.sleepinessMain / 100f);

    }
}