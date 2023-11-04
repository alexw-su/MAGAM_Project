using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour, IInteractable
{
    public Puzzle2 stateManager;
    public GameObject crystal;
    public GameObject doorBlock;
    public ParticleSystem particles1;
    public ParticleSystem particles2;
    void Start()
    {
        crystal.SetActive(false);
        particles1.gameObject.SetActive(false);
        particles2.gameObject.SetActive(false);
    }
    void Update()
    {
        float yRotation = transform.localEulerAngles.y % 360;
        if (yRotation >= -30 && yRotation <= 30)
        {
            stateManager.Change(Puzzle2States.TimeSet);
        }
        if (stateManager.CurrentState == Puzzle2States.TimeSet)
        {
            crystal.SetActive(true);
            doorBlock.GetComponent<Collider>().enabled = false;
            particles1.gameObject.SetActive(true);
            particles2.gameObject.SetActive(true);
        }
    }
    public void OnInteractionStart(bool isGrabbing)
    {
        transform.Rotate(0, -20, 0);
    }
}
