using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Item_Material_Dissolve : MonoBehaviour
{
    [SerializeField] float yMin;
    [SerializeField] float yMax;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    [SerializeField] MeshRenderer affectedGeometry;

    Material _material;

    private void Start()
    {
        InitAppearance(2f);
    }

    public void InitAppearance(float duration)
    {
        _material = new Material(affectedGeometry.material);
        affectedGeometry.material = _material;

        StartCoroutine(AppearRoutine(duration));
    }


    public void InitDissapperance(float duration)
    {
        StartCoroutine(DisappearRoutine(duration));
    }


    private IEnumerator AppearRoutine(float duration)
    {
        float timer = 0;
        float lerp = 0;

        while(timer < duration)
        {
            lerp = timer / duration;

            _material.SetFloat("_CutOffHeight", Mathf.Lerp(yMin, yMax, lerp));
            _material.SetColor("_EdgeColor", Color.Lerp(startColor, endColor, lerp));

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
    }
}
