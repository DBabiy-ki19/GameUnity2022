using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
    [SerializeField] Transform guardedObject; //Что охранять
    public Transform player; //Кого атаковать
    private NavMeshAgent agent; //Управление NavMeshAgent
    public Transform moveSpot; // Точки по которым патрулирует объект
    private float waitTime; // время ожидания (счётчик)
    public float startWaitTime; // время ожидания (надо задать)
    bool chill = true;
    private void Start()
    {
        waitTime = startWaitTime;
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
                if (waitTime <= 0)
                {
                    waitTime = startWaitTime;
                    moveSpot.position = RandomPointInAnnulus(guardedObject.position, 1f, 3f);
                }
                else
                {
                    waitTime -= Time.deltaTime;
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
            agent.SetDestination(player.transform.position);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(guardedObject.position, 1f);
        Gizmos.DrawWireSphere(guardedObject.position, 3f);
        Gizmos.DrawWireSphere(transform.position, 4f);
        Gizmos.color = Color.red;
    }
}
