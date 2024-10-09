using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Properties
    public Rigidbody rb;  // Reference to the Rigidbody component
    public float movementSpeed = 10f;
    public float jumpForce = 5f;
    public bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        HideCursor(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Keep the player upright
        StayUpright();
        
        // Reset the player's rotation if they fall over
        ResetPlayerRotation();

        // If player presses escape, unlock the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideCursor(false);
        }

        // Handle movement
        HandleMovement();

        // Jump if space is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Rotate the player based on the mouse movement
        RotatePlayer();
    }

    // HandleMovement method
    private void HandleMovement()
    {
        // Get input directions
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the desired velocity
        Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;

        // Set the player's velocity directly
        rb.velocity = new Vector3(movement.x * movementSpeed, rb.velocity.y, movement.z * movementSpeed);
    }

    // Jump method
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        isGrounded = false;
    }

    // OnCollisionEnter method
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    // hideCursor method
    private static void HideCursor(bool shouldHide)
    {
        if (shouldHide)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // RotatePlayer method to handle mouse rotation
    private void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * 2f;  // Adjust sensitivity if needed
        transform.Rotate(Vector3.up * mouseX);
    }

    // stayUpright method to keep the player upright
    private void StayUpright()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            Vector3 groundNormal = hit.normal;
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);
        }
    }
    
    // if player falls over, reset the player's rotation
    private void ResetPlayerRotation()
    {
        if (transform.eulerAngles.z > 30 || transform.eulerAngles.x > 30)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
}
