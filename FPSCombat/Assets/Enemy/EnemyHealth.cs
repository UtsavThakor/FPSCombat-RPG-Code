using JetBrains.Annotations;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log("Enemy health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Debug.Log("Enemy is Dead!");
            Destroy(gameObject);
        }
    }
}
