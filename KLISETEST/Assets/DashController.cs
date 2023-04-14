using UnityEngine;
using UnityEngine.InputSystem;

public class DashController : MonoBehaviour
{
    [SerializeField] private float teleportDistance = 5.0f;
    [SerializeField] private float bulletDetectionRadius = 0.5f; // Add this line to set the bullet detection radius
    [SerializeField] private string bulletTag = "bullet"; // Add this line to set the bullet tag
    private TimeController timeController; // Add this line to reference the TimeController script

    private PlayerInput playerInput;
    private Vector2 movementInput;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timeController = GetComponent<TimeController>();

        playerInput.actions.FindActionMap("Player").FindAction("Dash").performed += _ => Dash();
        playerInput.actions.FindActionMap("Player").FindAction("Move").performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        playerInput.actions.FindActionMap("Player").FindAction("Move").canceled += _ => movementInput = Vector2.zero;

    }


    private void Dash()
    {
        Vector2 movementDirection = movementInput.normalized;
        rigidbody2d.position += movementDirection * teleportDistance;

        // Add this block to check for nearby bullets and slow time if necessary
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, bulletDetectionRadius);
        foreach (Collider2D hitObject in hitObjects)
        {
            Debug.Log("Hit object tag: " + hitObject.tag);
            if (hitObject.tag == "bullet")
            {

                StartCoroutine(timeController.SlowTime());
                break;
            }
        }
    }

    private void OnEnable()
    {
        playerInput.actions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        playerInput.actions.FindActionMap("Player").Disable();
    }
}
