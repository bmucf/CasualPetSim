using UnityEngine;

// ~ Istvan Wallace
public class Rocko_Pet : Pet
{
    public string petNameBacking = "Rocko";
    public override string petName 
    {
        get => petNameBacking;
        set => petNameBacking = value;
    }

}
