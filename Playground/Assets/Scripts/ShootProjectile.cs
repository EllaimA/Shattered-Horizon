using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    // Properties
    public GameObject projectilePrefab;
    public GameObject shootPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot projectile when player left clicks
        if (Input.GetMouseButtonDown(0))
        {
            // Create a new projectile
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            
            projectile.transform.position = shootPoint.transform.position;
            projectile.transform.rotation = shootPoint.transform.rotation;
            
            // Get the rigidbody of the projectile
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            
            // Add force to the projectile
            rb.AddForce(transform.forward * 1000);
            
            // Destroy the projectile after 2 seconds
            Destroy(projectile, 2);
        }
    }
}
