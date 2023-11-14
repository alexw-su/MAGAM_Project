using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour, IInteractable
{
    public Puzzle2 stateManager;
    [Space]
    public float clockSolvedThreshold = 20.0f;


    public delegate void ClockSolved();
    public delegate void PointerPlaced();
    public event ClockSolved OnClockSolved;
    public event PointerPlaced OnPointerPlaced;


    public void OnInteractionStart(bool isGrabbing)
    {
        if (stateManager.CurrentState == Puzzle2States.PointerPlaced)
        {
            transform.Rotate(0, -20, 0);

            float yRotation = transform.localEulerAngles.y % 360;
            bool val = Mathf.Abs(yRotation) <= clockSolvedThreshold;
            if (Mathf.Abs(yRotation) <= clockSolvedThreshold)
            {
                OnClockSolved?.Invoke();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Obj_Clock")
        {
            OnPointerPlaced?.Invoke();
        }
    }
}
