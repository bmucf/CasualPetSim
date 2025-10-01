using System;
using System.Collections.Generic;

// ~ Istvan W


[Serializable]
public class Trait
{
    public string Name;
    public string Description;
    public List<string> IncompatibleWith;

    public Trait(string name, string description = "", List<string> incompatibleWith = null)
    {
        Name = name;
        Description = description;
        IncompatibleWith = incompatibleWith ?? new List<string>();
    }

    public override string ToString() => Name;
}
