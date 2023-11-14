using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GrabbableObjectHandler))]
public class GrabbableObject_SnapToLocation : MonoBehaviour
{
    [Header("Location")]
    [SerializeField] string locationTag;

    private GrabbableObjectHandler _grabObjHandler;
    private SnapToLocation_Handler _snapToLocation_Handler;

    private bool _isInSnapLocation = false;

    private void Start()
    {
        _grabObjHandler = GetComponent<GrabbableObjectHandler>();
        _grabObjHandler.OnGrabLetGo += GrabObjHandler_OnGrabLetGo;
    }


    private void OnDestroy()
    {
        _grabObjHandler.OnGrabLetGo -= GrabObjHandler_OnGrabLetGo;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger of: " + other.gameObject.name);

        if (other.gameObject.GetComponent<SnapToLocation_Handler>())
        {
            _snapToLocation_Handler = other.gameObject.GetComponent<SnapToLocation_Handler>();
            _isInSnapLocation = true;
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


    private void GrabObjHandler_OnGrabLetGo()
    {
        if (!_isInSnapLocation)
        {
            Debug.Log("TryAgain");
            return;
        }

        StartCoroutine(SnapToLocation_Routine(_snapToLocation_Handler));
        _snapToLocation_Handler.TriggerSnappedToLocation(this.gameObject);
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
    }
}
