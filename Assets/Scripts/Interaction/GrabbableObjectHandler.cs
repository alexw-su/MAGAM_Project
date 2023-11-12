using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectHandler : MonoBehaviour, IInteractable
{
    private Rigidbody _rb;
    private InteractionManager _interactionManager;

    private bool _isGrabbed = false;

    public delegate void GrabLetGo();
    public event GrabLetGo OnGrabLetGo;


    void Start()
    { 
        // Gets the rigidbody component of the object
        _rb = GetComponent<Rigidbody>();
        _interactionManager = FindObjectOfType<InteractionManager>();
    }

    //Is called, when OnInteraction Start is called with the isGrabbing attached
    public void OnInteractionStart(bool isGrabbing)
    {
        if (!isGrabbing)
            return;

        _isGrabbed = true;

        _rb.useGravity = false;
    }


    //Is Called from the InteractionManager
    public void OnInteractionRunning()
    {
        if (!_isGrabbed)
            return;

        transform.position = Vector3.Lerp(transform.position, _interactionManager.InteractionPoint.position, _interactionManager.GrabLerpingSpeed * Time.deltaTime);
    }


    //Is called from the InteractionManager, when Grabbing is let go
    public void OnInteractionStop()
    {
        _isGrabbed = false;

        _rb.useGravity = true;

        //Is Called to every subscribed object when grab is being let go.
        OnGrabLetGo?.Invoke();
    }

    //Check if snap threshold is reached to position can be snapped to it
    /*private bool CheckIfThresholdReached()
    {
        return Vector3.Distance(transform.position, _interactionManager.InteractionPoint.position) <= _interactionManager.GrabSnapThreshold;
    }*/
}
