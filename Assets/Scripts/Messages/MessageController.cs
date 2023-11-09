using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    private MessageContainer messageContainer = new MessageContainer();
    public TextAsset textJson;
    public bool isTrigger = true;
    public List<string> messageKeys = new List<string>();
    void Start()
    {
        if (textJson != null)
        {
            messageContainer = JsonConvert.DeserializeObject<MessageContainer>(textJson.text);
            //SendMessageToBus(messageContainer.messages["StartMessage"]);
        }
        else
        {
            Debug.Log("messageContainer is not assigned. Please assign a TextAsset with the JSON data.");
        }
    }
    public void SendMessageByKeys()
    {
        foreach (string key in messageKeys)
        {
            if (messageContainer.messages.ContainsKey(key))
            {
                CanvasMessage message = messageContainer.messages[key];
                StartCoroutine(WaitAndSendMessage(message));
                if (message.repeatable == false)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isTrigger && other.tag == "Player")
        {
            SendMessageByKeys();
        }
    }
    IEnumerator WaitAndSendMessage(CanvasMessage message)
    {
        if (message.delay > 0)
        {
            yield return new WaitForSeconds(message.delay);
        }
        MessageBus messageBus = FindObjectOfType<MessageBus>();

        if (messageBus != null)
        {
            messageBus.AddMessage(message.text, message.displayTime);
        }
    }
    public void SendMessageByKey(string key)
    {
        MessageBus messageBus = FindObjectOfType<MessageBus>();
        if (messageBus != null && messageContainer.messages.ContainsKey(key))
        {
            CanvasMessage message = messageContainer.messages[key];
            messageBus.AddMessage(message.text, message.displayTime);
        }
    }
}
