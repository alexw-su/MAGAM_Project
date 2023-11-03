using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour, IGrabbable
{
    public void Grab() 
    {
        Debug.Log("Grabbed");
    }

    public void Drop()
    {
        Debug.Log("Dropped");
    }
}
