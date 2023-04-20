using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 4;
    private int currentHealth;

    public GameObject healthBarPrefab;
    private Slider healthSlider; // Add healthSlider here
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
        healthSlider = GetComponentInChildren<Slider>(); // Modify this line
        SetMaxHealth(maxHealth);
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
        SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(healthBarCanvas.gameObject);
            Destroy(gameObject);
        }
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
