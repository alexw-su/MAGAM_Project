using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingScript : MonoBehaviour/*, IGrabbable*/, IInteractable
{
    Rigidbody rb;
    public Transform holdingPoint;

    private bool _isGrabbed = false;


    void Start()
    { 
        // Gets the rigidbody component of the object
        rb = GetComponent<Rigidbody>();
    }


    /*public void Grab()
    {
        // Moves object to the holding point
        rb.MovePosition(holdingPoint.position);
        
        // Disables gravity so that it doesn't slip out of grab
        rb.useGravity = false;
    }

    public void Drop()
    {
        Debug.Log("being dropped");
        // Reactivates gravity when letting go of object
        rb.useGravity = true;
    }*/


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
