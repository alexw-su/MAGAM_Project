using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject teleportToPoint;
    public GameObject teleportPointToMove;
    public GameObject postProcessingObject;
    public List<StringPair> stringPairs = new List<StringPair>();

    private PlayerController _playerController;
    private InputManager inputManager;
    private bool teleporting;
    public VFXManager vfx;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        inputManager = InputManager.Instance;
        teleporting = false;
    }

    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;

        if(!teleporting)
        {   
            // Ensures the painting can only be interacted with one at a time
            teleporting = true;

            // Update position of teleport point for other painting
            if(teleportPointToMove != null)
            {
                teleportPointToMove.transform.position = _playerController.transform.position;
            }

            // Locks player inputs
            inputManager.LockInput();

            // Starts vfx transition
            vfx.EnableFullScreenPassRendererFeature();
            StartCoroutine(WindUpTeleport());
            StartCoroutine(vfx.GradualIncreaseDistortion());
        }
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

        // Unlocks the player inputs
        inputManager.UnlockInput();

        foreach (StringPair pair in stringPairs)
        {
            MessageBus messageBus = FindObjectOfType<MessageBus>();
            if (messageBus != null)
            {
                messageBus.AddMessage(pair.Category, pair.Key);
            }
        }

        // Toggles On/Off post processing, if post processing is necessary
        if(postProcessingObject != null)
        {
            postProcessingObject.SetActive(!postProcessingObject.activeSelf);
        }

        // Reset VFX to normak
        vfx.TriggerFadeIn();
        StartCoroutine(vfx.GradualDecreaseDistortion());

        // Enable teleportation
        teleporting = false;
    }
}
