using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject puzzleLocation;
    public string messageKey;

    void Start()
    {

    }


    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;

        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.TeleportTo = puzzleLocation.transform.position;
            MessageGeneral messageGeneral = FindObjectOfType<MessageGeneral>();
            if (messageGeneral != null)
            {
                messageGeneral.SendMessageByKey(messageKey);
            }
        }
        //SendMessagesToMessageBus();

    }


    private void SendMessagesToMessageBus()
    {
        //MessageBus messageBus = FindObjectOfType<MessageBus>();

        //if (messageBus != null && messageList != null && messageList.messages != null)
        //{
        //    foreach (CanvasMessage message in messageList.messages)
        //    {
        //        messageBus.AddMessage(message.text, message.displayTime);
        //    }
        //}
    }
}
