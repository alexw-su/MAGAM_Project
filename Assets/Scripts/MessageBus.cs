using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class MessageBus : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject messagePanel;
    private Queue<CanvasMessage> messageQueue = new Queue<CanvasMessage>();
    private bool isDisplayingMessage = false;

    private void Start()
    {
        messagePanel.SetActive(false);
    }

    public void AddMessage(string text, float displayTime)
    {
        CanvasMessage message = new CanvasMessage(text, displayTime);
        messageQueue.Enqueue(message);

        if (!isDisplayingMessage)
        {
            DisplayNextMessage();
        }
    }

    private void DisplayNextMessage()
    {
        if (messageQueue.Count > 0)
        {
            CanvasMessage message = messageQueue.Dequeue();
            StartCoroutine(DisplayMessage(message));
        }
        else
        {
            messagePanel.SetActive(false);
            isDisplayingMessage = false;
        }
    }

    private IEnumerator DisplayMessage(CanvasMessage message)
    {
        isDisplayingMessage = true;
        messageText.text = message.text;
        messagePanel.SetActive(true);

        yield return new WaitForSeconds(message.displayTime);

        messagePanel.SetActive(false);
        isDisplayingMessage = false;
        DisplayNextMessage();
    }


}
