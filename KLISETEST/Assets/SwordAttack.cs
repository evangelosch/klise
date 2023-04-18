using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public GameObject audioSource;
    public float damage = 2;



    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        audioSource = GameObject.FindGameObjectWithTag("parry");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Health -= damage;
            }
        }
        else if (other.CompareTag("bullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity *= -3f;
                audioSource.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void EnableSwordCollider()
    {
        swordCollider.enabled= true;
    }

    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }
}
