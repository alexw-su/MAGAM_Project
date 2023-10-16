using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance {
        get {
            return _instance;
        }
    }
    private PlayerInput playerInput;

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
    
}
