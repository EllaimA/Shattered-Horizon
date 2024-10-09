using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{ 
    // Properties
    public int baseDamage; // Base damage value
    public float baseHitChance = 0.5f; // 50% base chance to hit
    public float critChance2x = 0.2f; // 20% chance for 2x damage
    public float critChance5x = 0.05f; // 5% chance for 5x damage

    // Method to deal damage to pin object when it collides with the ball
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the pin object
            GameObject pin = collision.gameObject;

            // Get the health component of the pin object
            PinHealth health = pin.GetComponent<PinHealth>();

            // Check if the health component is not null
            if (health != null)
            {
                // Roll for base hit chance
                if (Random.value <= baseHitChance)
                {
                    // Player successfully lands a hit, now check for critical hits
                    int finalDamage = baseDamage;

                    // Check for 5x critical hit first
                    if (Random.value <= critChance5x)
                    {
                        finalDamage *= 5; // Deal 5x damage
                    }
                    // If not 5x, check for 2x critical hit
                    else if (Random.value <= critChance2x)
                    {
                        finalDamage *= 2; // Deal 2x damage
                    }

                    // Deal damage to the pin object
                    health.TakeDamage(finalDamage);

                    // Debug log to track the damage dealt
                    Debug.Log("Damage dealt: " + finalDamage);
                }
                else
                {
                    // Missed hit
                    Debug.Log("Attack missed!");
                }
            }
        }
    }
}
