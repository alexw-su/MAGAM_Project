using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hitInfo, 2f))
        {
            Debug.DrawRay(transform.position, rayDirection * hitInfo.distance, Color.red);
            Debug.Log("HIT SOMETHING at a distance of: " + hitInfo.distance);
        }
        else
        {
            // If no hit, draw the full length of 20f
            Debug.DrawRay(transform.position, rayDirection * 2f, Color.red);
            Debug.Log("Hit Nothing");
        }
    }
}
