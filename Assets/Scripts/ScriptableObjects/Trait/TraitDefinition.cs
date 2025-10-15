using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pets/Trait Definition")]
public class TraitDefinition : ScriptableObject
{
    public string traitName;                // e.g. "Drowsy"
    [TextArea] public string description;   // e.g. "Needs more rest"
    public List<TraitDefinition> incompatibleWith; // direct references to other TraitDefinitions
}