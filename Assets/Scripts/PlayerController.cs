using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Attributes")]
    public float moveSpeed;
    public float acceleration;
    public float sprintMultiplier;
    public float jumpHeight;
    public float gravityValue;
    float currentMoveSpeed;
    Vector3 playerVelocity;
    Vector3 moveDirection;

    [Header("Booleans")]
    public bool grounded;
    public bool sprinting;

    [Header("Camera Holder Attributes")]
    public Transform cameraHolder;
    public float bobbingSpeed;
    public float bobbingAmount;
    public float swayAmount;
    float bobbingTime;
    float cameraHolderDefaultY;
    float cameraHolderDefaultX;

    InputManager inputManager;
    CharacterController controller;
    Transform cameraTransform;

    private Vector3? teleportTo = null;
    // public property to access teleportTo
    public Vector3? TeleportTo
    {
        get { return teleportTo; }
        set { teleportTo = value; }
    }
    
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
        cameraHolderDefaultY = cameraHolder.localPosition.y;
        cameraHolderDefaultX = cameraHolder.localPosition.x;

        // Fixes jump registering
        controller.minMoveDistance = 0;
    }
    void Update()
    {
        // Checks if player is touching mesh.
        grounded = controller.isGrounded;

        // Gets player input
        GetInput();

        // If player is giving movement input
        if (inputManager.IsPlayerMoving())
        {
            // Accelerates current speed up to move speed
            currentMoveSpeed += acceleration * Time.deltaTime;
            if (currentMoveSpeed > moveSpeed) currentMoveSpeed = moveSpeed;

            // Add head bobbing and swaying to camera holder
            bobbingTime += Time.deltaTime * bobbingSpeed;
            if (sprinting) bobbingTime += Time.deltaTime * bobbingSpeed;
            cameraHolder.localPosition = new Vector3(
                cameraHolderDefaultX + Mathf.Sin(bobbingTime * 0.5f) * swayAmount, 
                cameraHolderDefaultY + Mathf.Sin(bobbingTime) * bobbingAmount, 
                cameraHolder.localPosition.z
            );
        }

        // Moves Player according to input
        MovePlayer();

        // If no movement input
        if (!inputManager.IsPlayerMoving())
        {
            // Decelerate player movement
            currentMoveSpeed -= acceleration * Time.deltaTime;
            if(currentMoveSpeed < 0 ) currentMoveSpeed = 0;

            // Stop bobbing and swaying and return to default position
            bobbingTime = 0;
            cameraHolder.localPosition = new Vector3(
                Mathf.Lerp(cameraHolder.localPosition.x, cameraHolderDefaultX, Time.deltaTime * bobbingSpeed), 
                Mathf.Lerp(cameraHolder.localPosition.y, cameraHolderDefaultY, Time.deltaTime * bobbingSpeed), 
                cameraHolder.localPosition.z
            );
        }

        // Applying gravity
        if (grounded)
        {
            playerVelocity.y = -(gravityValue / 5);
        }
        else
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;
        }

        controller.Move(playerVelocity * Time.deltaTime);
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
        
            // Changes/Rotates directional movement based on Camera's direction
            moveDirection = cameraTransform.forward * moveDirection.z + cameraTransform.right * moveDirection.x;
            moveDirection.y = 0f;
            moveDirection.Normalize();
        }

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
        grounded = false;
        // Adds jump to player's velocity
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * -gravityValue);
    }

}
