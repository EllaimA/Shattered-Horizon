using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    /*
        Instruction to set the camera up optimally:
            Initially set the camera up in the Scene view while using the preview window to see the Game view.
            Then adjust the Offset to the same values as the camera's position.
    */
    // Properties
    public float movementSpeed;
    public float rotationSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Apply force to the ball
        rb.AddForce(movement * movementSpeed);

        // Calculate rotation based on movement direction and speed
        if (movement != Vector3.zero)
        {
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, movement);
            float rotationAmount = movementSpeed * Time.deltaTime;
            transform.Rotate(rotationAxis, rotationAmount);
        }
    }
}
