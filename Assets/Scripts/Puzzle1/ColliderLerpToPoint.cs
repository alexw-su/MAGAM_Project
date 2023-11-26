using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderLerpToPoint: MonoBehaviour
{
    public GameObject gameObjectToCollide;
    public Transform lerpingPoint;
    public float lerpTime;
    float timeElapsed;
    Transform startTransform;
    bool triggeredOnce;

    void Start()
    {
        timeElapsed = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObjectToCollide && !triggeredOnce)
        {   
            triggeredOnce = true;
            other.gameObject.tag = "Untagged";
            startTransform = other.gameObject.transform;
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
            StartCoroutine(LerpToPoint());
        }
    }

    IEnumerator LerpToPoint()
    {
        // Debug.Log("Lerping to Point");

        while (timeElapsed < lerpTime)
        {
            gameObjectToCollide.transform.position = Vector3.Lerp(startTransform.position,lerpingPoint.position,timeElapsed / lerpTime);

            timeElapsed += Time.deltaTime;
            
            yield return null;
        }
        gameObjectToCollide.GetComponent<WateringAnimationHandler>().PlayAnimation();
    }
}