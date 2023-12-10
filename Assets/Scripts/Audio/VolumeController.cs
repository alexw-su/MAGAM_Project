using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixerGroup mainMixer;
    [SerializeField] AnimationCurve mixerCurve;
    [Space]
    [SerializeField] TextMeshProUGUI volumeText;

    Slider _slider;


    // Start is called before the first frame update
    private void Start()
    {
        _slider = GetComponent<Slider>();

        _slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }


    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(delegate { ValueChangeCheck(); });
    }

    private void ValueChangeCheck()
    {
        mainMixer.audioMixer.SetFloat("MainVolumeParam", Mathf.Lerp(-80f, 0f, mixerCurve.Evaluate(_slider.value)));

        volumeText.text = ((int)(_slider.value * 100)).ToString() + "%";
    }
}
