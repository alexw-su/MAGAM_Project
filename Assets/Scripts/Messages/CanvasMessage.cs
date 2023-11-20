using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CanvasMessage
{
    public string text;
    public float displayTime;
    public bool repeatable;
    public float delay;

    public CanvasMessage(string text, float displayTime, bool repeatable = false, float delay = 0)
    {
        this.text = text;
        this.displayTime = displayTime;
        this.repeatable = repeatable;
        this.delay = delay;
    }
}
[System.Serializable]
public class MessageContainer
{
    public Dictionary<string, CanvasMessage> messages;
}
