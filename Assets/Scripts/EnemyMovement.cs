using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Properties
    public int movementSpeed = 5;
    public Rigidbody rb;  // Reference to the Rigidbody component
    private Vector3 _randomDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Set the initial random direction
        SetRandomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // Keep the enemy upright
        StayUpright();
        // Move the enemy in the random direction
        MoveRandom();
    }
    
    // OnCollisionEnter method
    private void OnCollisionEnter(Collision collision)
    {
        // If the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Take damage from the player
                playerHealth.TakeDamage(10);
            }
        }

        // If the enemy collides with a wall or other obstacle, change direction
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetRandomDirection();  // Pick a new direction if AI collides with an obstacle
        }
    }

    // MoveRandom method using Rigidbody
    private void MoveRandom()
    {
        // Move the enemy in the random direction with Rigidbody.MovePosition
        // rb.MovePosition(rb.position + _randomDirection * (Time.deltaTime * movementSpeed));
        
        var moveX = _randomDirection.x * movementSpeed * Time.deltaTime;
        var moveZ = _randomDirection.z * movementSpeed * Time.deltaTime;
        rb.AddForce(moveX, 0, moveZ);
    }

    // SetRandomDirection method to change movement direction
    private void SetRandomDirection()
    {
        // Generate a new random direction
        _randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }
    
    // stayUpright method to keep the enemy upright
    private void StayUpright()
    {
        // Create a raycast to check the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            // Get the normal of the hit point
            Vector3 groundNormal = hit.normal;
            // Calculate the rotation to keep the enemy upright
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
            // Apply the rotation to the enemy
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f);
        }
    }
}