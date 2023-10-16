using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
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
    
}
