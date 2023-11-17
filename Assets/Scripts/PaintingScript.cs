using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject teleportToPoint;
    public GameObject teleportPointToMove;
    public string messageKey;

    private PlayerController _playerController;
    public VFXManager vfx;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;
        
        // Update position of teleport point for other painting
        if(teleportPointToMove != null)
        {
            teleportPointToMove.transform.position = _playerController.transform.position;
        }
        
        // Starts vfx transition
        vfx.EnableFullScreenPassRendererFeature();
        StartCoroutine(WindUpTeleport());
        StartCoroutine(vfx.GradualIncreaseDistortion());

        //SendMessagesToMessageBus();
    }

    // Coroutine that teleports player after the fade out transition finishes.
    private IEnumerator WindUpTeleport()
    {
        // Start fade transition
        vfx.TriggerFadeOut();

        // Wait for fade
        yield return new WaitForSeconds(vfx.transitionTime);

        // Teleport Player
        Teleport();
    }

    // Teleports player to specified transform location
    private void Teleport()
    {
        if (_playerController != null)
        {
            _playerController.TeleportTo = teleportToPoint.transform.position;
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
        
        vfx.TriggerFadeIn();
        StartCoroutine(vfx.GradualDecreaseDistortion());
    }
}
