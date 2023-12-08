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
    float timeElapsed;

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
        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        while (timeElapsed < snapHandler.SnapSpeed)
        {
            float t = timeElapsed / snapHandler.SnapSpeed;

            transform.position = Vector3.Lerp(startPos, snapHandler.SnapToLocation.position, t);
            transform.rotation = Quaternion.Lerp(startRot, snapHandler.SnapToLocation.rotation, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = snapHandler.SnapToLocation.position;
        transform.rotation = snapHandler.SnapToLocation.rotation;

        snapHandler.TriggerSnappedToLocation(gameObject);

        if (destroyOnSnapFinish)
        {
            yield return new WaitForEndOfFrame();
            Destroy(gameObject);
        }
    }
}
