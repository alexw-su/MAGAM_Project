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

        transform.position = _snapToLocation_Handler.SnapToLocation.position;
        _snapToLocation_Handler.TriggerSnappedToLocation();
    }
}
