using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    [Header("Raycast")]
    public float raycastLength = 10f;
    public GameObject cameraReference;
    public PickUpController controller;

    [Header("Grabbing")]
    [SerializeField] private Transform interactionHolder;
    [SerializeField] private Transform interactionPoint;
    [Space]
    //[SerializeField] float grabSnapThreshold = 0.5f; --> Keeping this here so that we don't have to add it again if we want to use it
    [SerializeField] float grabLerpingSpeed = 0.5f;
    [SerializeField] AnimationCurve grabLerpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private int _highlightMask;
    private int _defaultMask;
    private GameObject _lastLookedAt;

    InputManager inputManager;

    private List<IInteractable> _interactableObjects;
    private bool _isGrabbing = false;

    //These Properties are so that you can access their value from the outside, but not change them from the outside.
    //Just good practise.
    public Transform InteractionPoint { get => interactionPoint; }
    public float GrabLerpingSpeed { get => grabLerpingSpeed; }
    private void Awake()
    {
        _highlightMask = LayerMask.NameToLayer("Highlight");
        _defaultMask = LayerMask.NameToLayer("Default");
    }
    void Start()
    {
        // Gets Input Manager from scene
        inputManager = InputManager.Instance;

        _interactableObjects = new List<IInteractable>();
    }


    void Update()
    {
        RaycastPlayerAim();
        InteractionHolderUpdate();
        if (!_isGrabbing || _interactableObjects.Count <= 0)
            return;
        foreach (var obj in _interactableObjects)
        {
            obj.OnInteractionRunning();
        }

    }
    void RaycastPlayerAim()
    {
        Vector3 rayDirection = cameraReference.transform.forward;
        if (Physics.Raycast(cameraReference.transform.position, rayDirection, out RaycastHit hitInfo, raycastLength))
        {
            // debugging
            Debug.DrawRay(cameraReference.transform.position, rayDirection * hitInfo.distance, Color.red);

            // Check object hit
            var target = hitInfo.collider.gameObject;
            var interactable = hitInfo.collider.GetComponent<IInteractable>();

            // Check the tag of the hit object
            if (target.CompareTag("Grabbable") || target.CompareTag("Interactable"))
            {
                HandleLookingAtObject(hitInfo);
            }
            else
            {
                ClearLastLookedAt();
            }
        }
        else
        {
            ClearLastLookedAt();
            // debugging
            Debug.DrawRay(transform.position, rayDirection * raycastLength, Color.red);
        }
    }


    //Matches rotation of InteractionHolder to Main Camera
    private void InteractionHolderUpdate()
    {
        interactionHolder.transform.rotation = cameraReference.transform.rotation;
    }


    void HandleLookingAtObject(RaycastHit hitInfo)
    {
        // highlight interactive/grabbable object
        GameObject target = hitInfo.collider.gameObject;

        if (_lastLookedAt != target)
        {
            _lastLookedAt = target;
            SetLayerRecursively(_lastLookedAt.transform, _highlightMask);
        }

        // If object is interacted, run Interact() on all interactable scripts
        if (inputManager.GetPlayerInteracted())
        {
            var interactables = target.GetComponents<IInteractable>();
            if (interactables != null)
            {
                foreach (IInteractable script in interactables)
                {
                    script.OnInteractionStart(false);
                }
            }
        }
        else if (inputManager.GetPlayerGrabbed())
        {
            var interactables = target.GetComponents<IInteractable>();

            if (interactables != null)
            {
                //Run OnInteractionStart with parameter true for grabbing.
                //Also add IInteractable to List of currently active Interactables for running OnInteractionRunning in the update.
                foreach (IInteractable script in interactables)
                {
                    script.OnInteractionStart(true);
                    _interactableObjects.Add(script);
                }
            }
            
            if (target.CompareTag("Grabbable"))
            {
                _isGrabbing = true;
                controller.PickupObject(hitInfo.transform.gameObject);
            }
        }
        else if (!inputManager.GetPlayerGrabbing())
        {
            var interactables = target.GetComponents<IInteractable>();

            if (interactables != null)
            {
                if (_isGrabbing)
                {
                    //Only called when _isGrabbing is true, so it is not called constantly when we are not grabbing anything.
                    for (int i = _interactableObjects.Count - 1; i >= 0; i--)
                    {
                        IInteractable script = _interactableObjects[i];
                        script.OnInteractionStop();
                    }
                    _interactableObjects = new List<IInteractable>();
                    _isGrabbing = false;
                    controller.DropObject();
                }
            }
        }
    }


    void ClearLastLookedAt()
    {
        if (_lastLookedAt != null)
        {
            SetLayerRecursively(_lastLookedAt.transform, _defaultMask);
            _lastLookedAt = null;
        }
    }

    //For Outside access to Raycast hits
    public RaycastHit GetRaycastHit()
    {
        Vector3 rayDirection = cameraReference.transform.forward;

        RaycastHit hitInfo = new RaycastHit();

        if (Physics.Raycast(cameraReference.transform.position, rayDirection, out hitInfo, raycastLength))
        {
            return hitInfo;
        }

        return hitInfo;
    }
    void SetLayerRecursively(Transform objTransform, int layer)
    {
        objTransform.gameObject.layer = layer;

        foreach (Transform child in objTransform)
        {
            SetLayerRecursively(child, layer);
        }
    }
}

