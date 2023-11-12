using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaintingScript : MonoBehaviour, IInteractable
{
    public GameObject puzzleLocation;
    public string messageKey;
    public Animator transition;
    public Material distortionMaterial;

    private PlayerController _playerController;

    // For Fade Transition
    private float transitionTime;

    // For Distortion
    private float blend;
    private float lerpDuration; 
    private float minDistortionValue; 
    private float maxDistortionValue; 
    private float timeElapsed;


    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        
        // For Fade Transition
        transitionTime = 1f;
        
        // For Distortion
        blend = 0;
        lerpDuration = 1;
        minDistortionValue = 0; 
        maxDistortionValue = 1; 
        timeElapsed = 0;
    }

    public void OnInteractionStart(bool isGrabbing)
    {
        if (isGrabbing)
            return;
        
        StartCoroutine(WindUpTeleport());
        StartCoroutine(GradualDistortion());

        //SendMessagesToMessageBus();
    }

    // Coroutine that teleports player after the fade out transition finishes.
    private IEnumerator WindUpTeleport()
    {
        // Play fade out transition
        transition.SetTrigger("FadeOut");

        // Wait for fade out
        yield return new WaitForSeconds(transitionTime);

        // Teleport Player
        Teleport();
    }

    // Coroutine that gradually increases the distortion.
    private IEnumerator GradualDistortion()
    {     
        while (timeElapsed < 1)
        {
            // Sets distortion amount based on time elapsed
            blend = Mathf.Lerp(minDistortionValue, maxDistortionValue, timeElapsed / lerpDuration);

            // Updates time elapsed
            timeElapsed += Time.deltaTime;

            // Sets distortion amount to the distortion material (to distort screen)
            distortionMaterial.SetFloat("_Blend", blend);
            yield return null;
        }

        // When time elapsed is above 1, set distortion to max.
        blend = maxDistortionValue;
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

        // Reset Distortion
        blend = 0f;
        timeElapsed = 0;
        distortionMaterial.SetFloat("_Blend", 0f);
        
        // Fade in
        transition.SetTrigger("FadeIn");
    }
}
