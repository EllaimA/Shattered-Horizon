using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Properties
    public int playerHealth;
    public int maxHealth;
    public bool isDead;

    // Start is called before the first frame update
    private void Start()
    {
        playerHealth = maxHealth;
    }
    // Update is called once per frame
    private void Update()
    {
        
    }
    
    // Add health method
    public void AddHealth(int health)
    {
        playerHealth += health;

        if (playerHealth > maxHealth) playerHealth = maxHealth;
    }

    // Take damage method
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;

        if (playerHealth <= 0) Die();
    }

    // Die method  
    private void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }
}