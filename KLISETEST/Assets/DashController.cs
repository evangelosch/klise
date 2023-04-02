using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashController : MonoBehaviour
{
    [SerializeField] private float teleportDistance = 5.0f;
    private PlayerInput playerInput;
    private InputActionMap inputActionMap;
    private Vector2 movementInput;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        inputActionMap = playerInput.actions.FindActionMap("Player");
      
        inputActionMap.FindAction("Dash").performed += _ => Dash();
        inputActionMap.FindAction("Move").performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActionMap.FindAction("Move").canceled += _ => movementInput = Vector2.zero;
    }

    private void Dash()
    {
        Vector2 movementDirection = movementInput.normalized;
        rigidbody2d.position += movementDirection * teleportDistance;
    }

    private void OnEnable()
    {
        inputActionMap.Enable();
    }

    private void OnDisable()
    {
        inputActionMap.Disable();
    }
}
