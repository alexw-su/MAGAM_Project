using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClockScript;

public class Painting2Leave : MonoBehaviour, IInteractable
{
    public delegate void LeavePainting();
    public event LeavePainting OnLeavePainting;
    public void OnInteractionStart(bool isGrabbing)
    {
        Debug.Log("STARTTTT");
        OnLeavePainting?.Invoke();
    }
}
