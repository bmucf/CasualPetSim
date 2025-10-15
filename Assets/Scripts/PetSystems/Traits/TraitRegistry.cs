using System.Collections.Generic;

// ~ Istvan W

public static class TraitRegistry
{
    public static readonly Dictionary<string, Trait> AllTraits = new()
    {
        { "Drowsy", new Trait("Drowsy", "Needs more rest", new List<string> { "Energetic" }) },
        { "Energetic", new Trait("Energetic", "Needs less rest", new List<string> { "Drowsy" }) },

        { "Lazy", new Trait("Lazy", "Needs less play to be happy", new List<string> { "Playful" }) },
        { "Playful", new Trait("Playful", "Needs more play to be happy", new List<string> { "Lazy" }) },

        { "Stinky", new Trait("Stinky", "Needs to be cleaned more often", new List<string> { "Prestine" }) },
        { "Prestine", new Trait("Prestine", "Stays cleaner longer", new List<string> { "Stinky" }) },

        { "Hungry", new Trait("Hungry", "Needs to be fed constantly", new List<string> { "Light Eater" }) },
        { "Light Eater", new Trait("Light Eater", "Does not need to be fed often", new List<string> { "Hungry" }) }
    };
}