using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour, IInteractable
{
    public Puzzle2 stateManager;

    void Start()
    {

    }
    void Update()
    {
        float yRotation = transform.localEulerAngles.y % 360;
        if (yRotation >= -30 && yRotation <= 30 && stateManager.CurrentState != Puzzle2States.TimeSet)
        {
            stateManager.Change(Puzzle2States.TimeSet);
        }
    }
    public void OnInteractionStart(bool isGrabbing)
    {
        transform.Rotate(0, -20, 0);
    }
}
