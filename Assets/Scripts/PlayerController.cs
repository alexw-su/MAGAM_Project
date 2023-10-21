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
        gravityValue = -9.81f;
        jumpHeight = 1.0f;

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

        // Moves Player according to input
        MovePlayer();

        // Applying gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
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
        // Gets directional input from inputManager
        Vector2 movement = inputManager.GetPlayerMovement();
        moveDirection = new Vector3(movement.x, 0f, movement.y);

        // Changes/Rotates directional movement based on Camera's direction
        moveDirection = cameraTransform.forward * moveDirection.z + cameraTransform.right * moveDirection.x;
        moveDirection.y = 0f;

        // Checks if jump input was triggered during grounded
        if (inputManager.GetPlayerJumped() && grounded)
        {
            Jump();
        }
    }

    // Uses variables based on input to move player.
    private void MovePlayer()
    {
        // Gives CharacterController movement input
        controller.Move(moveDirection * Time.deltaTime * moveSpeed);

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
