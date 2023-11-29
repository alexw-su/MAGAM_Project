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
    public bool inputRequired;
    public Input input;
    public CanvasMessage(string text, float displayTime, bool repeatable = false, float delay = 0, bool addToLog = true, bool inputRequired = false, string input = "None")
    {   
        this.text = text;
        this.displayTime = displayTime;
        this.repeatable = repeatable;
        this.delay = delay;
        this.addToLog = addToLog;
        this.inputRequired = inputRequired;

        if(inputRequired)
        {
            Input parsedInput;
            if(Enum.TryParse<Input>(input, true, out parsedInput))
            {
                this.input = parsedInput;
            }
            else 
            {
                Debug.LogWarning("Message's required input is not a recognized Input-Enum");
                this.input = Input.None;
            }
        }
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

public enum Input
{
    None,
    Move,
    Look,
    Jump,
    Run,
    Interact,
    Grab,
    Clear,
    Log,
}