using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStateChange : MonoBehaviour
{
    public Puzzle1 stateMachine;
    public puzzle1States state;
    public GameObject gameObjectToCollide;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObjectToCollide)
        {
            if(stateMachine.CurrentState != state)
            {
                stateMachine.Change(state);
            }
            
            other.gameObject.tag = "Untagged";
        }
    }
}