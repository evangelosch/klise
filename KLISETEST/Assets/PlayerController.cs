using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;

   // private Vector2 MovementInput;
    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public SwordAttack swordAttack;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (MovementInput == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
            return;
        }

        bool isMovementSuccess = TryMove(MovementInput) || TryMove(new Vector2(MovementInput.x, 0)) || TryMove(new Vector2(0, MovementInput.y));
        animator.SetBool("isMoving", isMovementSuccess);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero) return false;

        int count = rigidbody2D.Cast(direction, movementFilter, castCollisions, movementSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + direction * movementSpeed * Time.fixedDeltaTime);
            return true;
        }

        return false;
    }

    private void OnMove(InputValue movementValue)
    {
        MovementInput = movementValue.Get<Vector2>();
        //MovementInput = movementValue.Get<Vector2>();
        if (MovementInput != Vector2.zero)
        {
            animator.SetFloat("Xinput", MovementInput.x);
            animator.SetFloat("Yinput", MovementInput.y);

        }
        if (MovementInput.x < 0){
            spriteRenderer.flipX= true;
        }
        else
        {
            spriteRenderer.flipX= false;
        }
    }

    void OnParry() // Added the OnParry method back
    {
        animator.SetTrigger("swordAttack");
       
    }

    public Vector2 MovementInput { get; private set; }


}
