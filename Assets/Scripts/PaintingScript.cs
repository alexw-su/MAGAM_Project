using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject puzzleLocation;
    public TextAsset textJson;
    public MessageList messageList = new MessageList();

    void Start()
    {
        messageList = JsonUtility.FromJson<MessageList>(textJson.text);
    }
    public void Interact()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.TeleportTo = puzzleLocation.transform.position;
        }
        SendMessagesToMessageBus();
    }
    private void SendMessagesToMessageBus()
    {
        MessageBus messageBus = FindObjectOfType<MessageBus>();

        if (messageBus != null && messageList != null && messageList.messages != null)
        {
            foreach (CanvasMessage message in messageList.messages)
            {
                messageBus.AddMessage(message.text, message.displayTime);
            }
        }
    }
}
