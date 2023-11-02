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

    public Vector2 GetPlayerMovement()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return playerInput.Player.Look.ReadValue<Vector2>();
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
        
        if (grab.IsPressed() && grab.triggered) {
            held = true;
        }

        if (grab.IsPressed() && held)
        {
            return playerInput.Player.Grab.triggered;
        }
        
        if (!grab.IsPressed())
        {
            held = false;
            return false;
        }
        return false;
    }

}
