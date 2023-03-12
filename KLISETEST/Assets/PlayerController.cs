using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

    private bool isSuccess;
    private Vector2 movementInput;
    private Rigidbody2D rigidbody2D;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public SwordAttack swordAttack;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

  
    private void FixedUpdate()
    {
        PlayerMovement();
        MovementDirection();
    }

    void OnParry()
    {
      
        animator.SetTrigger("swordAttack");
      
    }
    private void PlayerMovement()
    {
        if (movementInput != Vector2.zero)
        {
            isSuccess = TryMove(movementInput);
            animator.SetBool("isMoving", true);
           


            if (!isSuccess)
            {
                isSuccess = TryMove(new Vector2(movementInput.x, 0));

                if (!isSuccess)
                {
                    isSuccess = TryMove(new Vector2(0, movementInput.y));
                }
                if (!isSuccess)
                {
                    animator.SetBool("isMoving", false);
                }

            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {

        if (direction != Vector2.zero)
        {
            // count if there is anything blocking the movement of the player. If list is 0 proceed
            int count = rigidbody2D.Cast(
                direction,
                movementFilter,
                castCollisions,
                movementSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rigidbody2D.MovePosition(rigidbody2D.position + direction * movementSpeed * Time.fixedDeltaTime);
                return true;
            }
        }
       
            return false;
        
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void MovementDirection()
    {
        //Set the direction of the player sprite
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void SwordAttack() {
        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    void EndSwordAttack()
    {
        swordAttack.StopAttack();
    }

    
}
