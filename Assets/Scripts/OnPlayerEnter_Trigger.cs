using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerEnter_Trigger : MonoBehaviour
{

    public delegate void PlayerEnterTrigger();
    public event PlayerEnterTrigger OnPlayerEnterTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            OnPlayerEnterTrigger?.Invoke();
        }
    }
}
