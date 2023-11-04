using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectHandler : MonoBehaviour, IInteractable
{
    Rigidbody rb;
    public Transform holdingPoint;

    private bool _isGrabbed = false;


    void Start()
    { 
        // Gets the rigidbody component of the object
        rb = GetComponent<Rigidbody>();
    }

    //Is called, when OnInteraction Start is called with the isGrabbing attached
    public void OnInteractionStart(bool isGrabbing)
    {
        if (!isGrabbing)
            return;

        Debug.Log("now start grabbing");

        _isGrabbed = true;

        rb.useGravity = false;
    }


    //Is Called from the InteractionManager
    public void OnInteractionRunning()
    {
        if (!_isGrabbed)
            return;

        // Moves object to the holding point
        rb.MovePosition(holdingPoint.position);
    }


    //Is called from the InteractionManager, when Grabbing is let go
    public void OnInteractionStop()
    {
        _isGrabbed = false;

        rb.useGravity = true;
    }
}
