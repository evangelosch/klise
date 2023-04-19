using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject healthBarPrefab;
    private EnemyHealthBar enemyHealthBar;
    private Transform healthBarCanvas;
    private void Start()
    {
        currentHealth = maxHealth;
        CreateHealthBar();
    }

    private void CreateHealthBar()
    {
        healthBarCanvas = Instantiate(healthBarPrefab, transform.position, Quaternion.identity).transform;
        healthBarCanvas.SetParent(transform);
        healthBarCanvas.transform.localPosition = new Vector3(0, 1f, 0);
        enemyHealthBar = GameObject.FindGameObjectWithTag("EnemyHealthBarCanvas").GetComponent<EnemyHealthBar>();
        enemyHealthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("playerBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        enemyHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(healthBarCanvas.gameObject);
            Destroy(gameObject);
        }
    }
}
