using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    [Header("Teleport Points")]
    public GameObject teleportToPoint;
    public GameObject teleportPointToMove;

    [Header("Post Processing")]
    public GameObject puzzlePostProcessingObject;
    public GameObject realityPostProcessingObject;
    public bool puzzleEnabled;
    public bool realityEnabled;

    [Header("Messages")]
    public List<StringPair> stringPairs = new List<StringPair>();

    private PlayerController _playerController;
    private InputManager inputManager;
    private bool teleporting;

    [Header("VFX Manager")]
    public VFXManager vfx;

    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        inputManager = InputManager.Instance;
        teleporting = false;
    }

    void Update()
    {
        if(puzzlePostProcessingObject != null)
        {
            puzzleEnabled = puzzlePostProcessingObject.activeSelf;
        }

        if(realityPostProcessingObject != null)
        {
            realityEnabled = realityPostProcessingObject.activeSelf;
        }
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
            inputManager.LockPlayer();

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
        inputManager.UnlockPlayer();

        foreach (StringPair pair in stringPairs)
        {
            MessageBus messageBus = FindObjectOfType<MessageBus>();
            if (messageBus != null)
            {
                messageBus.AddMessage(pair.Category, pair.Key);
            }
        }

        // Toggles On/Off post processing, if post processing is necessary
        if(puzzlePostProcessingObject != null)
        {
            puzzlePostProcessingObject.SetActive(!puzzlePostProcessingObject.activeSelf);
            puzzleEnabled = puzzlePostProcessingObject.activeSelf;
        }

        if(realityPostProcessingObject != null)
        {
            realityPostProcessingObject.SetActive(!realityPostProcessingObject.activeSelf);
            realityEnabled = realityPostProcessingObject.activeSelf;
        }

        // Reset VFX to normak
        vfx.TriggerFadeIn();
        StartCoroutine(vfx.GradualDecreaseDistortion());

        // Enable teleportation
        teleporting = false;
    }
}
