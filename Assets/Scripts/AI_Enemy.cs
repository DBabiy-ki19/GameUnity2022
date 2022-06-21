using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] Transform guardedObject; // Что охранять
    [SerializeField] Transform player; // От кого

    //player attack
    //--------------------------
    public int attackDamage = 40;

    public float attackRange = 0.5f;

    public Transform attackPoint;

    public LayerMask enemyLayers;
    //---------------------------

    public Transform moveSpot; // Точки по которым патрулирует объект
    public float startWaitTimePatrol; // Время ожидания (Патруль)
    private NavMeshAgent agent; // Управление NavMeshAgent
    private Animator animator; // Анимации
    private float waitTimePatrol; // Время ожидания (счётчик-патруль)
    private bool chill = true;

    private void Start()
    {
        animator = GetComponent<Animator>();

        waitTimePatrol = startWaitTimePatrol;
        moveSpot.position = RandomPointInAnnulus(guardedObject.position, 1f, 3f);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        Chill();
        Angry();
    }


    private void Chill() //патрулирование объекта (двигается по сгенерированным точкам)
    {
        FlipX(moveSpot.position);
        agent.speed = 1;
        agent.stoppingDistance = 0;

        if (chill)
        {
            agent.SetDestination(moveSpot.position);
            if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                if (waitTimePatrol <= 0)
                {
                    waitTimePatrol = startWaitTimePatrol;
                    moveSpot.position = RandomPointInAnnulus(guardedObject.position, 1f, 3f);
                }
                else
                {
                    waitTimePatrol -= Time.deltaTime;
                }
            }
        }

    }

    private void Angry() // ИИ приследует и атакует
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 5f)
        {
            chill = false;
            agent.speed = 3;
            agent.acceleration = 5;
            agent.stoppingDistance = 1f;
            agent.SetDestination(player.transform.position);
            FlipX(player.transform.position);
        }
        if (Vector2.Distance(player.transform.position, attackPoint.position) < 1f)
        {
            animator.SetTrigger("BeeAttack");
        }
        else
        {
            chill = true;
        }
    }

    public void Attack() // Вызывается в анимации атаки пчелы
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerController player = enemy.GetComponent<PlayerController>();
            player.TakeDamage(attackDamage);
        }
    } 

    public Vector2 RandomPointInAnnulus(Vector2 center, float minRadius, float maxRadius) //Функция генерация точки для патрулирования
    {

        var randomDirection = (Random.insideUnitCircle * center).normalized;

        var randomDistance = Random.Range(minRadius, maxRadius);

        var point = center + randomDirection * randomDistance;

        return point;
    }

    private void FlipX(Vector3 stalking)
    {
        if (transform.position.x - stalking.x <= 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(guardedObject.position, 1f);
        Gizmos.DrawWireSphere(guardedObject.position, 3f);
        Gizmos.DrawWireSphere(transform.position, 5f);
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
