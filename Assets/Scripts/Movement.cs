using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Enum to determine the current movement state of the player
    public enum MovementState
    {
        Idle,
        Running,
        StrafingLeft,
        StrafingRight,
        Jumping
    }

    [Header("Camera Settings")]
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] private MovementState currentState = MovementState.Idle;

    [Header("General Settings")]
    [SerializeField] bool lockCursor = true;

    private float cameraPitch = 0.0f;
    private float velocityY = 0.0f;
    private Vector3 velocity = Vector3.zero;
    private Animator _animator = null;
    private CharacterController controller = null;
    private Vector2 currentDir = Vector2.zero;
    private Vector2 currentDirVelocity = Vector2.zero;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        // Get the animator component from the child object
        _animator = GetComponentInChildren<Animator>();

        // Get the character controller component
        controller = GetComponent<CharacterController>();

        // Lock the cursor to the center of the screen
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        // Update the mouse look, movement, and animator
        UpdateMouseLook();

        // Update the movement and animator
        HandleMovement();

        // Update the animator
        UpdateAnimator();
    }

    void UpdateMouseLook()
    {
        // Get the mouse delta input
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        // Smooth the mouse delta
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        // Update the camera pitch and player rotation
        cameraPitch = Mathf.Clamp(cameraPitch - currentMouseDelta.y * mouseSensitivity, -90.0f, 90.0f);
        
        // Rotate the camera and player
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void HandleMovement()
    {
        // Get the target direction input
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        // Smooth the target direction
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        // Check if the player is grounded
        if (controller.isGrounded)
        {
            // Reset the Y velocity
            velocityY = 0.0f;

            // Determine the movement state
            DetermineMovementState();
        }
        else
        {
            // Set the movement state to jumping
            currentState = MovementState.Jumping;
        }

        // Apply the movement and gravity
        ApplyMovement();
        ApplyGravity();
    }

    void DetermineMovementState()
    {
        // Determine the movement state based on the input
        if (Input.GetKey(KeyCode.W))
        {
            currentState = MovementState.Running;
            // Debug.Log("Running");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentState = MovementState.StrafingLeft;
            // Debug.Log("Strafing Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentState = MovementState.StrafingRight;
            // Debug.Log("Strafing Right");
        }
        else
        {
            currentState = MovementState.Idle;
            // Debug.Log("Idle");
        }
    }


    void ApplyMovement()
    {
        // Determine the speed based on the current movement state
        float speed = walkSpeed;

        // Apply speed multipliers based on the movement state
        switch (currentState)
        {
            case MovementState.Running:
                speed *= 2.0f; // Running speed multiplier
                break;
            case MovementState.StrafingLeft:
            case MovementState.StrafingRight:
                speed *= 0.5f; // Strafing speed multiplier
                break;
        }

        // Move the player based on the current direction and speed
        velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    void ApplyGravity()
    {
        // Apply gravity to the Y velocity
        if (!controller.isGrounded)
        {
            // Apply gravity to the Y velocity
            velocityY += gravity * Time.deltaTime;
        }
    }

    void UpdateAnimator()
    {
        // Update the animator based on the current movement state
        bool isMoving = currentDir != Vector2.zero;

        _animator.SetBool("goingStraight", currentDir.y > 0 && isMoving);
        _animator.SetBool("goingLeft", currentDir.x < 0 && isMoving);
        _animator.SetBool("goingRight", currentDir.x > 0 && isMoving);
        _animator.SetBool("notMoving", !isMoving);
    }

}
