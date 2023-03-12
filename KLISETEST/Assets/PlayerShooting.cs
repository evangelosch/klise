using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject crosshairPrefab; // Prefab for the crosshair object
    public Transform shootingPoint; // Point from which the player will shoot
    public float shootingSpeed = 10f; // Speed at which the bullet will travel
    public LayerMask enemyLayer; // Layer where the enemies are placed

    private GameObject crosshair; // Reference to the current crosshair object
    private Vector2 shootingDirection; // Direction in which the player will shoot
    public Bullet bulletPrefab;
    public Transform enemyPosition;

    public float shootCooldownTime = 1.5f;
    private bool canShoot = true;

    private void Start()
    {
        enemyPosition = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        shootingPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // Update the crosshair position based on the mouse position
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

        // Calculate the shooting direction
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
            // Instantiate a bullet object at the shooting point
            Bullet bullet = Instantiate(bulletPrefab, new Vector2(shootingPoint.position.x, shootingPoint.position.y), Quaternion.identity);
           
            // Set the velocity of the bullet to the shooting direction multiplied by the shooting speed
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootingDirection * shootingSpeed;
            //SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            //spriteRenderer.flipX = true;

            // Destroy the bullet after a certain amount of time
            Destroy(bullet, 2f);

            // Check if the bullet hit an enemy and destroy it
            RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, shootingDirection, Mathf.Infinity, enemyLayer);
            if (hit.collider != null)
            {
                Destroy(hit.collider.gameObject);
            }
            canShoot = false;
            StartCoroutine(StartCooldown());
        }
    }

    public void ShootSpecial(Transform target)
    {
        Bullet bullet = Instantiate(bulletPrefab, new Vector2(shootingPoint.position.x, shootingPoint.position.y), Quaternion.identity);
        Debug.Log("shooting special");
        // Set the velocity of the bullet to the shooting direction multiplied by the shooting speed
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootingDirection * shootingSpeed;
        //SpriteRenderer spriteRenderer = bullet.GetComponent<SpriteRenderer>();
        //spriteRenderer.flipX = true;

        // Destroy the bullet after a certain amount of time
        Destroy(bullet, 2f);

        // Check if the bullet hit an enemy and destroy it
        RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, shootingDirection, Mathf.Infinity, enemyLayer);
        /*if (target != null)
        {
            Destroy(target.gameObject);
        }*/
    }
    IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(shootCooldownTime);
        canShoot = true;
        
    }




}
