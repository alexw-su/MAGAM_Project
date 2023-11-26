using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringAnimationScript : MonoBehaviour
{
    public Puzzle1 stateMachine;
    public puzzle1States state;
    public float rotationTime;
    public float pouringTime;
    float timeElapsed;
    Rigidbody rb;

    void Start()
    {
        timeElapsed = 0;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void PlayAnimation()
    {
        rb.useGravity = false;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;

        ResetRotation();
        StartCoroutine(Pour());
    }

    void ResetRotation()
    {
        // Debug.Log("Reseting Rotation");
        transform.rotation = Quaternion.identity;
    }

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
        
        rb.angularVelocity = Vector3.zero;
        // Debug.Log("Pouring");

        yield return new WaitForSeconds(pouringTime);
        stateMachine.Change(state);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
