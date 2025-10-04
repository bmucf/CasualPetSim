using UnityEngine;

public class IdleBehaviorState : PetBehaviorBaseState
{


    public override void EnterState(PetBehaviorStateManager pet)
    {
        Debug.Log("Hello from the idle state.");
    }

    public override void UpdateState(PetBehaviorStateManager pet)
    {
        pet.transform.Rotate(Vector3.up * Time.deltaTime * 200);
    }

    public override void OnCollisionEnter(PetBehaviorStateManager pet)
    {

    }
}
