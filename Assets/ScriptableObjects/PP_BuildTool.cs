using UnityEngine;

[CreateAssetMenu(fileName = "Species", menuName = "Pocket Pets/Species")]
public class PP_BuildTool : ScriptableObject
{
    [System.Serializable]
    public struct StatRange
    {
        public int lowerEnd;
        public int higherEnd;
    }


    public string speciesName;
    public GameObject petModel;

    public StatRange maxHungerRange;
    public float hungerRate;

    public StatRange maxHydrationRange;
    public float thirstRate;

    public StatRange maxRestRange;
    public float tireRate;
}
