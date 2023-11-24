using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Attribute Values")]
    public float moveSpeed;
    public float acceleration;
    public float sprintMultiplier;
    public float jumpHeight;
    public float gravityValue;
    public bool grounded;

    InputManager inputManager;
    CharacterController controller;
    Vector3 playerVelocity;
    Vector3 moveDirection;
    Transform cameraTransform;

    private float currentMoveSpeed;

    private Vector3? teleportTo = null;
    // public property to access teleportTo
    public Vector3? TeleportTo
    {
        get { return teleportTo; }
        set { teleportTo = value; }
    }
    private bool sprinting;
    void Start()
    {
        // Initializes CharacterController
        controller = gameObject.GetComponent<CharacterController>();

        // Gets InputManager from scene
        inputManager = InputManager.Instance;

        // Gets Main Camera's Transform
        cameraTransform = Camera.main.transform;

        // Setting Attributes
        currentMoveSpeed = 0;

        // Fixes jump registering
        controller.minMoveDistance = 0;
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

        //
        if (inputManager.IsPlayerMoving())
        {
            // Accelerates current speed up to move speed
            currentMoveSpeed += acceleration * Time.deltaTime;
            if (currentMoveSpeed > moveSpeed) currentMoveSpeed = moveSpeed;
        }

        // Moves Player according to input
        MovePlayer();

        // If no movement input, deaccelerate player movement
        if (!inputManager.IsPlayerMoving())
        {
            currentMoveSpeed -= acceleration * Time.deltaTime;
            if(currentMoveSpeed < 0 ) currentMoveSpeed = 0;
        }

        // Applying gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(inputManager.GetPlayerQuit())
        {
            Application.Quit();
        }
    }
    private void FixedUpdate()
    {
        if (teleportTo != null)
        {
            // teleport player to the saved location
            transform.position = TeleportTo.Value;
            TeleportTo = null;
        }
    }

    // Gets player inputs from InputManager and updates variables.
    private void GetInput()
    {
        if(inputManager.IsPlayerMoving())
        {
            // Gets directional input from inputManager
            Vector2 movement = inputManager.GetPlayerMovement();
            moveDirection = new Vector3(movement.x, 0f, movement.y);
        }

        // Changes/Rotates directional movement based on Camera's direction
        moveDirection = cameraTransform.forward * moveDirection.z + cameraTransform.right * moveDirection.x;
        moveDirection.y = 0f;

        // Checks if jump input was triggered during grounded
        if (inputManager.GetPlayerJumped() && grounded)
        {
            Jump();
        }

        // Checks if sprint input is pressed
        if (inputManager.GetPlayerSprinting())
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
    }

    // Uses variables based on input to move player.
    private void MovePlayer()
    {

        // Gives CharacterController movement input
        if(!sprinting) 
        {
            controller.Move(moveDirection * Time.deltaTime * currentMoveSpeed);
        }
        else 
        {
            controller.Move(moveDirection * Time.deltaTime * currentMoveSpeed * sprintMultiplier);
        }

        // Rotates player object's forward. 
        if (moveDirection != Vector3.zero)
        {
            gameObject.transform.forward = moveDirection;
        }


    }

    // Applies jump
    private void Jump()
    {
        // Adds jump to player's velocity
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

}
