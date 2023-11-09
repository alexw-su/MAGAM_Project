using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject puzzleLocation;
    public string messageKey;
    public Animator transition;

    private PlayerController _playerController;
    private float transitionTime;

    void Start()
    {
        transitionTime = 1f;
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;
        
        StartCoroutine(WindUpTeleport());

        //SendMessagesToMessageBus();
    }

    private IEnumerator WindUpTeleport()
    {
        // Play Fade transition
        transition.SetTrigger("FadeOut");

        // Wait for Fade
        yield return new WaitForSeconds(transitionTime);

        // Teleport Player
        Teleport();
    }

    private void Teleport()
    {
        if (_playerController != null)
        {
            _playerController.TeleportTo = puzzleLocation.transform.position;
            MessageGeneral messageGeneral = FindObjectOfType<MessageGeneral>();
            if (messageGeneral != null)
            {
                messageGeneral.SendMessageByKey(messageKey);
            }
        }

        transition.SetTrigger("FadeIn");
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
