using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour, IInteractable
{
    public Puzzle2 stateManager;
    [Space]
    public float clockSolvedThreshold = 30.0f;


    public delegate void PointerPlaced();
    public event PointerPlaced OnPointerPlaced;


    public void OnInteractionStart(bool isGrabbing)
    {
        // Debug.Log("STATE" + stateManager.CurrentState);
        if (stateManager.CurrentState == Puzzle2States.PointerPlaced)
        {
            transform.Rotate(0, -20, 0);

            float yRotation = transform.localEulerAngles.y % 360;
            bool val = Mathf.Abs(yRotation) <= clockSolvedThreshold;
            if (Mathf.Abs(yRotation) <= clockSolvedThreshold)
            {
                stateManager.correctTime = true;
            }
            else
            {
                stateManager.correctTime = false;
            }
        }
    }
    public void OnInteractionRunning()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Obj_Clock")
        {
            OnPointerPlaced?.Invoke();
            SphereCollider sphereCollider = other.GetComponent<SphereCollider>();

            // Check if the SphereCollider exists before destroying it
            if (sphereCollider != null)
            {
                // Destroy the SphereCollider component
                Destroy(sphereCollider);
            }
        }
    }
}
