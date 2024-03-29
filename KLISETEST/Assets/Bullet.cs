using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigidBody.position.x > 2 || rigidBody.position.x < -2 || rigidBody.position.y > 1 || rigidBody.position.y < -1)
        {
            Destroy(gameObject);
        }
    }
    public void destroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
            destroyBullet();
        }
    }
}
