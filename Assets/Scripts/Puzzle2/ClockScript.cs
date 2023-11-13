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
        if (stateManager.CurrentState != Puzzle2States.PointerPlaced)
        {
            transform.Rotate(0, -20, 0);

            float yRotation = transform.localEulerAngles.y % 360;

            if (Mathf.Abs(yRotation) <= clockSolvedThreshold)
            {
                OnClockSolved?.Invoke();
            }
        }

    }
}
