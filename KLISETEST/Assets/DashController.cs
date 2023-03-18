using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField] private float teleportDistance = 0.3f;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.actions["Dash"].triggered)
        {
            Vector2 movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
            Vector3 movementDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
            transform.position += movementDirection * teleportDistance;
        }
    }
}
