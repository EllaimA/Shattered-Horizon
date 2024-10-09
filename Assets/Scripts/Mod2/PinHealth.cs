using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHealth : MonoBehaviour
{
    // Properties
    public int health;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Set the health to the max health
        health = maxHealth;    
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        // Subtract the damage from the health
        health -= damage;

        // Check if the health is less than or equal to 0
        if (health <= 0)
        {
            // Destroy the pin object
            Destroy(gameObject);
        }
    }
}
