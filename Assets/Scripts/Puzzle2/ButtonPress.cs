using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using static ClockScript;

public class ButtonPress : MonoBehaviour, IInteractable
{
    public Puzzle2 stateManager;
    public float clockSolvedThreshold = 30.0f;
    public delegate void TimeSet();
    public event TimeSet OnTimeSet;
    //public MMF_Player positiveFeedback;
    public ParticleSystem particlesBroken;
    public ParticleSystem particlesDone;
    public AudioClip brokenClockSound;
    public AudioClip workingClockSound;
    AudioSource audioSource;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void OnInteractionStart(bool isGrabbing)
    {
        float yRotation = stateManager.clock1.transform.localEulerAngles.x % 360;
        bool val = Mathf.Abs(yRotation) <= clockSolvedThreshold;
        if (Mathf.Abs(yRotation) <= clockSolvedThreshold && stateManager.clock1.transform.localEulerAngles.y == 0)
        {
            OnTimeSet?.Invoke();
            audioSource.clip = workingClockSound;
            audioSource.Play();
            if (particlesDone != null)
            {
                particlesDone.Play();
            }
        }
        else
        {
            Debug.Log("WRONG");
            audioSource.clip = brokenClockSound;
            audioSource.Play();
            if (particlesBroken != null)
            {
                particlesBroken.Play();
            }
        }
    }
}
