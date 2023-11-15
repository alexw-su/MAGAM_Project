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
        if(other.gameObject == gameObjectToCollide)
        {
            stateMachine.Change(state);
            other.gameObject.tag = "Untagged";
        }
    }
}