using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 1f;
    public float dashCooldown = 1f;
    public float dashDistance = 5f;

    private Vector2 moveInput;
    private Rigidbody2D rigidbody2D;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Vector2 dashDirection;

    



    void OnDash()
    {
       
        if (dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimer = dashTime;
            dashCooldownTimer = dashCooldown;
            dashDirection = moveInput.normalized;

            rigidbody2D.MovePosition(rigidbody2D.position + dashDirection * dashSpeed * Time.fixedDeltaTime);
            Debug.Log("dash");
            dashTimer -= Time.deltaTime;
           
                isDashing = false;
            

        }
    }



    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        dashCooldownTimer -= Time.fixedDeltaTime;
    }
}
