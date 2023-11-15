using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnCollision_SnapToLocation : MonoBehaviour
{
    [Header("Location")]
    [SerializeField] string locationTag;
    [SerializeField] bool destroyOnSnapFinish = true;

    private SnapToLocation_Handler _snapToLocation_Handler;

    private bool _isInSnapLocation = false;

    private void Start()
    {
    }


    private void OnDestroy()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SnapToLocation_Handler>())
        {
            gameObject.tag = "Untagged";
            _snapToLocation_Handler = other.gameObject.GetComponent<SnapToLocation_Handler>();

            StartCoroutine(SnapToLocation_Routine(_snapToLocation_Handler));
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (_isInSnapLocation)
        {
            _snapToLocation_Handler = null;
            _isInSnapLocation = false;
        }
    }


    private IEnumerator SnapToLocation_Routine(SnapToLocation_Handler snapHandler)
    {
        float timer = 0f;
        float lerp = 0f;

        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        while (timer < snapHandler.SnapSpeed)
        {
            lerp = curve.Evaluate(timer / snapHandler.SnapSpeed);

            transform.position = Vector3.Lerp(startPos, snapHandler.SnapToLocation.position, lerp);

            transform.rotation = Quaternion.Lerp(startRot, snapHandler.SnapToLocation.rotation, lerp);

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = snapHandler.SnapToLocation.position;
        transform.rotation = snapHandler.SnapToLocation.rotation;

        snapHandler.TriggerSnappedToLocation(gameObject);

        if(destroyOnSnapFinish)
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}
