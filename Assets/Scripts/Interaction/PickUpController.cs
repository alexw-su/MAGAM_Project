using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour, IInteractable
{
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    public Transform holdArea;
    public float pickupRange = 5.0f;
    public float pickupForce = 150.0f;
    public GameObject GetHeldObj { get => heldObj; }

    private void FixedUpdate()
    {
        if (heldObj != null && heldObj.CompareTag("Grabbable"))
        {
            // Moves object to holding area
            MoveObject();
        }
        if (heldObj != null && !heldObj.CompareTag("Grabbable"))
        {
            // Drop object incase tag changes mid-grab
            DropObject();
        }
    }
    public void PickupObject(GameObject pickObj)
    {
        if (pickObj.CompareTag("Grabbable"))
        {
            if (pickObj.GetComponent<Rigidbody>())
            {
                heldObjRB = pickObj.GetComponent<Rigidbody>();
                heldObjRB.useGravity = false;
                heldObjRB.drag = 10;
                heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

                heldObjRB.transform.parent = holdArea;
                heldObj = pickObj;
            }
        }
    }
    public void DropObject()
    {
        if (heldObj != null)
        {
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;

            heldObj.transform.parent = null;
            heldObj = null;


        }
    }
    public void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }
    }
}
