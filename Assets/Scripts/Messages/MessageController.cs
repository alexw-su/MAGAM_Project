using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MessageController : MonoBehaviour
{
    public bool isTrigger = true;
    public List<StringPair> stringPairs = new List<StringPair>();
    MessageBus messageBus;

    private bool _firstTime = true; // wink wink
    private void Start()
    {
        messageBus = FindObjectOfType<MessageBus>();
    }


    public void SendMessages()
    {
        bool destroySelf = true;
        foreach (StringPair pair in stringPairs)
        {
            if (messageBus != null)
            {
                CanvasMessage msg = messageBus.GetMessage(pair.Category, pair.Key);
                if (msg != null)
                {
                    if (!_firstTime)
                    {
                        if (msg.repeatable)
                        {
                            messageBus.AddMessage(pair.Category, pair.Key);
                        }
                    }
                    else
                    {
                        messageBus.AddMessage(pair.Category, pair.Key);
                    }
                    // no repeatable messages - destroy component
                    if (msg.repeatable)
                    {
                        destroySelf = false;
                    }
                }
                else
                {
                    Debug.LogWarning($"MessageController: category '{pair.Category}' and key '{pair.Key}' not found.");
                }

            }
        }
        if (destroySelf)
        {
            Destroy(gameObject);
        }
        _firstTime = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger && other.tag == "Player")
        {
            SendMessages();
        }
    }
}
