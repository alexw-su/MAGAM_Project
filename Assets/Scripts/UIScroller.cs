using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Manages Credit Roll after game ends
public class UIScroller : MonoBehaviour
{
    public GameObject text;
    public Image background;

    public Transform startPoint;
    public Transform endPoint;

    public float duration = 10.0f;

    private float _timer = 0f;

    public void InitCredits()
    {
        StartCoroutine(CreditRoutine());
    }


    private IEnumerator CreditRoutine()
    {
        background.gameObject.SetActive(true);
        var nwColor = background.color;

        nwColor.a = 0f;

        background.color = nwColor;

        text.gameObject.SetActive(true);

        while (_timer < duration)
        {
            if(_timer < 2f)
            {
                float lerp = _timer / 2f;
                var newColor = background.color;

                newColor.a = Mathf.Lerp(0, 1f, lerp);

                background.color = newColor;
            }
            else
            {
                var newColor = background.color;

                newColor.a = 1f;

                background.color = newColor;
            }

            text.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, _timer/duration);

            _timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
