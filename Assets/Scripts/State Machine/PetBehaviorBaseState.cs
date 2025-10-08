using UnityEngine;

public abstract class PetBehaviorBaseState
{
    public abstract void EnterState(PetBehaviorStateManager pet);
    public abstract void UpdateState(PetBehaviorStateManager pet);
    public abstract void OnCollisionEnter(PetBehaviorStateManager pet);
}
