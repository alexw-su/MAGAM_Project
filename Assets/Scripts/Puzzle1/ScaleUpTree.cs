using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpTree : MonoBehaviour
{
    public float transformationTime;
    private float timeElapsed;
    private Vector3 currentScale;
    private Quaternion currentRotation;

    void Start()
    {
        timeElapsed = 0;
        currentScale = transform.localScale;
        currentRotation = transform.rotation;
    }
    public IEnumerator ScaleToSize(float size)
    {

        while (timeElapsed < transformationTime)
        {
            // Sets scale based on time elapsed
            var scale = Mathf.Lerp(currentScale.x, size, timeElapsed / transformationTime);
            var rotate = 360 / transformationTime * Time.deltaTime;

            // Updates time elapsed
            timeElapsed += Time.deltaTime;

            // Sets scale and rotation for transform
            transform.localScale = new Vector3(scale,scale,scale);
            transform.Rotate(Vector3.up, rotate);
            yield return null;
            print(rotate);
        }

        // When time elapsed is above 1, set scale and rotation to max.
        transform.localScale = new Vector3(size,size,size);
        
        // Update current scale and rotation for script.
        currentScale = transform.localScale;
        currentRotation = transform.rotation;
        timeElapsed = 0;
    }

    public void StartScaleToSize(float size)
    {
        StartCoroutine(ScaleToSize(size));
    }

}
