using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringAnimationHandler : MonoBehaviour
{
    public Puzzle1 stateMachine;
    public puzzle1States state;
    public ParticleSystem ps;
    public float rotationTime;
    public float pouringTime;
    float timeElapsed;
    Rigidbody rb;

    void Start()
    {
        timeElapsed = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        ps.Stop();
    }

    public void PlayAnimation()
    {
        // Prevent from falling
        rb.useGravity = false;
        rb.velocity = Vector3.zero;

        // Prevent continues rotation
        rb.angularVelocity = Vector3.zero;

        // Setup for pouring animation
        ResetRotation();
        StartCoroutine(Pour());
    }

    // Resets the rotation of the watering can.
    void ResetRotation()
    {
        // Debug.Log("Reseting Rotation");
        transform.rotation = Quaternion.identity;
    }

    // Lerps the rotation of the watering can so it looks like it is pouring water
    IEnumerator Pour()
    {
        // Debug.Log("Rotating");
        while (timeElapsed < rotationTime)
        {
            var rotate = 50 / rotationTime * Time.deltaTime;

            // Updates time elapsed
            timeElapsed += Time.deltaTime;

            // Sets rotation for transform
            transform.Rotate(Vector3.right, rotate);
            yield return null;
        }

        // Play watering particles
        ps.Play();
        // Debug.Log("Pouring");

        // Wait until the water is done pouring
        yield return new WaitForSeconds(pouringTime);

        // Stop particles and change states
        ps.Stop();
        stateMachine.Change(state);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
