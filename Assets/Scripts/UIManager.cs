using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    public Canvas pauseScreen;
    public Canvas inGameUIScreen;
    public MessageBus messageBus;
    public CinemachineInputProvider cip;
    
    InputManager inputManager;
    bool paused;
    bool logOpen;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        inputManager = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        logOpen = messageBus.IsLogOpen;

        if(inputManager.EscKeyPressed())
        {
            if(logOpen)
            {
                messageBus.ToggleMessageLog();
            }
            else 
            {
                TogglePauseScreen();
            }
        }
    }

    public void TogglePauseScreen()
    {
        // Opens pause screen
        paused = !paused;
        pauseScreen.gameObject.SetActive(paused);
        
        // Input changes
        if(paused)
        {
            inputManager.LockPlayer();
            cip.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inputManager.UnlockPlayer();
            cip.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Toggles Game UI
        inGameUIScreen.gameObject.SetActive(!paused);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
