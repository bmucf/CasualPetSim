using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Cycle Settings")]
    public float dayLengthInMinutes = 5f;  // 5 minute = 24-hour cycle
    public Light sunLight;                 // Directional Light
    public Gradient lightColor;            // Color over time
    public AnimationCurve lightIntensity;  // Intensity over time
    
    private float timeOfDay = 0f;          // 0 to 5 (represents 24 hours)

    void Update()
    {
        // Advance time
        timeOfDay += (Time.deltaTime / (dayLengthInMinutes * 60f));
        if (timeOfDay >= 1f) timeOfDay = 0f;

        // Rotate the light
        float sunAngle = timeOfDay * 360f - 90f; // -90 so sunrise starts at horizon
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Change light color/intensity if curves are set
        if (lightColor != null)
            sunLight.color = lightColor.Evaluate(timeOfDay);
        if (lightIntensity != null)
            sunLight.intensity = lightIntensity.Evaluate(timeOfDay);
    }
}
