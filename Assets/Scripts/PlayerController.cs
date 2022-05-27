using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public SwordAttack swordAttack;

    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    private Vector2 movementInput;

    private Rigidbody2D rb;

    private Animator animator;

    private int count;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }



    // Update is called once per frame

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (movementInput.x < 0)
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);

                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);

                }

                if (!success && movementInput.x > 0)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("IsMoving", success);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
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
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        animator.SetTrigger("swordAttack");
        
    }

    public void LockMovement()
    {
        swordAttack.swordCollider.enabled = true;
        canMove = false;
    }

    public void UnlockMovement()
    {
        swordAttack.swordCollider.enabled = false;
        canMove = true;
    }
}

