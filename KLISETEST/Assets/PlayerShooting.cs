using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private GameObject crosshairPrefab;
    private Transform shootingPoint;
    public float shootingSpeed = 10f;
    public LayerMask enemyLayer;

    private GameObject crosshair;
    private Vector2 shootingDirection;
    private GameObject bulletPrefab;

    public float shootCooldownTime = 1.5f;
    private bool canShoot = true;

    private TimeController timeController;

    private void Start()
    {
       
        crosshairPrefab = Resources.Load<GameObject>("CrosshairPrefab");
        crosshair = Instantiate(crosshairPrefab);

        bulletPrefab = Resources.Load<GameObject>("PlayerBulletPrefab");
        timeController = GetComponent<TimeController>();

        shootingPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (timeController.IsTimeSlowed)
        {
            canShoot = true;
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        crosshair.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);

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
            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
            bullet.transform.SetParent(null);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(shootingDirection.x, shootingDirection.y) * shootingSpeed;

            Destroy(bullet.gameObject, 2f);

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
