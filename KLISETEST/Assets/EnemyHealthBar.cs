using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Slider healthSlider; // Replace the Image with a Slider component
    private int maxHealth;


    private void Start()
    {
        healthSlider = GameObject.FindGameObjectWithTag("enemyHealthBarSlider").GetComponent<Slider>();
    }
    public void SetHealthSlider(Slider slider)
    {
        this.healthSlider = slider;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }

    public void SetHealth(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }
}
