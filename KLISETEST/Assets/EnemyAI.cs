using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 0.1f;
    public GameObject bulletPrefab;
    private Transform firePoint;
    public float fireRate = 1f;

    private float _nextFire;

    private Rigidbody2D _rb;
    private Vector2 _movement;
    private Vector2 _direction;
    public float bulletForce = 20f;
    public float bulletSpeed = 1f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        firePoint = this.transform;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ShootAtRandomIntervals());
    }

    private void Update()
    {
        _direction = player.position - transform.position;
        _direction.Normalize();
        _movement = _direction;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator ShootAtRandomIntervals()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, fireRate));
            //animator.SetTrigger("shoot");
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);    // Spawn a new bullet at the fire point
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    // Get the bullet's rigidbody component
            rb.AddForce(_direction * bulletForce, ForceMode2D.Impulse);    // Add a force to the bullet in the direction of the player

            // Set the bullet's rotation based on the direction to the player
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            bullet.GetComponent<Rigidbody2D>().velocity = _direction * bulletSpeed;
        }
    }
}
