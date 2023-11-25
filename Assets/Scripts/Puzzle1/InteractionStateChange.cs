using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionStateChange : MonoBehaviour, IInteractable
{   
    public Puzzle1 stateMachine;
    public puzzle1States state;

    public void OnInteractionStart(bool isGrabbing) 
    { 
        if(isGrabbing)
            return;

        if(stateMachine.CurrentState != state)
        {
            stateMachine.Change(state);
        }
    }
}
