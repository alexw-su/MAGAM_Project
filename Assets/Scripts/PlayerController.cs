using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Attribute Values")]
    public float moveSpeed;
    public float jumpHeight;
    public float gravityValue;
    public bool grounded;

    InputManager inputManager;
    CharacterController controller;
    Vector3 playerVelocity;
    Vector3 moveDirection;
    void Start()
    {
        // Initializes CharacterController and InputManager
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        
        // Setting Attributes
        gravityValue = -9.81f;
        jumpHeight = 1.0f;

        // Hides Cursor
        Cursor.visible = false;
    }
    void Update()
    {
        // Checks if player is touching mesh.
        grounded = controller.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Gets player input
        GetInput();

        // Moves Player according to input
        MovePlayer();

        // Applying gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void GetInput()
    {
        // Gets movement direction from inputManager
        Vector2 movement = inputManager.GetPlayerMovement();
        moveDirection = new Vector3(movement.x, 0f, movement.y);

        // Checks if jump input was triggered during grounded
        if (inputManager.GetPlayerJumped() && grounded)
        {
            Jump();
        }
    }
    private void MovePlayer()
    {
        // Gives CharacterController movement input
        controller.Move(moveDirection * Time.deltaTime * moveSpeed);
        
        // Rotates player object's forward. 
        if(moveDirection != Vector3.zero) 
        {
            gameObject.transform.forward = moveDirection;
        }


    }
    private void Jump()
    {   
        // Adds jump to player's velocity
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

}
