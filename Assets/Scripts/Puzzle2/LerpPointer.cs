using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPointer : MonoBehaviour
{
    public GameObject gameObjectToCollide;
    public Transform lerpingPoint;
    public float lerpTime;
    float timeElapsed;
    Transform startTransform;
    bool triggeredOnce;
    float minDistance = 0.3f;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObjectToCollide && !triggeredOnce)
        {
            triggeredOnce = true;
            other.gameObject.tag = "Interactable";
            startTransform = other.gameObject.transform;
            StartCoroutine(LerpToPoint(other.gameObject.GetComponent<Rigidbody>()));
        }
    }

    IEnumerator LerpToPoint(Rigidbody rb)
    {
        Quaternion startRotation = gameObjectToCollide.transform.rotation;
        Quaternion targetRotation = lerpingPoint.rotation;

        while (timeElapsed < lerpTime && Vector3.Distance(gameObjectToCollide.transform.position, lerpingPoint.position) > minDistance)
        {
            float t = timeElapsed / lerpTime;
            gameObjectToCollide.transform.position = Vector3.Lerp(startTransform.position, lerpingPoint.position, t);
            gameObjectToCollide.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ensure final position and rotation match the target precisely
        gameObjectToCollide.transform.position = lerpingPoint.position;
        gameObjectToCollide.transform.rotation = targetRotation;

        // destroy the Rigidbody component after lerping
        Destroy(rb);
    }
}
