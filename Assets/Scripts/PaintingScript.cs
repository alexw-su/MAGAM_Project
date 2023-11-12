using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject puzzleLocation;
    public string messageKey;

    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.TeleportTo = puzzleLocation.transform.position;
        }

        MessageController messageController = GetComponent<MessageController>();

        if (messageController == null)
        {
            Debug.LogError("messageController not found!");
        }
        else
        {
            messageController.SendMessageByKey(messageKey);
        }
    }
}
