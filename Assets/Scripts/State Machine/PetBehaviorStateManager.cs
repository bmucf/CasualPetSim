using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PetBehaviorStateManager : MonoBehaviour
{
    public PetBehaviorBaseState currentState;
    public IdleBehaviorState IdleBehaviorState = new IdleBehaviorState();
    public ExplorationBehaviorState ExplorationBehaviorState = new ExplorationBehaviorState();

    private void Start()
    {
        currentState = IdleBehaviorState;

        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PetBehaviorBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}