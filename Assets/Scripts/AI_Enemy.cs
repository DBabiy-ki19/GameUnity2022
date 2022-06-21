using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] Transform guardedObject; // ��� ��������

    //player attack
    //--------------------------
    public int attackDamage = 40;

    public float attackRange = 0.5f;

    public Transform attackPoint;

    public LayerMask enemyLayers;
    //---------------------------

    public Transform player; // ���� ���������
    public Transform moveSpot; // ����� �� ������� ����������� ������
    public float startWaitTimePatrol; // ����� �������� (�������)
    public float startWaitTimeAttack; // ����� �������� (�����)
    private NavMeshAgent agent; // ���������� NavMeshAgent
    private Animator animator; // ��������
    private float waitTimePatrol; // ����� �������� (�������-�������)
    private float waitTimeAttack; // ����� �������� (�������-�����)
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


    private void Chill() //�������������� ������� (��������� �� ��������������� ������)
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

    private void Angry() // �� �������
    {
        if (Vector2.Distance(player.transform.position, transform.position) < 4f)
            {
                chill = false;
                agent.speed = 3;
                agent.acceleration = 5;
                agent.stoppingDistance = 1f;
                agent.SetDestination(player.transform.position);
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

    public Vector2 RandomPointInAnnulus(Vector2 center, float minRadius, float maxRadius) //������� ��������� ����� ��� ��������������
    {

        var randomDirection = (Random.insideUnitCircle * center).normalized;

        var randomDistance = Random.Range(minRadius, maxRadius);

        var point = center + randomDirection * randomDistance;

        return point;
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerController player = enemy.GetComponent<PlayerController>();
            player.TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(guardedObject.position, 1f);
        Gizmos.DrawWireSphere(guardedObject.position, 3f);
        Gizmos.DrawWireSphere(transform.position, 4f);
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
