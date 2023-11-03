using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingScript : MonoBehaviour, IGrabbable
{
    Rigidbody rb;
    public Transform holdingPoint;


    void Start()
    { 
        // Gets the rigidbody component of the object
        rb = GetComponent<Rigidbody>();
    }


    public void Grab()
    {
        // Moves object to the holding point
        rb.MovePosition(holdingPoint.position);
        
        // Disables gravity so that it doesn't slip out of grab
        rb.useGravity = false;
    }

    public void Drop()
    {
        // Reactivates gravity when letting go of object
        rb.useGravity = true;
    }
}
