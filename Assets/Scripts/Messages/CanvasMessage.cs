using System;
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
    public bool addToLog;
    public CanvasMessage(string text, float displayTime, bool repeatable = false, float delay = 0, bool addToLog = true)
    {
        this.text = text;
        this.displayTime = displayTime;
        this.repeatable = repeatable;
        this.delay = delay;
        this.addToLog = addToLog;
    }
}

[System.Serializable]
public class MessageContainer
{
    public Dictionary<string, Dictionary<string, CanvasMessage>> messages;
}
[Serializable]
public class StringPair
{
    public string Category;
    public string Key;
}