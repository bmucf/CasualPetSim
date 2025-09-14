using UnityEngine;

[System.Serializable]
public class PetNeed
{
    public string needName;
    public float level;
    public float decayRate;

    public void UpdateNeed()
    {
        level -= decayRate * Time.deltaTime;
        level = Mathf.Clamp(level, 0, 100);
    }
}
