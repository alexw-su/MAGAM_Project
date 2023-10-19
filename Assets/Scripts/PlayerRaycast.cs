using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    public float raycastLength = 10f;
    public GameObject player;

    // this probably wont be needed later on, but right now thats how I highlight what player is looking at
    public Material greyMaterial;
    public Material yellowMaterial;

    private GameObject _lastLookedAt;
    private Vector3? teleportTo = null;

    private void FixedUpdate()
    {
        if (teleportTo != null)
        {
            // teleport player to the saved location
            player.transform.position = teleportTo.Value;
            teleportTo = null;
        }

    }
    void Update()
    {
        RaycastPlayerAim();
    }

    void RaycastPlayerAim()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hitInfo, raycastLength))
        {
            // debugging
            Debug.DrawRay(transform.position, rayDirection * hitInfo.distance, Color.red);

            string hitObjectTag = hitInfo.collider.tag;

            // Check the tag of the hit object
            if (hitInfo.collider.CompareTag("painting"))
            {
                HandleLookingAtPainting(hitInfo);
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
    void HandleLookingAtPainting(RaycastHit hitInfo)
    {
        // highlight interactive object
        MeshRenderer renderer = hitInfo.collider.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = yellowMaterial;
        }
        if (Input.GetMouseButtonDown(0))
        {
            string objectName = hitInfo.collider.gameObject.name;
            Debug.Log("Moucse Pressed on:" + objectName);
            int index = GetPaintingIndex(objectName);

            if (index != -1)
            {
                // call TeleportPlayerToPainting with the index
                TeleportPlayerToPainting(index);
            }
        }
        // remove highlight
        _lastLookedAt = hitInfo.collider.gameObject;
    }
    void TeleportPlayerToPainting(int index)
    {
        GameObject puzzleLocationObject = GameObject.Find("PuzzleLocation" + index);

        if (puzzleLocationObject != null)
        {
            Vector3 puzzleLocation = puzzleLocationObject.transform.position;
            // save puzzle location
            teleportTo = puzzleLocation;
        }
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
    int GetPaintingIndex(string objectName)
    {
        if (objectName.StartsWith("painting"))
        {
            // extract the index from the object name
            string indexStr = objectName.Substring("painting".Length);
            if (int.TryParse(indexStr, out int index))
            {
                return index;
            }
        }
        // doesnt match the pattern
        return -1;
    }
}
