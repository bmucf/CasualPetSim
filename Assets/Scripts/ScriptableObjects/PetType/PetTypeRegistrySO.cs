using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pets/Pet Registry")]
public class PetTypeRegistrySO : ScriptableObject
{
    public List<PetTypeDefinition> petTypes;

    private Dictionary<string, PetTypeDefinition> lookup;

    public void BuildLookup()
    {
        lookup = new Dictionary<string, PetTypeDefinition>();
        foreach (var def in petTypes)
        {
            if (!string.IsNullOrEmpty(def.typeName))
                lookup[def.typeName] = def;
        }
    }

    public bool TryGet(string typeName, out PetTypeDefinition def)
    {
        if (lookup == null) BuildLookup();
        return lookup.TryGetValue(typeName, out def);
    }
}