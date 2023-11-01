using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CanvasMessage
{
    public string text;
    public float displayTime;

    public CanvasMessage(string text, float displayTime)
    {
        this.text = text;
        this.displayTime = displayTime;
    }
}
[System.Serializable]
public class MessageList
{
    public CanvasMessage[] messages;
}
