using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;
    private Slider healthSlider;
    public GameObject deathEffectPrefab;

    private void Start()
    {
        GameObject sliderObject = GameObject.FindGameObjectWithTag("HealthSlider");
        if (sliderObject != null)
        {
            healthSlider = sliderObject.GetComponent<Slider>();
        }
        else
        {
            Debug.LogError("Health slider not found! Make sure it's tagged with 'HealthSlider'.");
        }

        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
       /* if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }*/
        Destroy(gameObject);
    }
}

