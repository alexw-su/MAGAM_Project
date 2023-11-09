using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class MessageGeneral : MonoBehaviour
{
    public TextAsset textJson;
    public MessageContainer messageContainer = new MessageContainer();
    void Start()
    {
        if (textJson != null)
        {
            messageContainer = JsonConvert.DeserializeObject<MessageContainer>(textJson.text);
            SendMessageToBus(messageContainer.messages["StartMessage"]);
            //temporary solution, I didnt have time to come up with something better
            messageContainer.messages["StartMessage"].viewed = true;
        }
        else
        {
            Debug.Log("messageContainer is not assigned. Please assign a TextAsset with the JSON data.");
        }

    }

    void Update()
    {

    }
    private void SendMessageToBus(CanvasMessage message)
    {
        MessageBus messageBus = FindObjectOfType<MessageBus>();

        if (messageBus != null && message.viewed == false)
        {
            messageBus.AddMessage(message.text, message.displayTime);
        }
    }
    public void SendMessageByKey(string key)
    {
        Debug.Log("SendMessageByKey:" + key);

        if (messageContainer.messages.ContainsKey(key))
        {
            Debug.Log("SendMessageByKey: in");
            SendMessageToBus(messageContainer.messages[key]);
            //temporary solution, I didnt have time to come up with something better
            messageContainer.messages[key].viewed = true;
        }
    }
}
