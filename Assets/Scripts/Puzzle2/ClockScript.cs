using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour, IInteractable
{
    public Puzzle2 stateManager;
    [Space]
    public float clockSolvedThreshold = 20.0f;


    public delegate void ClockSolved();
    public event ClockSolved OnClockSolved;


    public void OnInteractionStart(bool isGrabbing)
    {
        transform.Rotate(0, -20, 0);

        float yRotation = transform.localEulerAngles.y % 360;

        //Mathf.Abs returns the absolute value (as in makes it positive) of the rotation, simplyfies the if statement
        if (Mathf.Abs(yRotation) <= clockSolvedThreshold)
        {
            OnClockSolved?.Invoke();
        }
    }
}
