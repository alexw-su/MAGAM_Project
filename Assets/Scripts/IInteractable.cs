using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    //All methods are virtual, so that the classes implementing the interface only has to implement the methods it needs.
    //(i.e. classes that don't manage grabbing probably don't need the OnInteractionRunning method.)

    virtual void OnInteractionStart(bool isGrabbing) { }
    virtual void OnInteractionRunning() { }
    virtual void OnInteractionStop() { }
    virtual void Interact() { }
}
