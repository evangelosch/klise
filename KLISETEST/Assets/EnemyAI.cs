using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject bulletPrefab;    // The bullet prefab to shoot
    //public Transform firePoint;        // The point where the bullet should be spawned
    public float bulletForce = 20f;    // The force with which the bullet should be fired
    public float fireRate = 1f;
    public float bulletSpeed = 1f;
        // The rate at which the enemy should shoot (in seconds)

    private Animator animator;         // The animator component attached to the enemy
    private Transform player;          // The transform of the player object
    private float timeSinceLastFire = 0f;   // The time since the enemy last fired

    void Start()
    {
        
        animator = bulletPrefab.GetComponent<Animator>(); ;
        player = GameObject.FindGameObjectWithTag("Player").transform;    // Find the player object by tag and get its transform
    }

    void Update()
    {
        // Calculate the direction to the player
        Vector2 direction = player.position - transform.position;

        // Set the animator parameters based on the direction to the player
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);

        // Check if it's time to fire again
        if (timeSinceLastFire >= fireRate)
        {
            // Fire at the player's position
            Shoot(direction.normalized);

            // Reset the time since the enemy last fired
            timeSinceLastFire = 0f;
        }
        else
        {
            // Increment the time since the enemy last fired
            timeSinceLastFire += Time.deltaTime;
        }
    }

    void Shoot(Vector2 direction)
    {
        //animator.SetTrigger("shoot");
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);    // Spawn a new bullet at the fire point
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();    // Get the bullet's rigidbody component
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);    // Add a force to the bullet in the direction of the player

        // Set the bullet's rotation based on the direction to the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}
