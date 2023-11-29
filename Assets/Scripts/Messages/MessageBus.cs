using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MessageBus : MonoBehaviour
{
    public TextAsset textJson;
    public Dictionary<string, Dictionary<string, CanvasMessage>> _messageContainer = new Dictionary<string, Dictionary<string, CanvasMessage>>();
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public GameObject messageLogPanel;
    public TextMeshProUGUI messageLogText;
    public TextMeshProUGUI instructionsText;
    private bool _isLogOpen = false;
    // currently displayed messages
    private List<CanvasMessage> _displayedMessagesSet = new List<CanvasMessage>();
    // messages displayed last frame, saved for optitmization
    private List<CanvasMessage> _previousMessages = new List<CanvasMessage>();
    // log with all displayed messages
    private List<CanvasMessage> messageLog = new List<CanvasMessage>();
    InputManager inputManager;


    private void Start()
    {
        inputManager = InputManager.Instance;
        if (textJson != null)
        {
            var jsonData = JsonConvert.DeserializeObject<MessageContainer>(textJson.text);

            if (jsonData != null && jsonData.messages != null)
            {
                var messages = jsonData.messages;

                foreach (var categoryKey in messages.Keys)
                {
                    var messageCategory = messages[categoryKey];
                    _messageContainer[categoryKey] = messageCategory;
                }
            }
            else
            {
                Debug.Log("No messages found in JSON or null data.");
            }
        }
        else
        {
            Debug.Log("textJson is not assigned. Assign a TextAsset with the JSON data.");
        }
        StartCoroutine(HideInstructions());
    }
    private void Update()
    {
        if (inputManager.CKeyPressed())
        {
            _displayedMessagesSet.Clear();
        }
        if (inputManager.YKeyPressed())
        {
            ToggleMessageLog();
        }


        if (!_isLogOpen && !_displayedMessagesSet.SequenceEqual(_previousMessages)) // dont update if nothing changes or log open
        {
            _previousMessages = new List<CanvasMessage>(_displayedMessagesSet);

            if (_displayedMessagesSet.Count > 0)
            {
                string text = "";
                for (int i = 0; i < _displayedMessagesSet.Count; i++)
                {
                    text += _displayedMessagesSet[i].text + "\n"; ;
                    if (i < _displayedMessagesSet.Count - 1)
                    {
                        text += "-\n";
                    }
                }
                messageText.text = text;
                messagePanel.SetActive(true);
            }
            else
            {
                messagePanel.SetActive(false);
            }
        }

    }

    public void AddMessage(string category, string key)
    {
        Debug.Log($"ADD MESSAGE {category}, {key}");

        if (_messageContainer.ContainsKey(category) && _messageContainer[category].ContainsKey(key))
        {
            CanvasMessage message = _messageContainer[category][key];

            if ((!messageLog.Contains(message) || message.repeatable) && !message.inputRequired)
            {
                messageLog.Add(message);
                StartCoroutine(DisplayMessage(message));
            }
            else if (message.inputRequired)
            {
                StartCoroutine(DisplayButtonMessage(message));
            }
            else
            {
                Debug.LogWarning($"Message with category '{category}' and key '{key}' was already displayed.");
            }
        }
        else
        {
            Debug.LogWarning($"Message with category '{category}' and key '{key}' not found.");
        }
    }

    private IEnumerator DisplayMessage(CanvasMessage message)
    {

        yield return new WaitForSeconds(message.delay);
        _displayedMessagesSet.Add(message);
        yield return new WaitForSeconds(message.displayTime);
        _displayedMessagesSet.Remove(message);
    }

    private IEnumerator DisplayButtonMessage(CanvasMessage message)
    {
        // Wait for delay
        yield return new WaitForSeconds(message.delay);
        _displayedMessagesSet.Add(message);

        // Wait for display time
        yield return new WaitForSeconds(message.displayTime);
        
        // Wait for corresponding button
        bool inputPressed = false;
        while(!inputPressed)
        {
            inputPressed = GetInputMatch(message.input);
            yield return null;
        }

        // Remove message
        _displayedMessagesSet.Remove(message);
    }

    // Returns true or false if current player input matches with a Input-enum
    private bool GetInputMatch(Input input)
    {
        // Check if message's required input is not defined
        if(input == Input.None) 
        {
            Debug.LogWarning($"Input-Required Message has not defined a required input to be pressed");
            
            // To move on without problems -> returns true
            return true;
        }

        // Check if message's required input matches with current player input
        if(inputManager.IsPlayerMoving() && input == Input.Move) return true;
        if(inputManager.IsPlayerLooking() && input == Input.Look) return true;
        if(inputManager.GetPlayerJumped() && input == Input.Jump) return true;
        if(inputManager.GetPlayerSprinting() && input == Input.Run) return true;
        if(inputManager.GetPlayerInteracted() && input == Input.Interact) return true;
        if((inputManager.GetPlayerGrabbed() || inputManager.GetPlayerGrabbing()) && input == Input.Jump) return true;
        return false;
    }

    public CanvasMessage GetMessage(string category, string key)
    {
        if (_messageContainer.ContainsKey(category) && _messageContainer[category].ContainsKey(key))
        {
            return _messageContainer[category][key];
        }
        else
        {
            Debug.LogWarning($"Cannot get message '{category}' and key '{key}' not found.");
            return null;
        }
    }
    private IEnumerator HideInstructions()
    {
        yield return new WaitForSeconds(20);
        instructionsText.text = "";

    }
    private void ToggleMessageLog()
    {
        _isLogOpen = !_isLogOpen;
        if (_isLogOpen)
        {
            Time.timeScale = 0f;
            string text = "";
            for (int i = 0; i < messageLog.Count; i++)
            {
                text += messageLog[i].text + "\n"; ;
                if (i < messageLog.Count - 1)
                {
                    text += "-\n";
                }
            }
            messageLogText.text = text;
            messagePanel.SetActive(false);
            inputManager.ToggleLookInput(false);
            messageLogPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            messagePanel.SetActive(true);
            messageLogPanel.SetActive(false);
            inputManager.ToggleLookInput(true);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}


//private string GetMessageKey(CanvasMessage message)
//    {
//        foreach (var category in _messageContainer)
//        {
//            foreach (var kvp in category.Value)
//            {
//                if (kvp.Value == message)
//                {
//                    return kvp.Key;
//                }
//            }
//        }
//        return null;
//    }

//    private string GetMessageCategory(string key)
//    {
//        foreach (var category in _messageContainer)
//        {
//            if (category.Value.ContainsKey(key))
//            {
//                return category.Key;
//            }
//        }
//        return null;
//    }
