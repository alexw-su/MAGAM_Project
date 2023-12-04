using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4_OnSnapKey_PlayDoor : MonoBehaviour
{
    [SerializeField] MMF_Player doorPlayer;
    [SerializeField] SnapToLocation_Handler snapToLocationHandler;

    private void Start()
    {
        snapToLocationHandler.OnSnappedToLocation += SnapToLocationHandler_OnSnappedToLocation;
    }

    private void OnDestroy()
    {
        snapToLocationHandler.OnSnappedToLocation -= SnapToLocationHandler_OnSnappedToLocation;
    }

    private void SnapToLocationHandler_OnSnappedToLocation(GameObject gameObject)
    {
        doorPlayer.PlayFeedbacks();
    }
}
