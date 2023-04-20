using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public BoxCollider2D swordCollider;
    public GameObject audioSource;
    private int damage = 1;

    private Vector2 offsetRight = new Vector2(0.1701258f, 1.393821f);
    private Vector2 offsetLeft = new Vector2(-1.237372f, 1.531497f);
    private Vector2 offsetUp = new Vector2(-0.6509732f, 3.369529f);
    private Vector2 offsetDown = new Vector2(-0.6509732f, -0.1854521f);

    private Vector2 boxColliderRightSideSize = new Vector2(0.6131177f, 1.826531f);
    private Vector2 boxColliderLeftSideSize = new Vector2(0.6131177f, 1.826531f);
    private Vector2 boxColliderUpSideSize = new Vector2(0.8914261f, 1.208664f);
    private Vector2 boxColliderDownSideSize = new Vector2(0.8914261f, 1.208664f);

    private PlayerController playerController;

    void Start()
    {
        swordCollider = GetComponent<BoxCollider2D>();
        audioSource = GameObject.FindGameObjectWithTag("parry");
        playerController = transform.parent.GetComponent<PlayerController>();
       
    }


    private void Update()
    {
        Vector2 movementInput = playerController.MovementInput;

        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y)) // Horizontal movement has priority
        {
            if (movementInput.x > 0) // Facing right
            {
                swordCollider.size = boxColliderRightSideSize;
                swordCollider.offset = offsetRight;
            }
            else if (movementInput.x < 0) // Facing left
            {
                swordCollider.size = boxColliderLeftSideSize;
                swordCollider.offset = offsetLeft;
            }
        }
        else
        {
            if (movementInput.y > 0) // Facing up
            {
                swordCollider.size = boxColliderUpSideSize;
                swordCollider.offset = offsetUp;
            }
            else if (movementInput.y < 0) // Facing down
            {
                swordCollider.size = boxColliderDownSideSize;
                swordCollider.offset = offsetDown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log(damage);
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
        swordCollider.enabled = true;
    }

    public void DisableSwordCollider()
    {
        swordCollider.enabled = false;
    }

}
