using UnityEngine;

public class EnemyLockOn : MonoBehaviour
{
    public float lockOnRange = 5f;
    public KeyCode lockOnKey = KeyCode.Tab;
    private GameObject lockOnIndicatorPrefab;
    private Transform target;
    private GameObject lockOnIndicator;
    private bool lockOnEnabled = false;
    public GameObject bulletPrefab;
    private Transform shootingPoint;
    private GameObject player;

    private void Start()
    {
        lockOnIndicatorPrefab = Resources.Load<GameObject>("LockOnIndicatorPrefab");
        bulletPrefab = Resources.Load<GameObject>("PlayerBulletPrefab");
        player = GameObject.FindGameObjectWithTag("Player");
        shootingPoint = player.transform;
    }

    private void Update()
    {
        if (lockOnEnabled && target != null && lockOnIndicator != null)
        {
            lockOnIndicator.transform.position = target.position;
        }
    }

    void OnLockEnemy()
    {
        if (!lockOnEnabled)
        {
            target = FindClosestEnemyInRange();
            if (target != null)
            {
                lockOnIndicator = Instantiate(lockOnIndicatorPrefab, target.position, Quaternion.identity);
                ShootSpecial(target);
            }
            lockOnEnabled = true;
        }
        else
        {
            Destroy(lockOnIndicator);
            lockOnEnabled = false;
        }
    }

    private Transform FindClosestEnemyInRange()
    {
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

        return closestEnemy;
    }

    public void ShootSpecial(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector2(shootingPoint.position.x, shootingPoint.position.y), Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * 10f;
    }

    private void OnDestroy()
    {
        if (lockOnIndicator != null)
        {
            Destroy(lockOnIndicator);
        }
    }
}
