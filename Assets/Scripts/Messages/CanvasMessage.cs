using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CanvasMessage
{
    public string text;
    public float displayTime;
    public bool viewed;

    public CanvasMessage(string text, float displayTime)
    {
        this.text = text;
        this.displayTime = displayTime;
        this.viewed = false;
    }
}
[System.Serializable]
public class MessageContainer
{
    public Dictionary<string, CanvasMessage> messages;
}
