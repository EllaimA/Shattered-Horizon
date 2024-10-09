using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    // Properties
    public int impactDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // OnCollisionEnter method
    private void OnCollisionEnter(Collision collision)
    {
        // If the projectile collides with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the enemy's health component
            PlayerHealth enemyHealth = collision.gameObject.GetComponent<PlayerHealth>();
            
            // If the enemy has health
            if (enemyHealth != null)
            {
                // Take damage from the enemy
                enemyHealth.TakeDamage(10);
                // Print the enemy's health to the console
                Debug.Log("Enemy Health: " + enemyHealth.playerHealth);
            }
        }
    }
}
