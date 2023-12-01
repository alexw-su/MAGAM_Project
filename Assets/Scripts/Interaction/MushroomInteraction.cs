using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class MushroomInteraction : MonoBehaviour, IInteractable
{
    public MMF_Player feedback;
    
    public void OnInteractionStart(bool isGrabbing) 
    { 
        if(isGrabbing) return;

        feedback.PlayFeedbacks();
    }
}
