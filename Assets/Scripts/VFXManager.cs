using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public FullScreenPassRendererFeature FSPRF;
    private static VFXManager _instance;
    public static VFXManager Instances
    {
        get
        {
            return _instance;
        }
    }
    public Animator transitioner;
    public Material distortionMaterial;

    // For Fade Transition
    public float transitionTime;

    // For Distortion
    private float blend;
    private float lerpDuration;
    private float minDistortionValue;
    private float maxDistortionValue;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        // Destroys multiple instances of Input Manager
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        // FullScreenPassRendererFeature
        DisableFullScreenPassRendererFeature();

        // For Fade Transition
        transitionTime = 1f;

        // For Distortion
        blend = 0;
        lerpDuration = 1;
        minDistortionValue = 0;
        maxDistortionValue = 1;
        timeElapsed = 0;
    }

    // Triggers the fade out transition on animator
    public void TriggerFadeOut()
    {
        transitioner.SetTrigger("FadeOut");
    }

    // Triggers the fade in transition on animator
    public void TriggerFadeIn()
    {
        transitioner.SetTrigger("FadeIn");
    }


    // Coroutine that gradually increases the distortion.
    public IEnumerator GradualIncreaseDistortion()
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
        timeElapsed = 0;
    }

    // Coroutine that gradually increases the distortion.
    public IEnumerator GradualDecreaseDistortion()
    {
        while (timeElapsed < 1)
        {
            // Sets distortion amount based on time elapsed
            blend = Mathf.Lerp(maxDistortionValue, minDistortionValue, timeElapsed / lerpDuration);

            // Updates time elapsed
            timeElapsed += Time.deltaTime;

            // Sets distortion amount to the distortion material (to distort screen)
            distortionMaterial.SetFloat("_Blend", blend);
            yield return null;
        }

        // When time elapsed is above 1, set distortion to 0.
        blend = minDistortionValue;
        timeElapsed = 0;
        DisableFullScreenPassRendererFeature();
    }

    public void EnableFullScreenPassRendererFeature()
    {
        FSPRF.SetActive(true);
    }

    public void DisableFullScreenPassRendererFeature()
    {
        FSPRF.SetActive(false);
    }
}
