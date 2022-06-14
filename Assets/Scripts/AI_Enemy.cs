using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] Transform guardedObject; // Что охранять
    public Transform player; // Кого атаковать
    public Transform moveSpot; // Точки по которым патрулирует объект
    public float startWaitTimePatrol; // Время ожидания (Патруль)
    public float startWaitTimeAttack; // Время ожидания (Атака)
    private NavMeshAgent agent; // Управление NavMeshAgent
    private Animator animator; // Анимации
    private float waitTimePatrol; // Время ожидания (счётчик-патруль)
    private float waitTimeAttack; // Время ожидания (счётчик-атака)
    bool chill = true;
    private void Start()
    {
        animator = GetComponent<Animator>();

        waitTimePatrol = startWaitTimePatrol;
        waitTimeAttack = startWaitTimeAttack;
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


    private void Chill()
    {
        agent.speed = 1;
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

    private void Angry()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 4f)
            {
                chill = false;
                agent.speed = 3;
                agent.acceleration = 5;
                agent.stoppingDistance = 1f;
                agent.SetDestination(player.transform.position);
                waitTimeAttack = startWaitTimeAttack;
            }
            else
            {
                chill = true;
            }
    }

    public Vector2 RandomPointInAnnulus(Vector2 center, float minRadius, float maxRadius)
    {

        var randomDirection = (Random.insideUnitCircle * center).normalized;

        var randomDistance = Random.Range(minRadius, maxRadius);

        var point = center + randomDirection * randomDistance;

        return point;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("BeeAttack");
        }
    }

    public void ClearWaitTimeAttack()
    {
        waitTimeAttack = startWaitTimeAttack;
    }
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(guardedObject.position, 1f);
        Gizmos.DrawWireSphere(guardedObject.position, 3f);
        Gizmos.DrawWireSphere(transform.position, 4f);

    }
}
