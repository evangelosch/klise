using UnityEngine;

public class EnemyBulletCollision : MonoBehaviour
{
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            playerHealth.TakeDamage(1);
            Destroy(collision.gameObject);
        }
    }
}
