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
        if (stateManager.CurrentState == Puzzle2States.PointerPlaced)
        {
            transform.Rotate(-30f, 0f, 0f, Space.World);
        }
    }
    public void OnInteractionRunning()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "altermative_clock")
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
