using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Attach this to your HUDController object under HUD.
// Then drag references in the Inspector.
public class PetStatusHUD : MonoBehaviour
{
    
    public Pet pet;
    public Image hungerFill;        // shows FOOD / satiety / fed-ness
    public Image cleanlinessFill;   // based on dirtinessMain
    public Image restFill;          // based on sleepinessMain
    public Image moodFill;          // based on sadnessMain

    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI cleanlinessText;
    public TextMeshProUGUI restText;
    public TextMeshProUGUI moodText;

    public float smoothSpeed = 4f;
    public Gradient barGradient;

    void Reset()
    {
        // Default gradient: red (low) -> yellow (mid) -> green (high)
        barGradient = new Gradient();
        barGradient.colorKeys = new GradientColorKey[] {
            new GradientColorKey(Color.red,    0f),
            new GradientColorKey(Color.yellow, 0.5f),
            new GradientColorKey(Color.green,  1f)
        };
        barGradient.alphaKeys = new GradientAlphaKey[] {
            new GradientAlphaKey(1f, 0f),
            new GradientAlphaKey(1f, 1f)
        };
    }

    void Start()
    {
        // safety net: try auto-find if you forgot to assign
        if (pet == null)
        {
            pet = FindObjectOfType<Pet>();
            if (pet == null)
            {
                Debug.LogWarning("PetStatusHUD: No Pet found in scene. Drag your Rocko_Pet here.");
            }
        }
    }

    void Update()
    {
        if (pet == null) return;

        // Convert problem-based stats (0=good,100=bad) into nice bars (1=good,0=bad)

        // hungerMain: 0 = not hungry (good), 100 = starving (bad)
        float fedRatio = 1f - Mathf.Clamp01(pet.hungerMain / 100f);

        // dirtinessMain: 0 = clean (good), 100 = filthy (bad)
        float cleanRatio = 1f - Mathf.Clamp01(pet.dirtinessMain / 100f);

        // sleepinessMain: 0 = awake (good), 100 = exhausted (bad)
        float restedRatio = 1f - Mathf.Clamp01(pet.sleepinessMain / 100f);

        // sadnessMain: 0 = happy (good), 100 = very sad (bad)
        float happyRatio = 1f - Mathf.Clamp01(pet.sadnessMain / 100f);

        // Update each bar + text
        UpdateBar(hungerFill, fedRatio, hungerText);
        UpdateBar(cleanlinessFill, cleanRatio, cleanlinessText);
        UpdateBar(restFill, restedRatio, restText);
        UpdateBar(moodFill, happyRatio, moodText);
        Debug.Log($"HUD DEBUG hunger={pet.hungerMain} fedRatio={fedRatio}");

    }

    void UpdateBar(Image img, float target01, TextMeshProUGUI txt)
    {
        if (img != null)
        {
            // Smooth animation toward target
            float current = img.fillAmount;
            float next = Mathf.MoveTowards(current, target01, smoothSpeed * Time.deltaTime);
            img.fillAmount = next;

        }

        if (txt != null)
        {
            int pct = Mathf.RoundToInt(target01 * 100f);
            txt.text = pct.ToString() + "%";
        }
    }
}
