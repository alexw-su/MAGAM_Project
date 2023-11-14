using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToLocation_Handler : MonoBehaviour
{
    [SerializeField] Transform snapToLocation;

    [SerializeField] float snapSpeed = 0.5f;

    public Transform SnapToLocation { get => snapToLocation; }
    public float SnapSpeed { get => snapSpeed; }

    public delegate void SnappedToLocation(GameObject gameObject);
    public event SnappedToLocation OnSnappedToLocation;

    //Will Trigger the OnSnappedToLocation Event
    public void TriggerSnappedToLocation(GameObject snappedObject)
    {
        OnSnappedToLocation?.Invoke(snappedObject);
    }
}
