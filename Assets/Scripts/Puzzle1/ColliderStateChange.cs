using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderStateChange : MonoBehaviour
{
    public Puzzle1 stateMachine;
    public GameObject gameObjectToCollide;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObjectToCollide)
        {
            stateMachine.Change(puzzle1States.TreeWatered);
            other.gameObject.tag = "Untagged";
        }
    }
}