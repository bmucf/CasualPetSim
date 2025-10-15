using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pets/Trait Registry")]
public class TraitRegistrySO : ScriptableObject
{
    public List<TraitDefinition> traits;

    private Dictionary<string, TraitDefinition> lookup;

    public void BuildLookup()
    {
        lookup = new Dictionary<string, TraitDefinition>();
        foreach (var t in traits)
            lookup[t.traitName] = t;
    }

    public bool TryGet(string name, out TraitDefinition def)
    {
        if (lookup == null) BuildLookup();
        return lookup.TryGetValue(name, out def);
    }
}