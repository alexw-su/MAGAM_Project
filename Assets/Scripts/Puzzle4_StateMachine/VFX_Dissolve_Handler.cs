using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Dissolve_Handler : MonoBehaviour
{
    [Header("Other Appearance")]
    [SerializeField] bool doSize = true;
    [SerializeField] bool doPosition = true;
    [SerializeField] bool disableAfterDissolve = true;
    [SerializeField] float startSize = 1f;
    [SerializeField] float endSize = 3f;
    [SerializeField] AnimationCurve sizeAnimCurve = AnimationCurve.EaseInOut(0,0,1,1);
    [SerializeField] float startHeight = 4f;
    [SerializeField] float endHeight = 2.2f;

    [Header("Material Stuff")]
    [SerializeField] float yMin;
    [SerializeField] float yMax;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] MeshRenderer affectedGeometry;

    Material _material;

    public void InitAppearance(float duration)
    {
        _material = new Material(affectedGeometry.material);
        affectedGeometry.material = _material;

        StartCoroutine(AppearRoutine(duration));
    }


    public void InitDissapperance(float duration)
    {
        _material = new Material(affectedGeometry.material);
        affectedGeometry.material = _material;

        StartCoroutine(DisappearRoutine(duration));
    }


    private IEnumerator AppearRoutine(float duration)
    {
        float timer = 0;
        float lerp = 0;

        while(timer < duration)
        {
            lerp = timer / duration;

            //Material Part
            _material.SetFloat("_CutOffHeight", Mathf.Lerp(yMin, yMax, lerp));
            _material.SetColor("_EdgeColor", Color.Lerp(startColor, endColor, lerp));

            float tempLerp = sizeAnimCurve.Evaluate(lerp);
            if (doSize)
            {
                //Size Part
                affectedGeometry.gameObject.transform.localScale = Mathf.Lerp(startSize, endSize, tempLerp) * Vector3.one;
            }
            if(doPosition)
            {
                //Position Part
                gameObject.transform.position = new Vector3(transform.position.x, Mathf.Lerp(startHeight, endHeight, tempLerp), transform.position.z);
            }

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _material.SetFloat("_CutOffHeight", yMax);
        _material.SetColor("_EdgeColor", endColor);
    }


    private IEnumerator DisappearRoutine(float duration)
    {
        float timer = 0;
        float lerp = 0;

        while (timer < duration)
        {
            lerp = timer / duration;

            _material.SetFloat("_CutOffHeight", Mathf.Lerp(yMax, yMin, lerp));
            _material.SetColor("_EdgeColor", Color.Lerp(endColor, startColor, lerp));

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _material.SetFloat("_CutOffHeight", yMin);
        _material.SetColor("_EdgeColor", startColor);

        if(disableAfterDissolve)
        {
            gameObject.SetActive(false);
        }
    }
}
