using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Custom Playback Options")]
    [SerializeField] bool playRandom = true;
    [SerializeField] int backgroundClipRandomIndex = 0;

    [Header("Audio Source")]
    [SerializeField] AudioSource backgroundSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource footstepsSource;

    [Header("Audio Clips")]
    [SerializeField] List<AudioClip> backgroundAudioClips;
    [SerializeField] List<AudioClip> footStepAudioClip;
    [SerializeField] List<AudioClip> runningAudioClips;

    public bool isPlayingFootsteps = false;


    private void Start()
    {
        if(!playRandom)
        {
            PlayBackgroundClip(backgroundClipRandomIndex);
            return;
        }

        PlayBackgroundClip(Random.Range(0, backgroundAudioClips.Count));
    }

    private void Update()
    {
        if(!footstepsSource.isPlaying && isPlayingFootsteps)
        {
            isPlayingFootsteps = false;
        }
    }


    #region Background
    //Assigns the clip at index to backgroundSource and starts playing it.
    private void PlayBackgroundClip(int index)
    {
        backgroundSource.clip = backgroundAudioClips[index];

        backgroundSource.Play();
    }
    #endregion


    #region SFX
    //Can be called from outside to call any AudioClip, maybe a library would be nice for that.
    public void PlaySFX(AudioClip audioClip)
    {
        sfxSource.PlayOneShot(audioClip);
    }

    #endregion


    #region Footsteps
    public void StartFootsteps(bool running)
    {
        if (!isPlayingFootsteps)
            isPlayingFootsteps = true;

        if(!running)
            footstepsSource.clip = footStepAudioClip[Random.Range(0, footStepAudioClip.Count)];
        else
            footstepsSource.clip = runningAudioClips[Random.Range(0, runningAudioClips.Count)];

        footstepsSource.Play();
    }


    public void PauseFootsteps()
    {
        if(isPlayingFootsteps)
            isPlayingFootsteps = false;

        footstepsSource.Pause();
    }
    #endregion
}
