
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject crosshairPrefab;
    public Transform shootingPoint;
    public float shootingSpeed = 10f;
    public LayerMask enemyLayer;

    private GameObject crosshair;
    private Vector2 shootingDirection;
    public PlayerBullet bulletPrefab; // Renamed to PlayerBulletPrefab
    public Transform enemyPosition;

    public float shootCooldownTime = 1.5f;
    private bool canShoot = true;

    public TimeController timeController;

    private void Start()
    {
        enemyPosition = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (timeController.IsTimeSlowed)
        {
            canShoot = true;
        }
        shootingPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (crosshair == null)
        {
            crosshair = Instantiate(crosshairPrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            crosshair.transform.position = worldPosition;
        }

        shootingDirection = (worldPosition - shootingPoint.position).normalized;

    }

    void OnFire()
        {
            Shoot();
        }
    void Shoot()
    {
        if (canShoot)
        {
            PlayerBullet bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootingDirection * shootingSpeed;

            Destroy(bullet, 2f);

            RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, shootingDirection, Mathf.Infinity, enemyLayer);
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }

            if (!timeController.IsTimeSlowed)
            {
                canShoot = false;
                StartCoroutine(StartCooldown());
            }
        }
    }

    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(shootCooldownTime);
        canShoot = true;
    }
}
   