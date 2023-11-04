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

    [Header("Audio Clips")]
    [SerializeField] List<AudioClip> backgroundAudioClips;


    private void Start()
    {
        if(!playRandom)
        {
            PlayBackgroundClip(backgroundClipRandomIndex);
            return;
        }

        PlayBackgroundClip(Random.Range(0, backgroundAudioClips.Count));
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
}
