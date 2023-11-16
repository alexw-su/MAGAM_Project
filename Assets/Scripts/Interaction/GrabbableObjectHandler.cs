using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectHandler : MonoBehaviour, IInteractable
{

    public delegate void GrabLetGo();
    public bool isRotatable = true; // Flag to control rotation


    void Start()
    {
    }

    public void OnInteractionStart(bool isGrabbing)
    {
    }
    public void OnInteractionRunning()
    {
    }
    public void OnInteractionStop()
    {
    }

    //Check if snap threshold is reached to position can be snapped to it
    /*private bool CheckIfThresholdReached()
    {
        return Vector3.Distance(transform.position, _interactionManager.InteractionPoint.position) <= _interactionManager.GrabSnapThreshold;
    }*/
}
