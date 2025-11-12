using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelUI : MonoBehaviour
{
    [Header("Sliders")]
    public Slider volumeSlider;
    public Slider brightnessSlider;

    private void OnEnable()
    {
        if (volumeSlider)
        {
            float v = PlayerPrefs.GetFloat("MasterVolume", 1f);
            volumeSlider.value = v;
            UpdateVolume(v);
            volumeSlider.onValueChanged.AddListener(UpdateVolume);
        }

        if (brightnessSlider)
        {
            float b = PlayerPrefs.GetFloat("ScreenBrightness", 1f);
            brightnessSlider.value = b;
            UpdateBrightness(b);
            brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
        }
    }

    private void OnDisable()
    {
        if (volumeSlider) volumeSlider.onValueChanged.RemoveAllListeners();
        if (brightnessSlider) brightnessSlider.onValueChanged.RemoveAllListeners();
    }

    // Volume COntrol
    public void UpdateVolume(float v)
    {
        AudioListener.volume = v;
        PlayerPrefs.SetFloat("MasterVolume", v);
    }

    // Brightness Control
    public void UpdateBrightness(float v)
    {
        
        var overlay = GameObject.Find("BrightnessOverlay");
        if (overlay)
        {
            var img = overlay.GetComponent<CanvasGroup>();
            if (img) img.alpha = 1f - v;
        }
        PlayerPrefs.SetFloat("ScreenBrightness", v);
    }
}
