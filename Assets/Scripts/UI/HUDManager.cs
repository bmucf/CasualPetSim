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

    public int stepSize = 10;

    private void Update()
    {
        if (pet == null) return;

        if (hungerSlider != null)
            hungerSlider.value = Quantize(pet.hungerMain);

        if (dirtinessSlider != null)
            dirtinessSlider.value = Quantize(pet.dirtinessMain);

        if (sadnessSlider != null)
            sadnessSlider.value = Quantize(pet.sadnessMain);

        if (sleepinessSlider != null)
            sleepinessSlider.value = Quantize(pet.sleepinessMain);
    }

    // Helper: snap 0–100 stat into steps, then normalize 0–1
    private float Quantize(float statValue)
    {
        // snap to nearest multiple of stepSize
        int snapped = Mathf.RoundToInt(statValue / stepSize) * stepSize;
        // normalize and invert (so 0 = full, 100 = empty)
        return 1f - (snapped / 100f);
    }

}