using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Properties
    public Transform target; // Reference to the target object
    public Vector3 offset; // Offset from the target object
    public float smoothSpeed = 0.125f; // Speed of camera movement

    // Update is called once per frame
    void LateUpdate()
    {
        // Calculate desired position based ont arget's position and offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate the camera's position between current position and desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Set the camera position to the smoothed position
        transform.position = smoothedPosition;

        // Make the camera look at the target object without rotating
        transform.LookAt(target);
    }
}
