using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float raycastLength = 10f;
    public GameObject cameraReference;

    // this probably wont be needed later on, but right now thats how I highlight what player is looking at
    public Material greyMaterial;
    public Material yellowMaterial;

    GameObject _lastLookedAt;

    InputManager inputManager;

    void Start()
    {
        // Gets Input Manager from scene
        inputManager = InputManager.Instance;
    }
    void Update()
    {
        RaycastPlayerAim();
    }

    void RaycastPlayerAim()
    {
        Vector3 rayDirection = cameraReference.transform.forward;
        if (Physics.Raycast(cameraReference.transform.position, rayDirection, out RaycastHit hitInfo, raycastLength))
        {
            // debugging
            Debug.DrawRay(cameraReference.transform.position, rayDirection * hitInfo.distance, Color.red);
            var interactable = hitInfo.collider.GetComponent<IInteractable>();

            // Check the tag of the hit object
            if (interactable != null)
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
    void HandleLookingAtObject(RaycastHit hitInfo)
    {
        // highlight interactive object
        MeshRenderer renderer = hitInfo.collider.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = yellowMaterial;
        }
        if (inputManager.GetPlayerInteracted())
        {
            hitInfo.collider.GetComponent<IInteractable>().Interact();
        }
        // remove highlight
        _lastLookedAt = hitInfo.collider.gameObject;
    }
    void ClearLastLookedAt()
    {
        if (_lastLookedAt != null)
        {
            MeshRenderer lastRenderer = _lastLookedAt.GetComponent<MeshRenderer>();
            if (lastRenderer != null)
            {
                lastRenderer.material = greyMaterial;
            }
            _lastLookedAt = null;
        }
    }
}

