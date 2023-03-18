using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public float lockOnRange = 5f; // range at which the enemy can be locked on
    public KeyCode lockOnKey = KeyCode.Tab; // key to lock onto enemy
    public GameObject lockOnIndicatorPrefab; // prefab for lock-on indicator
    private Transform target; // reference to the enemy transform
    private GameObject lockOnIndicator; // reference to the lock-on indicator game object
    private bool lockOnEnabled = false;
    public Bullet bulletPrefab;
    private Transform shootingPoint;
    private GameObject player;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       

    }

    private void Update()
    {
        shootingPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void OnLockEnemy()
    {
        if (!lockOnEnabled)
        {
            {
                // find the nearest enemy within lock-on range
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                float closestDistance = Mathf.Infinity;
                Transform closestEnemy = null;

                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance <= lockOnRange && distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy.transform;
                    }
                }

                // lock onto the closest enemy
                target = closestEnemy;

                // create lock-on indicator
                if (lockOnIndicatorPrefab != null && target != null)
                {
                    lockOnIndicator = Instantiate(lockOnIndicatorPrefab, target.position, Quaternion.identity);
                    ShootSpecial(target);
                }
            }
            lockOnEnabled = true;
        }
        else if (lockOnEnabled)
        {
            Destroy(lockOnIndicator);
            lockOnEnabled = false;
            Debug.Log("piou");
        }
    }

    public void ShootSpecial(Transform target)
    {
        Bullet bullet = Instantiate(bulletPrefab, new Vector2(shootingPoint.position.x, shootingPoint.position.y), Quaternion.identity);
        Debug.Log("shooting special");
        // Set the velocity of the bullet to the shooting direction multiplied by the shooting speed
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.velocity = target.transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * 10f;
       
    }

    private void OnDestroy()
    {
        // destroy lock-on indicator when script is disabled
        if (lockOnIndicator != null)
        {
            Destroy(lockOnIndicator);
        }
    }
}
