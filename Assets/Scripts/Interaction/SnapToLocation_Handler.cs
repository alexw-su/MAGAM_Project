using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLocation_Handler : MonoBehaviour
{
    [SerializeField] Transform snapToLocation;

    public Transform SnapToLocation { get => snapToLocation; }


    public delegate void SnappedToLocation(GameObject gameObject);
    public event SnappedToLocation OnSnappedToLocation;


    public void TriggerSnappedToLocation(GameObject snappedObject)
    {
        OnSnappedToLocation?.Invoke(snappedObject);
    }
}
