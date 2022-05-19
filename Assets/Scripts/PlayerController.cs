using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private int count;

    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    private Vector2 movementInput;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRen;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            spriteRen.flipX = !(movementInput.x > 0);
            bool success = TryMove(movementInput);

            if (!success && movementInput.x > 0)
            {
                success = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!success && movementInput.y > 0)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetBool("IsMoving", success);
        }   else{
            animator.SetBool("IsMoving", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {
         count = rb.Cast(
         direction,
         movementFilter,
         castCollisions,
         moveSpeed * Time.fixedDeltaTime + collisionOffset);

        

        if (count == 0)
        {
            rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * direction);
            return true;
        } else
        {
            return false;
        }
    }

    private void OnMove (InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
