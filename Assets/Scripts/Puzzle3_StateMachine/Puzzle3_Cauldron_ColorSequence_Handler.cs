using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle3_Cauldron_ColorSequence_Handler : MonoBehaviour
{
    [SerializeField] AnimationCurve pulseAnimationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] GameObject cauldronGooeyObject;

    private bool _started;

    private int _colorIndex = 0;

    private float _timer = 0;
    private float _lerp = 0;
    private float _intervall = 0;

    private List<Color> _colorSequence;

    private Material _material;

    public void InitColorSequence(List<Color> colorSequence, float intervall)
    {
        _started = true;
        _colorSequence = colorSequence;

        _intervall = intervall;

        _material = new Material(cauldronGooeyObject.GetComponent<MeshRenderer>().material);
        cauldronGooeyObject.GetComponent<MeshRenderer>().material = _material;
    }


    private void Update()
    {
        if (!_started)
            return;

        _timer += Time.deltaTime;

        _lerp = _timer;

        if (_timer < _intervall * 0.5f)
        {
            //Light up
            _lerp = _timer / _intervall * 2f;
            _lerp = pulseAnimationCurve.Evaluate(_lerp);

            _material.SetColor("_MainColor", Color.LerpUnclamped(Color.white, _colorSequence[_colorIndex], _lerp));
        }
        else if (_timer >= _intervall * 0.5f && _timer < _intervall)
        {
            _lerp = Mathf.Repeat(_timer, _intervall * 0.5f);
            _lerp = pulseAnimationCurve.Evaluate(_lerp);

            _material.SetColor("_MainColor", Color.LerpUnclamped(_colorSequence[_colorIndex], Color.white, _lerp));
        }
        else
        {
            _timer = 0f;
            _lerp = 0f;

            _colorIndex++;

            if (_colorIndex >= _colorSequence.Count)
            {
                _colorIndex = 0;
            }
        }
    }

}
