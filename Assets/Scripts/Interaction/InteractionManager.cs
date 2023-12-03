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
        // Raycasting
        RaycastPlayerAim();

        // Updating rotation of interaction holder
        InteractionHolderUpdate();

        // Prevents running the loop below
        if (!_isGrabbing || _interactableObjects.Count <= 0)
            return;

        // Loop for grabbable object (interaction) scripts.
        foreach (var obj in _interactableObjects)
        {
            obj.OnInteractionRunning();
        }
    }

    void RaycastPlayerAim()
    {
        Vector3 rayDirection = cameraReference.transform.forward;

        // If player is not holding/grabbing an object already, then use Raycast.
        if (!_isGrabbing)
        {
            if (Physics.Raycast(cameraReference.transform.position, rayDirection, out RaycastHit hitInfo, raycastLength))
            {
                // debugging
                Debug.DrawRay(cameraReference.transform.position, rayDirection * hitInfo.distance, Color.red);

                // Get the object of hitInfo
                var target = hitInfo.collider.gameObject;

                // Check the tag of the hit object
                if (target.CompareTag("Grabbable") || target.CompareTag("Interactable"))
                {
                    HighlightHitObject(target);
                    HandleHitObject(target);
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
        else
        {
            // else, continue handling grabbed/held object.
            if (_lastLookedAt != null)
            {
                HighlightHitObject(_lastLookedAt);
                HandleHitObject(_lastLookedAt);
            }
            else
            {
                ClearLastLookedAt();
            }
        }
    }


    //Matches rotation of InteractionHolder to Main Camera
    private void InteractionHolderUpdate()
    {
        interactionHolder.transform.rotation = cameraReference.transform.rotation;
    }

    // Highlights a given game object.
    private void HighlightHitObject(GameObject target)
    {
        // Check whether target is different from previous highlighted object
        if (_lastLookedAt != target)
        {
            // Remove highlight of previous object
            if (_lastLookedAt != null)
            {
                SetLayerRecursively(_lastLookedAt.transform, _defaultMask);
            }

            // Set current target as previous object and highlight
            _lastLookedAt = target;
            SetLayerRecursively(_lastLookedAt.transform, _highlightMask);

        }
        else if (_isGrabbing)
        {
            SetLayerRecursively(_lastLookedAt.transform, _defaultMask);
        }
    }

    // Handles the interaction or grabbing of given object
    void HandleHitObject(GameObject target)
    {
        // If object is interacted, run OnInteractStart() on all interactable scripts
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
        
        // If object is grabbed, run OnInteractStart() and afterwards InteractRunning() on all interactable scripts
        if (inputManager.GetPlayerGrabbed())
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

            // Set grabbing to true
            _isGrabbing = true;

            // Run Pickup method
            controller.PickupObject(target);
        }
        else if (!inputManager.GetPlayerGrabbing())
        {
            var interactables = target.GetComponents<IInteractable>();

            if (_isGrabbing)
            {
                // If there are any Interactables
                if (interactables != null)
                {
                    //Only called when _isGrabbing is true, so it is not called constantly when we are not grabbing anything.
                    for (int i = _interactableObjects.Count - 1; i >= 0; i--)
                    {
                        IInteractable script = _interactableObjects[i];
                        script.OnInteractionStop();
                    }
                    _interactableObjects = new List<IInteractable>();
                }

                // If object is grabbable
                if (target.CompareTag("Grabbable"))
                {
                    controller.DropObject();
                }

                // Set to grabbing to false
                _isGrabbing = false;
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
        _isGrabbing = false;
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

