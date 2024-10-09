using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Transform target;
    public float height;
    public float distance;
    public float angle;
    public float smoothSpeed;

    private Vector3 refVelocity;

    private void LateUpdate()
    {
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (!target)
        {
            return;
        }

        // Maintain world position with distance and height
        Vector3 worldPosition = (Vector3.forward * -distance) + (Vector3.up * height);

        // Apply the rotation based on the desired angle
        Vector3 rotatedVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPosition;

        // Get the target position without flattening the y value
        Vector3 targetPosition = target.position;

        // Final camera position based on the target's actual y position
        Vector3 finalPosition = targetPosition + rotatedVector;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed);

        // Have the camera look at the target's position (including y)
        transform.LookAt(target.position);
    }
}