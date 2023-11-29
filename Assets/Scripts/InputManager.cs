using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private PlayerInput playerInput;
    private bool held;


    private void Awake()
    {
        // Destroys multiple instances of Input Manager
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        // Instantiates the Input System
        playerInput = new PlayerInput();

        // Hides Cursor - commented for development time
        //Cursor.visible = false;

        // Locks Cursor to center of view
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    public void LockInput()
    {
        playerInput.Disable();
    }

    public void UnlockInput()
    {
        playerInput.Enable();
    }

    public void ToggleLookInput(bool toggle)
    {
        if (toggle)
        {
            playerInput.Player.Look.Enable();
        }
        else
        {
            playerInput.Player.Look.Disable();
        }

    }
    public Vector2 GetPlayerMovement()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>();
    }

    public bool IsPlayerMoving()
    {
        return playerInput.Player.Movement.IsPressed();
    }

    public Vector2 GetPlayerLook()
    {
        return playerInput.Player.Look.ReadValue<Vector2>();
    }

    public bool IsPlayerLooking()
    {
        var look = GetPlayerLook();
        if(look.x > 0 || look.x < 0) return true;
        if(look.y > 0 || look.y < 0) return true;
        return false;
    }

    public bool GetPlayerJumped()
    {
        return playerInput.Player.Jump.triggered;
    }

    public bool GetPlayerInteracted()
    {
        return playerInput.Player.Interact.triggered;
    }

    public bool GetPlayerGrabbed()
    {
        var grab = playerInput.Player.Grab;

        // sets held to true if the player has held the button down longer than the threshold
        if (grab.triggered)
        {
            held = true;
            return true;
        }
        // Return false when player lets go of button
        if (!grab.IsPressed())
        {
            held = false;
        }
        return false;
    }
    public bool GetPlayerGrabbing()
    {
        return held;

    }

    public bool GetPlayerSprinting()
    {
        return playerInput.Player.Sprint.IsPressed();
    }
    public bool CKeyPressed()
    {
        return playerInput.Player.CPressed.triggered;
    }
    public bool YKeyPressed()
    {
        return playerInput.Player.YPressed.triggered;
    }
    public bool GetPlayerQuit()
    {
        return playerInput.Player.Quit.triggered;
    }
}
