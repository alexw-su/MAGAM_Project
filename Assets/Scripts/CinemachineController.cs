using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CinemachineController : CinemachineExtension
{
    [SerializeField]
    // A limit to the degree for the vertical rotation
    private float clampAngle = 80f;

    [SerializeField]
    // Camera horizontal rotation speed
    public float horizontalSpeed = 10f;

    [SerializeField]
    // Camera vertical rotation speed
    public float verticalSpeed = 10f;
    public GameObject cameraReference;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void Awake()
    {
        // Get InputManager from scene
        inputManager = InputManager.Instance;

        // Gets the initial rotation of Camera
        startingRotation = transform.localRotation.eulerAngles;

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            // Checks if we are in the stage which orients the camera's target point
            if (stage == CinemachineCore.Stage.Aim)
            {
                // Gets player's mouse input from InputManager
                Vector2 deltaInput = inputManager.GetPlayerLook();

                // Translating player input into rotations
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y -= deltaInput.y * horizontalSpeed * Time.deltaTime;

                // Clamps the camera's vertical rotation
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                // Sets camera rotation based on input.
                state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
