using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleLeave : MonoBehaviour, IInteractable
{
    public delegate void LeavePainting();
    public event LeavePainting OnLeavePainting;

    public void OnInteractionStart(bool isGrabbing)
    {
        OnLeavePainting?.Invoke();
    }
}
