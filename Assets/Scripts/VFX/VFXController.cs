using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

// - Istvan Wallace

public class VFXController : MonoBehaviour
{
    public VisualEffect death;
    public VisualEffect angry;
    public VisualEffect love;
    public VisualEffect happy;

    public float loveReductionValue = 2f;

    private void Awake()
    {

    }
    // Dictionary to store new CD times
    private Dictionary<string, float> effectCooldowns = new()
    {
        { "Death", 0f },
        { "Angry", 0f },
        { "Happy", 0f },
        { "Love", 0f }
    };
    // Dictionary that stores the CD duration
    public Dictionary<string, float> effectCooldownDurations = new()
    {
        {"Death" , 15f},
        {"Angry", 15f},
        {"Happy", 15f },
        {"Love", 2f }
    };

    public void TryPlayEffect(string effectName, Pet pet = null)
    {
        VisualEffect vfx = effectName switch
        {
            "Death" => death,
            "Angry" => angry,
            "Happy" => happy,
            "Love" => love,
            _ => null
        };

        if (vfx == null) return;

        effectCooldowns.TryGetValue(effectName, out float nextAllowedTime);

        if ( Time.time >= nextAllowedTime)
        {
            vfx.Play();
            effectCooldowns[effectName] = Time.time + effectCooldownDurations[effectName];

            // If Love was played trigger:
            if (effectName == "Love" && pet != null)
            {
                pet.sadnessMain -= loveReductionValue;
            }
        }
    }
}
