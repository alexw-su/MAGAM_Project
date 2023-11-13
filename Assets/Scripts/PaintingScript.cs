using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject puzzleLocation;
    public string messageKey;
    

    private PlayerController _playerController;
    private VFXManager vfx;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        vfx = VFXManager.Instance;
        print(vfx);
    }

    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;
        
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
            _playerController.TeleportTo = puzzleLocation.transform.position;
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
