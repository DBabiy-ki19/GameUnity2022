using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //player attack
    //--------------------------
    public int attackDamage = 40;

    public float attackRange = 0.5f;

    public Transform attackPoint;

    public LayerMask enemyLayers;
    //---------------------------

    //player hp
    //---------------------------
    public int maxHealth = 100;
    private int currentHealt;
    public HealthBar healthBar;
    //---------------------------

    public float moveSpeed = 1f;

    public float collisionOffset = 0.05f;

    public ContactFilter2D movementFilter;

    private Vector2 movementInput;

    private Rigidbody2D rb;

    private Animator animator;

    private int count;

    private bool canMove = true;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();



    // Start is called before the first frame update
    private void Start()
    {
        currentHealt = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }



    // Player move

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

    // Player attack
    private void OnFire()
    {
        TakeDamage(20); // Получение урона от атаки
        animator.SetTrigger("swordAttack");
    }

   public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            Enemy enemy1 = enemy.GetComponent<Enemy>();
            enemy1.TakeDamage(attackDamage);
        }
    }
    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    // Player health point
    private void TakeDamage(int damage)
    {
        currentHealt -= damage;
        healthBar.SetHealth(currentHealt);
    }
}

