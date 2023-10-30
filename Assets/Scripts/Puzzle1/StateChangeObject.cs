using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChangeObject : MonoBehaviour
{
    public Puzzle1 stateMachine;
    public puzzle1States state;

    void Update() 
    {
        if(stateMachine.CurrentState == state) 
        {
            gameObject.SetActive(false);
        }
    }
    
}
